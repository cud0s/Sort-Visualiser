using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortVisualiser
{
    abstract class Sort
    {
        private bool weighedTime;
        private bool swapUsed = false;
        protected List<int> markList = new List<int>();
        protected List<int> sortedList = new List<int>();
        protected List<int> currentState = new List<int>();
        protected List<int> numbers = new List<int>();

        protected int indexOfCompared;
        protected int indexOfCompared2;

        protected int usedMemory;
        protected int compares;
        protected int moves;

        private int oldMoves;
        private int oldCompares;

        protected int inputLength;
        private Chart chart;
        private RichTextBox textBox;
        private RichTextBox movesBox;
        private RichTextBox comparesBox;
        private RichTextBox bigOBox;
        private RichTextBox inputLengthBox;
        private RichTextBox usedMemoryBox;
        public static int sleepTime
        {
            get;
            set;
        }

        protected String inputNumbers;


        private Color backComparedColor = Color.LightGreen;
        private Color backSortedColor = Color.Gray;
        private Color backDefaultColor = Color.GhostWhite;

        private Color chartDefaultColor = Color.Orange;
        private Color chartMovedColor = Color.Red;
        private Color chartComparedColor = Color.LightGreen;
        private Color chartSortedColor = Color.Navy;

        private Color textMovedColor = Color.Red;
        private Color textDefaultColor = Color.Black;

        public override abstract String ToString();
        public abstract void startSorting(String newInputNumbers);

        protected void textMarkFromList()
        {
            textBox.Clear();
            for (int a = 0; a < numbers.Count; a++)
            {
                textBox.AppendText(Convert.ToString(numbers[a]));
                textBox.SelectionStart = a;
                textBox.SelectionLength = 1;
                if (markList.Contains(a))
                {
                    textBox.SelectionColor = textMovedColor;
                }
                else
                {
                    textBox.SelectionColor = textDefaultColor;
                }

                textBox.SelectionBackColor = backDefaultColor;
            }
            update();
        }

        protected void textMarkCompared()
        {

            for (int a = 0; a < numbers.Count; a++)
            {
                textBox.SelectionStart = a;
                textBox.SelectionLength = 1;
                if (a == indexOfCompared || a == indexOfCompared2)
                {
                    textBox.SelectionBackColor = backComparedColor;
                }
                else
                {
                    textBox.SelectionBackColor = backDefaultColor;
                }
            }
            update();
        }

        protected void textMarkChanged()
        {
            movesBox.Text = moves.ToString();
            movesBox.Refresh();

            textBox.Clear();
            markList.Clear();
            for (int a = 0; a < numbers.Count; a++)
            {
                Color color = textDefaultColor;
                if (a < currentState.Count)
                {
                    if (numbers[a] != currentState[a])
                    {
                        markList.Add(a);
                        color = textMovedColor;
                    }
                }
                else
                {
                    markList.Add(a);
                    color = textMovedColor;
                }
                textBox.SelectionStart = textBox.TextLength;
                textBox.SelectionLength = 0;

                textBox.SelectionColor = color;
                textBox.SelectionBackColor = backDefaultColor;
                textBox.AppendText(Convert.ToString(numbers[a]));

                textBox.SelectionColor = textBox.ForeColor;
            }
            update();
            updateCurrStateList();
        }

        private void markSorted()
        {
            for (int a = 0; a < numbers.Count; a++)
            {
                textBox.SelectionStart = a;
                textBox.SelectionLength = 1;
                if (sortedList.Contains(a) && textBox.SelectionBackColor == backDefaultColor)
                {
                    textBox.SelectionBackColor = backSortedColor;
                }
            }
        }

        void drawChart()
        {
            chart.Series["series1"].Points.Clear();
            for (int a = 0; a < numbers.Count; a++)
            {
                chart.Series["series1"].Points.AddY(numbers[a]);
                chart.Series["series1"].Points[a].Color = chartDefaultColor;
                if (sortedList.Contains(a))
                {
                    chart.Series["series1"].Points[a].Color = chartSortedColor;
                }

                if (markList.Contains(a))
                {
                    chart.Series["series1"].Points[a].Color = chartMovedColor;
                }

                if (a==indexOfCompared || a == indexOfCompared2)
                {
                    chart.Series["series1"].Points[a].Color = chartComparedColor;
                }
            }
            chart.Refresh();
        }

        protected void update()
        {
            drawChart();
            markSorted();
            movesBox.Text = moves.ToString();
            movesBox.Refresh();

            usedMemoryBox.Text = usedMemory.ToString();
            usedMemoryBox.Refresh();

            comparesBox.Text = compares.ToString();
            comparesBox.Refresh();

            textBox.Refresh();

            bigOBox.Text = getBigO();
            bigOBox.Refresh(); //may delete this later


            markList.Clear();
            indexOfCompared = indexOfCompared2 = -1;

            int newSTime;
            if (weighedTime)
            {
                newSTime = Convert.ToInt32((moves - oldMoves) * sleepTime * 0.7);
                newSTime += Convert.ToInt32((compares - oldCompares) * sleepTime * 0.3);
                if (newSTime < 0)
                {
                    newSTime = 0;
                }
                if (newSTime > 7000)
                {
                    newSTime = 7000;
                }
                oldMoves = moves;
                oldCompares = compares;
            }
            else
            {
                newSTime = sleepTime;
            }
            System.Threading.Thread.Sleep(newSTime);
        }

        private void updateCurrStateList()
        {
            String stateString = textBox.Text;
            currentState.Clear();
            foreach (char a in stateString)
            {
                currentState.Add((int)a - '0');
            }
        }
        protected void inputData(String newInputNumbers)
        {
            swapUsed = false;
            usedMemory = 0;
            compares = 0;
            moves = 0;
            sortedList = new List<int>();
            inputNumbers = newInputNumbers;
            numbers.Clear();
            textBox.Clear();

            chart.Series["series1"].Color = chartDefaultColor;
            foreach (char a in inputNumbers)
            {
                int newInt = (int)a - '0';
                if (newInt >= 0 && newInt < 10)
                {
                    numbers.Add(newInt);
                }
                textBox.AppendText(Convert.ToString(a));
                chart.Series["series1"].Points.AddY(a);
                textBox.Refresh();
                chart.Refresh();
            }
            inputLength = numbers.Count;
            inputLengthBox.Text = inputLength.ToString();
            inputLengthBox.Refresh();

            updateCurrStateList();
        }

        protected void swapAndMark(int a, int b)
        {
            swap(a, b);
            textMarkChanged();
        }

        /**
         * Takes index a and b, and swaps values numbers[a], numbers[b]
         * */
        protected void swap(int a, int b)
        {
            if (!swapUsed)
            {
                usedMemory++;
                swapUsed = true;
            }
            moves++;
            int temp = numbers[b];
            numbers[b] = numbers[a];
            numbers[a] = temp;
        }

        protected void logComparison(int a, int b)
        {
            compares++;
            indexOfCompared = a;
            indexOfCompared2 = b;
            textMarkCompared();
        }

        protected String getBigO()
        {
            String answer = "O(n)";
            double ratio = (moves + compares) * 1.0 / inputLength * 1.0;
            //Debug.WriteLine(ratio);
            if ((ratio / inputLength) >= inputLength)
            {
                answer = "O(n^n)";
            }
            else
            {
                if (ratio >= 1)
                {
                    if (ratio > 1.5)
                    {
                        if (ratio / inputLength > 0.3)
                        {
                            answer = "O(n^2)";
                        }
                        else
                        {
                            answer = "O(n * " + Convert.ToInt32(ratio).ToString() + ")";
                        }
                    }
                    else
                    {
                        if (ratio > 1.0)
                        {
                            answer = "O(n + " + Convert.ToInt32(moves - inputLength).ToString() + ")";
                        }
                        else
                        {
                            answer = "O(n)";
                        };
                    }
                }
            }
            return answer;
        }

        public Sort(RichTextBox newTextBox, RichTextBox newMovesBox, RichTextBox newComparesBox, RichTextBox newInputLength, RichTextBox newBigO, RichTextBox newUsedMemory, Chart newChart, Boolean newWeighedTime)
        {
            weighedTime = newWeighedTime;
            chart = newChart;
            usedMemoryBox = newUsedMemory;
            inputLengthBox = newInputLength;
            bigOBox = newBigO;
            textBox = newTextBox;
            movesBox = newMovesBox;
            comparesBox = newComparesBox;
            sleepTime = 700;
        }
    }
}

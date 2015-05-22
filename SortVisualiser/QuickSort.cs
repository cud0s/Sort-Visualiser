using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortVisualiser
{
    class QuickSort : Sort
    {

        public override void startSorting(string newInputNumbers)
        {
            inputData(newInputNumbers);
            reorder(0, numbers.Count - 1);
        }

        private void reorder(int from, int to)
        {
            int pivot = choosePivot(from, to);

            usedMemory++;
            swapAndMark(pivot, to);
            int storeIndex = from;
            for (int a = from; a < to; a++)
            {
                logComparison(a, to);
                if (numbers[a] < numbers[to])
                {
                    swapAndMark(storeIndex, a);
                    storeIndex++;
                }
            }
            swapAndMark(storeIndex, to);
            sortedList.Add(storeIndex);

            logComparison(from, storeIndex - 1);
            if (from < storeIndex - 1)
            {
                reorder(from, storeIndex - 1);
            }
            else
            {
                sortedList.Add(from);
                sortedList.Add(storeIndex - 1);
            }

            logComparison(to, storeIndex + 1);
            if (storeIndex + 1 < to)
            {
                reorder(storeIndex + 1, to);
            }
            else
            {
                sortedList.Add(to);
                sortedList.Add(storeIndex + 1);
            }
            usedMemory--;
        }

        private int partition(int from, int to)
        {
            return choosePivot(from, to);
        }

        private int choosePivot(int from, int to)
        {
            Debug.WriteLine((to + from) / 2);
            return (to + from) / 2;
        }

        public override string ToString()
        {
            return "Quick Sort";
        }
        public QuickSort(RichTextBox a, RichTextBox b, RichTextBox c, RichTextBox d, RichTextBox e, RichTextBox f, Chart g, bool h)
            : base(a, b, c, d, e, f, g, h)
        {
        }
    }
}

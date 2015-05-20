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
    class BubbleSort : Sort
    {
        public override String ToString()
        {
            return "Bubble Sort";
        }
        public override void startSorting(String newInputNumbers)
        {
            inputData(newInputNumbers);
            for (int a = 0; a < numbers.Count-1; a++)
            {
                for (int b = 0; b < numbers.Count-a-1; b++)
                {
                    logComparison(b, b + 1);
                    if (numbers[b] > numbers[b+1])
                    {
                        swapAndMark(b, b+1);
                    }
                }
                sortedList.Add(numbers.Count - a - 1);
                update();
            }
            sortedList.Add(0);
            update();
        }



        public BubbleSort(RichTextBox a, RichTextBox b, RichTextBox c, RichTextBox d, RichTextBox e, RichTextBox f, Chart g, bool h)
            : base(a, b, c, d, e, f, g, h)
        {
        }
    }
}

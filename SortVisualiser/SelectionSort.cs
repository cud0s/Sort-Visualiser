using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortVisualiser
{
    class SelectionSort : Sort
    {
        public override String ToString()
        {
            return "Selection Sort";
        }
        public override void startSorting(String newInputNumbers)
        {
            inputData(newInputNumbers);

            for (int a = 0; a < numbers.Count - 1; a++)
            {
                int lowest = a;
                for (int b = a + 1; b < numbers.Count; b++)
                {
                    logComparison(lowest, b);
                    if (numbers[lowest] > numbers[b])
                    {
                        lowest = b;
                    }
                }
                sortedList.Add(a);
                swapAndMark(a, lowest);
            }
            sortedList.Add(numbers.Count - 1);
            update();
        }



        public SelectionSort(RichTextBox a, RichTextBox b, RichTextBox c, RichTextBox d, RichTextBox e, RichTextBox f, Chart g, bool h)
            : base(a, b, c, d, e, f, g, h)
        {
        }

    }
}

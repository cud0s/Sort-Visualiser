using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortVisualiser
{
    class InsertionSort : Sort
    {
        public override String ToString()
        {
            return "Insertion Sort";
        }
        public override void startSorting(String newInputNumbers)
        {
            inputData(newInputNumbers);
            sortedList.Add(0);
            for (int a = 1; a < numbers.Count; a++)
            {
                int b = a;
                int logIndex = a;
                logComparison(a, b - 1);
                while (b > 0 && numbers[b - 1] > numbers[b])
                {
                    if (a != b)
                    {
                        logComparison(b, b - 1);
                    }
                    else
                    {
                        sortedList.Add(a);
                    }
                    swap(b, b - 1);

                    markList.Add(b-1);
                    markList.Add(b);
                    textMarkFromList();
                    b--;
                }
                if (b != 0)
                {
                    logComparison(b, b - 1);
                }

                    //    markList.Add(b);
                sortedList.Add(a);
                textMarkFromList();
            }
            textMarkFromList();            
        }

        public InsertionSort(RichTextBox a, RichTextBox b, RichTextBox c, RichTextBox d, RichTextBox e, RichTextBox f, Chart g, bool h)
            : base(a, b, c, d, e, f, g, h)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortVisualiser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            Object selectedLoad = listBox.SelectedItem;
            if (selectedLoad is Sort)
            {
                ((Sort)selectedLoad).startSorting(richTextBox1.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false; 
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            
            chart1.Series["series1"].Points.AddY(9);
            chart1.Series["series1"].Points[0].Color = Color.Transparent;
            chart1.BackColor = chart1.ChartAreas["ChartArea1"].BackColor = Color.Transparent;

            listBox.Items.Add(new BubbleSort(richTextBox2, richTextBox4, richTextBox3, richTextBox5, richTextBox6, richTextBox7, chart1, checkBox1.Checked));
            listBox.Items.Add(new InsertionSort(richTextBox2, richTextBox4, richTextBox3, richTextBox5, richTextBox6, richTextBox7, chart1, checkBox1.Checked));
            listBox.Items.Add(new SelectionSort(richTextBox2, richTextBox4, richTextBox3, richTextBox5, richTextBox6, richTextBox7, chart1, checkBox1.Checked));
            listBox.Items.Add(new QuickSort(richTextBox2, richTextBox4, richTextBox3, richTextBox5, richTextBox6, richTextBox7, chart1, checkBox1.Checked));
            listBox.Items.Add(new ShellSort(richTextBox2, richTextBox4, richTextBox3, richTextBox5, richTextBox6, richTextBox7, chart1, checkBox1.Checked));


            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(label8, "SWAG - scientifically based wild ass guess");
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 600;
            toolTip1.ReshowDelay = 1000;

            comboBox1.SelectedIndex = 1;
            listBox.SelectedIndex = 0;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double timeInS = Double.Parse(comboBox1.SelectedItem.ToString());
            int timeInMs = Convert.ToInt32(timeInS * 1000);
            Sort.sleepTime = timeInMs;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int RowNumber= 1;
        int ColumnNumber = 1;
        int CounterQ=0;
        string Empty = null;
        bool Flag1 = true; //флаг колличества команд
        bool Flag2 = false;
        string OutQ;
        string[] AccessRights = new string[10] ;
        int NullVar = 0;
        bool Outflow = false;


        Dictionary<int,string> subjects = new Dictionary<int,string>(); //  ключ номер строки таблицы
        Dictionary<int, string> objects = new Dictionary<int, string>(); //  ключ номер столбца таблицы
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.TopLeftHeaderCell.Value = "Subjects/Objects";

            dataGridView1.Visible =false ;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox1.Text=="")
            {
                MessageBox.Show ("Enter Subject!");
                return;
            }
            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }
            subjects.Add(RowNumber,textBox1.Text);
            dataGridView1.RowCount = RowNumber + 1;
            dataGridView1.Rows[RowNumber-1].HeaderCell.Value = textBox1.Text;
            RowNumber = RowNumber + 1;
            textBox9.AppendText("CS[" + textBox1.Text + "];");
            objects.Add(ColumnNumber, textBox1.Text);
            dataGridView1.ColumnCount = ColumnNumber + 1;
            dataGridView1.Columns[ColumnNumber - 1].HeaderText = textBox1.Text;
            ColumnNumber = ColumnNumber + 1;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Enter Subject!");
                return;
            }
            int DeleteRow = 0, RowCounter;
            bool Found = false;
            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }
            RowCounter = Int32.Parse(dataGridView1.RowCount.ToString());
            ICollection<int> skeys = subjects.Keys;
            foreach (int j in skeys)
            {

                if (subjects[j] == textBox3.Text)
                {

                    DeleteRow = j - 1;
                    Found = true;
                    break;
                }

            }

            if (Found == false)
            {
                MessageBox.Show("Entered Subject " + textBox3.Text + " does not exist!");
                return;
            }

            dataGridView1.Rows.RemoveAt(DeleteRow);
            DeleteRow = DeleteRow + 1;
            subjects.Remove(DeleteRow);

            for (int m = 1; m < RowCounter; m++)
            {

                if (m == DeleteRow + 1)
                {
                    string value = subjects[m];
                    subjects.Remove(m);
                    subjects.Add(m - 1, value);
                    DeleteRow = DeleteRow + 1;

                }
            }

            RowNumber = RowNumber - 1;



            int DeleteColumn = 0, ColumnCounter;

            ColumnCounter = Int32.Parse(dataGridView1.ColumnCount.ToString());
            ICollection<int> okeys = objects.Keys;
            foreach (int j in okeys)
            {

                if (objects[j] == textBox3.Text)
                {

                    DeleteColumn = j - 1;
                    break;
                }

            }

            dataGridView1.Columns.RemoveAt(DeleteColumn);
            DeleteColumn = DeleteColumn + 1;
            objects.Remove(DeleteColumn);

            for (int m = 1; m < ColumnCounter; m++)
            {

                if (m == DeleteColumn + 1)
                {
                    string value = objects[m];
                    objects.Remove(m);
                    objects.Add(m - 1, value);
                    DeleteColumn = DeleteColumn + 1;

                }
            }

            ColumnNumber = ColumnNumber - 1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                MessageBox.Show("Enter Law!");
                return;
            }

            if (textBox6.Text == "")
            {
                MessageBox.Show("Enter Subject!");
                return;
            }

            if (textBox5.Text == "")
            {
                MessageBox.Show("Enter Object!");
                return;
            }
            int Column = 0, Row = 0;
            string NullRule = null;

            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }

            if (Flag1 == true)
            {
                textBox10.AppendText('Q' + (CounterQ - 1).ToString() + ";");
                Flag1 = false;
            }

            ICollection<int> okeys = objects.Keys;
            ICollection<int> skeys = subjects.Keys;
            foreach (int j in skeys)
            {

                if (subjects[j] == textBox6.Text)
                {

                    Row = j - 1;
                    break;
                }

            }

            foreach (int j in okeys)
            {

                if (objects[j] == textBox5.Text)
                {

                    Column = j - 1;
                    break;
                }
            }

            NullRule = ' ' + NullRule + dataGridView1[Column, Row].Value + textBox11.Text + ' ';
            dataGridView1[Column, Row].Value = NullRule;
            textBox9.AppendText("ER[" + textBox11.Text + ';' + textBox6.Text + ';' + textBox5.Text + "];");
            if (Outflow == false)
            {
                int ColumnCounter, RowCounter;
                ColumnCounter = Int32.Parse(dataGridView1.ColumnCount.ToString());
                RowCounter = Int32.Parse(dataGridView1.RowCount.ToString());
                for (int i = 0; i <= RowCounter - 1; i++)
                {
                    for (int j = 0; j <= ColumnCounter - 1; j++)
                    {
                        if (dataGridView1[j, i].Value != null)
                        {
                            string prov = dataGridView1[j, i].Value.ToString();
                            if (prov == " w " || prov == " r " || prov == " w r " || prov == " r w ")
                            {


                                for (int m = 0; m < ColumnCounter - 1; m++)
                                {

                                    if (dataGridView1[m, j].Value != null)
                                    {
                                        if (dataGridView1[m, i].Value == null || dataGridView1[m, j].Value.ToString() != dataGridView1[m, i].Value.ToString())   //объекты не сравнивают
                                        {

                                            OutQ = CounterQ.ToString();
                                            AccessRights[NullVar] = "ER[" + textBox11.Text + ';' + textBox6.Text + ';' + textBox5.Text + "];";
                                            Outflow = true;
                                            return;

                                        }
                                    }
                                }
                                for (int m = 0; m < ColumnCounter - 1; m++)
                                {

                                    if (dataGridView1[m, i].Value != null & m != j)
                                    {
                                        if (dataGridView1[m, j].Value == null || dataGridView1[m, j].Value.ToString() != dataGridView1[m, i].Value.ToString())   //объекты не сравнивают
                                        {

                                            OutQ = CounterQ.ToString();
                                            AccessRights[NullVar] = "ER[" + textBox11.Text + ';' + textBox6.Text + ';' + textBox5.Text + "];";
                                            Outflow = true;
                                            return;

                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Object!");
                return;
            }
            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }
            objects.Add(ColumnNumber,textBox2.Text);
            dataGridView1.ColumnCount = ColumnNumber + 1;
            dataGridView1.Columns[ColumnNumber - 1].HeaderText = textBox2.Text;
            ColumnNumber = ColumnNumber + 1;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            textBox9.AppendText("CO[" + textBox2.Text + "];");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Enter Object!");
                return;
            }
            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }
            int DeleteColumn = 0, ColumnCounter;
            bool Found = false;
            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }
            ColumnCounter = Int32.Parse(dataGridView1.ColumnCount.ToString());
            ICollection<int> okeys = objects.Keys;
            foreach (int j in okeys)
            {

                if (objects[j] == textBox4.Text)
                {

                    DeleteColumn = j - 1;
                    Found = true;
                    break;
                }

            }

            if (Found == false)
            {
                MessageBox.Show("Entered Object " + textBox4.Text + " does not exist!");
                return;
            }

            dataGridView1.Columns.RemoveAt(DeleteColumn);
            DeleteColumn = DeleteColumn + 1;
            objects.Remove(DeleteColumn);

            for (int m = 1; m < ColumnCounter; m++)
            {

                if (m == DeleteColumn + 1)
                {
                    string value = objects[m];
                    objects.Remove(m);
                    objects.Add(m - 1, value);
                    DeleteColumn = DeleteColumn + 1;

                }
            }

            ColumnNumber = ColumnNumber - 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                MessageBox.Show("Enter Law!");
                return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("Enter Subject!");
                return;
            }
            if (textBox8.Text == "")
            {
                MessageBox.Show("Enter Object!");
                return;
            }
            string value= null;
            if (Flag2 == false)
            {
                MessageBox.Show("Create a Command!");
                return;
            }

          
            
            int Column = 0, Row = 0;
           
            ICollection<int> okeys = objects.Keys;
            ICollection<int> skeys = subjects.Keys;
            foreach (int j in skeys)
            {

                if (subjects[j] == textBox7.Text)
                {

                    Row = j-1;
                    break;
                }

            }

            foreach (int j in okeys)
            {

                if (objects[j] == textBox8.Text)
                {

                    Column = j-1;
                    break;
                }
            }

            if (dataGridView1[Column, Row].Value != null)
            {
                value = value + dataGridView1[Column, Row].Value;
                dataGridView1[Column, Row].Value = Empty;
                value = value.Remove(value.IndexOf(textBox12.Text), 1);
                dataGridView1[Column, Row].Value = value;
                textBox9.AppendText("DR[" + textBox12.Text + ';' + textBox7.Text + ';' + textBox8.Text + "];");
            }
            else
            {
                MessageBox.Show("Given cell is empty!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            Flag2 = true;

            if (RowNumber == 1)
            {
                textBox9.Text = textBox9.Text + 'Q' + CounterQ + ':';
                CounterQ = CounterQ + 1;
            }
            else
            {
                textBox9.Text = textBox9.Text + Environment.NewLine;
                textBox9.Text = textBox9.Text + 'Q' + CounterQ + ':';
                CounterQ = CounterQ + 1;

            }
            Flag1 = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (CounterQ == 0)
            {
                MessageBox.Show("There is no Command!");
                return;
            }
            dataGridView1.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(OutQ) == 0)
            {

                MessageBox.Show("The system is secure!");
                return;
            }
            textBox10.Text = textBox10.Text.Substring(textBox10.Text.IndexOf(OutQ)-1); 

                MessageBox.Show("Outflow laws in the command Q" +(Convert.ToInt32(OutQ)-1).ToString() + " when making a law " + AccessRights[NullVar]);
                return;
            
        }

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV4_1
{
    public partial class Form1 : Form
    {
        double Xb, Xe, Xs, a, b, d, x, y;
        double[] X, Y;
        bool Err;

        #region ТекстБоксы
        void TextBoxMethod(double num, TextBox tb, ErrorProvider ep)
        {
            button2.Enabled = false;
            bool error;
            try
            {
                num = Convert.ToDouble(tb.Text);
                error = false;
            }
            catch
            {
                error = true;
            }
            if (error)
            {
                if (tb.Text == "") ep.SetError(tb, "Значение отсутствует.");
                else ep.SetError(tb, "Неправильно введены данные.");
                button2.Enabled = false;
            }
            else
                ep.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(Xb, textBox1, errorProvider1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(Xe, textBox2, errorProvider2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(a, textBox3, errorProvider3);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(b, textBox4, errorProvider4);
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(d, textBox5, errorProvider5);
        }
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = true;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = false;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = false;
        }

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            Err = true;
            button2.Enabled = false;
            listBox1.SelectedIndex = 0;
        }
        class ValueTable
        {
            public double x { get; }
            public double y { get; }
            public ValueTable(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBoxMethod(Xb, textBox1, errorProvider1);
            TextBoxMethod(Xe, textBox2, errorProvider2);
            if (radioButton2.Checked)
            {
                TextBoxMethod(a, textBox3, errorProvider3);
                TextBoxMethod(b, textBox4, errorProvider4);
            }
            else
                TextBoxMethod(d, textBox5, errorProvider5);

            try
            {
                Xs = Convert.ToDouble(listBox1.SelectedItem);
                Xb = Convert.ToDouble(textBox1.Text);
                Xe = Convert.ToDouble(textBox2.Text);
                if (radioButton2.Checked)
                {
                    a = Convert.ToDouble(textBox3.Text);
                    b = Convert.ToDouble(textBox4.Text);
                }
                else
                    d = Convert.ToDouble(textBox5.Text);
                if (!(Xe > Xb))
                {
                    errorProvider2.SetError(textBox2, "Значение конечного X должно быть больше начального X.");
                    throw new Exception();
                } 
                listBox2.Items.Clear();
                List<ValueTable> vt = new List<ValueTable>();
                for (x = Xb; x < Xe + 1.0 / 100000000; x += Xs)
                {
                    switch (radioButton1.Checked)
                    {
                        case true: y = Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - d); break;
                        case false: y = 0.01 * (a + b * x) - Math.Pow(Math.E, Math.Pow(x, 3) + b); break;
                    }
                    vt.Add(new ValueTable(x, y));
                    listBox2.Items.Add(string.Format("{0,12:0.0000} ----- {1:F3}", x, y));
                }
                X = new double[vt.Count];
                Y = new double[vt.Count];
                int ii = 0;
                foreach(var g in vt)
                {
                    X[ii] = g.x;
                    Y[ii] = g.y;
                    ii++;
                }
                button2.Enabled = true;
            }
            catch
            {
                button2.Enabled = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(X, Y, Xb, Xe, Xs);
            f2.Show();
        }

    }
}

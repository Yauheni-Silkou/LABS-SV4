using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV4_2
{
    public partial class Wizard : Form
    {
        public enum Function
        {
            F1, F2
        }

        Function function;
        int count;
        //----
        double xBegin, xEnd, xStep, a, b, d;
        Color graphColor;
        MainWindow mainWindow;


        public Wizard(MainWindow window)
        {
            InitializeComponent();
            count = 1;
            mainWindow = window;
        }

        bool TextBoxMethod(ref double number, TextBox tb, ErrorProvider ep)
        {
            bool error;
            try
            {
                number = Convert.ToDouble(tb.Text);
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
            }
            else
                ep.Clear();
            return error;
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            panel1.Location = panel2.Location = panel3.Location
                = panel4.Location = new Point(20, 0);
            panel1.Visible = true;
            buttonBack.Enabled = false;
            buttonNext.Enabled = false;
            buttonFinish.Enabled = false;
            listBox1.SelectedIndex = 3;
            comboBox1.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            count--;
            buttonBack.Enabled = count > 1;
            buttonNext.Enabled = count < 4;
            buttonFinish.Enabled = count == 3 || count == 4;
            switch (count)
            {
                case 1:
                    panel2.Visible = false;
                    panel1.Visible = true;
                    break;
                case 2:
                    panel3.Visible = false;
                    panel2.Visible = true;
                    break;
                case 3:
                    panel4.Visible = false;
                    panel3.Visible = true;
                    break;
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            switch (count)
            {
                case 2:
                    if (TextBoxMethod(ref xBegin, textBox1, errorProvider1) |
                   TextBoxMethod(ref xEnd, textBox2, errorProvider2))
                        return;
                    if (xBegin >= xEnd)
                    {
                        errorProvider1.SetError(textBox1, "Начальное значение x должно быть меньше конечного.");
                        return;
                    }
                    if (xEnd - xBegin < 5 * xStep)
                    {
                        errorProvider6.SetError(listBox1, "При таком интервале необходимо задать шаг поменьше.");
                        return;
                    }
                    break;
                case 3:
                    if (function == Function.F2 && (TextBoxMethod(ref a, textBox3, errorProvider3) |
                    TextBoxMethod(ref b, textBox4, errorProvider4)) ||
                    (function == Function.F1 && TextBoxMethod(ref d, textBox5, errorProvider5)))
                        return;
                    break;
            }
            count++;
            buttonBack.Enabled = count > 1;
            buttonNext.Enabled = count < 4;
            buttonFinish.Enabled = count == 3 || count == 4;
            switch (count)
            {
                case 2:
                    panel1.Visible = false;
                    panel2.Visible = true;
                    break;
                case 3:
                    #region Коэффициенты
                    errorProvider6.Clear();
                    if (function == Function.F1)
                    {
                        label5.Text = "Введите коэффициент d.";
                        label6.Visible = false;
                        textBox3.Visible = false;
                        label7.Visible = false;
                        textBox4.Visible = false;
                        label8.Visible = true;
                        textBox5.Visible = true;
                    }
                    else
                    {
                        label5.Text = "Введите коэффициенты a и b.";
                        label6.Visible = true;
                        textBox3.Visible = true;
                        label7.Visible = true;
                        textBox4.Visible = true;
                        label8.Visible = false;
                        textBox5.Visible = false;
                    }
                    panel2.Visible = false;
                    panel3.Visible = true;
                    break;
                #endregion
                case 4:
                    panel3.Visible = false;
                    panel4.Visible = true;
                    break;
            }
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            if (function == Function.F2 && (TextBoxMethod(ref a, textBox3, errorProvider3) |
            TextBoxMethod(ref b, textBox4, errorProvider4)) ||
            (function == Function.F1 && TextBoxMethod(ref d, textBox5, errorProvider5)))
                return;
            xStep = Convert.ToDouble(listBox1.SelectedItem);
            switch (comboBox1.SelectedText)
            {
                case "Зеленый":
                    graphColor = Color.FromArgb(10, 200, 0);
                    break;
                case "Синий":
                    graphColor = Color.Blue;
                    break;
                case "Золотой":
                    graphColor = Color.FromArgb(240, 200, 0);
                    break;
                case "Маджента":
                    graphColor = Color.FromArgb(240, 0, 240);
                    break;
                case "Черный":
                    graphColor = Color.Black;
                    break;
                default:
                    comboBox1.SelectedIndex = 0;
                    graphColor = Color.FromArgb(240, 0, 0);
                    break;
            }
            switch (function)
            {
                case Function.F1:
                    mainWindow.data = new Data(xBegin, xEnd, xStep, null, null, d, trackBar1.Value, graphColor, comboBox1.SelectedText);
                    break;
                case Function.F2:
                    mainWindow.data = new Data(xBegin, xEnd, xStep, null, null, d, trackBar1.Value, graphColor, comboBox1.SelectedText);
                    break;
            }
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            buttonNext.Enabled = true;
            function = Function.F1;
            textBox3.Clear();
            textBox4.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            buttonNext.Enabled = true;
            function = Function.F2;
            textBox5.Clear();
            errorProvider5.Clear();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label11.Text = (trackBar1.Value - 1).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(ref xBegin, textBox1, errorProvider1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(ref xEnd, textBox2, errorProvider2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(ref a, textBox3, errorProvider3);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(ref b, textBox4, errorProvider4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TextBoxMethod(ref d, textBox5, errorProvider5);
        }


    }
}

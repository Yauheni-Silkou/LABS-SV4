using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SV4_2
{
    public partial class Child : Form
    {
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
        Chart chart; bool overflow;
        private void CreateChart(Control control)
        {
            // Создаём новый элемент управления Chart
            chart = new Chart();
            // Помещаем его на форму
            chart.Parent = control;
            // Задаём размеры элемента
            chart.SetBounds(0, 0, panelGraph.Width, panelGraph.Height);

            // Создаём новую область для построения графика
            ChartArea area = new ChartArea();
            // Даём ей имя (чтобы потом добавлять графики)
            area.Name = "myGraph";
            // Задаём левую и правую границы оси X
            area.AxisX.Minimum = data.XBegin;
            area.AxisX.Maximum = data.XEnd;
            // Определяем шаг сетки
            area.AxisX.MajorGrid.Interval = (data.XEnd - data.XBegin) / 20;
            // Добавляем область в диаграмму
            chart.ChartAreas.Add(area);
            chart.Anchor = control.Anchor;

            // Создаём объект для графика
            Series series1 = new Series();
            // Ссылаемся на область для построения графика
            series1.ChartArea = "myGraph";
            // Задаём тип графика - сплайны
            series1.ChartType = SeriesChartType.Spline;
            // Указываем ширину линии графика
            series1.BorderWidth = 3;
            // Название графика для отображения в легенде
            series1.LegendText = "y(x)";
            // Добавляем в список графиков диаграммы
            chart.Series.Add(series1);

            // Создаём легенду, которая будет показывать названия
            Legend legend = new Legend();
            chart.Legends.Add(legend);

            series1.Color = data.GraphColor;
            //series1.
        }

        Data data;
        double[] x, y;
        public Child(string name, MainWindow mainWindow)
        {
            InitializeComponent();
            Text = name;
            data = new Data(mainWindow.data.XBegin, mainWindow.data.XEnd, mainWindow.data.XStep,
                mainWindow.data.A, mainWindow.data.B, mainWindow.data.D,
                mainWindow.data.Thickness, mainWindow.data.GraphColor, mainWindow.data.ColorName);
            overflow = false;
            listBox1.Visible = panelValues.Visible = /*panelGraph.Visible =*/ false;
        }

        private void Child_Load(object sender, EventArgs e)
        {
            List<ValueTable> valueTable = new List<ValueTable>();
            for (double xValue = data.XBegin; xValue < data.XEnd + 1.0 / 100000000; xValue += data.XStep)
            {
                double yValue;
                if (data.A == null)
                    yValue = Math.Pow(xValue, 4) + Math.Cos(2 + Math.Pow(xValue, 3) - (double)data.D);
                else yValue = 0.01 * ((double)data.A + (double)data.B * xValue) - Math.Pow(Math.E, Math.Pow(xValue, 3) + (double)data.B);
                valueTable.Add(new ValueTable(xValue, yValue));
                listBox1.Items.Add(string.Format("{0,12:0.00000} ----- {1:F3}", xValue, yValue));
            }
            x = new double[valueTable.Count];
            y = new double[valueTable.Count];
            for (int j = 0; j < x.Length; j++)
            {
                if (y[j] >= 3.5e+28 || double.IsPositiveInfinity(y[j]) ||
                    y[j] <= -3.5e+28 || double.IsNegativeInfinity(y[j]))
                {
                    y[j] = double.NaN;
                    overflow = true;
                }
            }
            int i = 0;
            foreach (var value in valueTable)
            {
                x[i] = value.x;
                y[i] = value.y;
                i++;
            }

            if (data.A == null) pictureBox1.Image = SV4_2.Properties.Resources.func1;
            else pictureBox1.Image = SV4_2.Properties.Resources.func2;
            label1.Text = "Начальное значение: " + data.XBegin.ToString() + "\n" +
                "Конечное значение: " + data.XEnd.ToString() + "\n" +
                "Шаг: " + data.XStep.ToString();
            if (data.A != null)
                label2.Text = "A = " + data.A.ToString() + "\n" +
                    "B = " + data.B.ToString() + "\n";
            else
                label2.Text = "D = " + data.D.ToString();
            label3.Text = "Толщина: " + data.Thickness.ToString() + ";\t Цвет: ";

            CreateChart(panelGraph);
            chart.Series[0].Points.DataBindXY(x, y);
            if (overflow) MessageBox.Show("Не все значения удалось отобразить на графике.", "Внимание");
        }
    }
}

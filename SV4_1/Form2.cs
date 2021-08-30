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

namespace SV4_1
{
    public partial class Form2 : Form
    {
        double[] x, y;
        double beg, end, step;
        Chart chart;

        /// <summary>
        /// Создаём элемент управления Chart и настраиваем его
        /// </summary>
        private void CreateChart()
        {
            // Создаём новый элемент управления Chart
            chart = new Chart();
            // Помещаем его на форму
            chart.Parent = this;
            // Задаём размеры элемента
            chart.SetBounds(10, 10, ClientSize.Width - 20, ClientSize.Height - 20);

            // Создаём новую область для построения графика
            ChartArea area = new ChartArea();
            // Даём ей имя (чтобы потом добавлять графики)
            area.Name = "myGraph";
            // Задаём левую и правую границы оси X
            area.AxisX.Minimum = beg;
            area.AxisX.Maximum = end;
            // Определяем шаг сетки
            area.AxisX.MajorGrid.Interval = (end - beg) / 20;
            // Добавляем область в диаграмму
            chart.ChartAreas.Add(area);

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
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CreateChart();
            chart.Series[0].Points.DataBindXY(x, y);
            if (overflow) MessageBox.Show("Не все значения удалось отобразить на графике.", "Внимание");
        }
        bool overflow;
        public Form2(double[] x, double[] y, double beg, double end, double step)
        {
            InitializeComponent();
            this.beg = beg;
            this.end = end;
            this.step = step;
            this.x = x;
            this.y = y;
            overflow = false;
            for (int i = 0; i < x.Length; i++)
            {
                if (y[i] >= 3.5e+28 || double.IsPositiveInfinity(y[i]) ||
                    y[i] <= -3.5e+28 || double.IsNegativeInfinity(y[i]))
                {
                    y[i] = double.NaN;
                    overflow = true;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;

namespace SV4_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int angle = 45;
        double Ox, Oy, Ex, Ey, Ax, Ay, Bx, By, Cx, Cy, Dx, Dy;

        public double AO = 100, AD = 200, CD = 100, CE = 75, DE = 100, BC = 185, OE = 200;

        private void valuesButton_Click(object sender, RoutedEventArgs e)
        {
            ValuesWindow valw = new ValuesWindow(this);
            valw.ShowDialog();
            SetSystem(false);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            VideoWindow vd = new VideoWindow();
            vd.ShowDialog();
        }

        DispatcherTimer timer = new DispatcherTimer();

        bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
            Ox = axisY.X1;
            Oy = axisX.Y1;
            Ex = Ox + OE;
            Ey = Oy;

            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Tick += Timer_Tick;

            SetSystem(false);
        }

        void SetSystem(bool isDecrement)
        {
            if (isDecrement) angle--;
            angle %= 360;

            double alpha = angle * 3.14 / 180.0;

            Ax = Ox + AO * Math.Cos(alpha);
            Ay = Oy + AO * Math.Sin(alpha);

            lineAO.X1 = Ox;
            lineAO.Y1 = Oy;
            lineAO.X2 = Ax;
            lineAO.Y2 = Ay;

            double AE = GetSideUsingLawOfCosines(AO, OE, alpha);
            double phi = GetAngleUsingLawOfCosines(AD, AE, DE);
            double beta = GetAngleUsingLawOfCosines(AO, AE, OE);
            double gamma = Math.PI - beta - phi;

            Dx = Ex + DE * Math.Cos(gamma);
            Dy = Ey + DE * Math.Sin(gamma);

            lineAD.X1 = Ax;
            lineAD.Y1 = Ay;
            lineAD.X2 = Dx;
            lineAD.Y2 = Dy;

            lineDE.X1 = Ex;
            lineDE.Y1 = Ey;
            lineDE.X2 = Dx;
            lineDE.Y2 = Dy;

            double delta = 2 * Math.PI - GetAngleUsingLawOfCosines(CD, CE, DE) + gamma;
            Cx = Ex + CE * Math.Cos(delta);
            Cy = Ey + CE * Math.Sin(delta);

            lineCE.X1 = Ex;
            lineCE.Y1 = Ey;
            lineCE.X2 = Cx;
            lineCE.Y2 = Cy;

            lineCD.X1 = Cx;
            lineCD.Y1 = Cy;
            lineCD.X2 = Dx;
            lineCD.Y2 = Dy;


            double sigma = Math.Acos((Ey - Cy) / (CE));
            double epsilon = GetAngleUsingLawOfSines(BC, CE, sigma);
            double omega = Math.PI - sigma - epsilon;
            double BE = GetSideUsingLawOfCosines(CE, BC, omega);

            Bx = Ex;
            By = Ey - BE;

            lineBC.X1 = Cx;
            lineBC.Y1 = Cy;
            lineBC.X2 = Bx;
            lineBC.Y2 = By;

            block.Margin = new Thickness(Bx - 25, By - 40, 0, 0);
        }

        #region Functions
        double GetSideUsingLawOfCosines(double b, double c, double alpha)
        {
            return Math.Sqrt(Math.Pow(b, 2) + Math.Pow(c, 2) - 2 * b * c * Math.Cos(alpha));
        }
        double GetAngleUsingLawOfSines(double a, double b, double alpha)
        {
            return Math.Asin(b * Math.Sin(alpha) / a);
        }
        double GetAngleUsingLawOfCosines(double a, double b, double c)
        {
            return Math.Acos((Math.Pow(b, 2) + Math.Pow(c, 2) - Math.Pow(a, 2)) / (2 * b * c));
        }
        #endregion

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetSystem(true);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                timer.Start();
                startButton.Content = "Пауза";
            }
            else
            {
                timer.Stop();
                startButton.Content = "Пуск";
            }
            flag = !flag;
        }

        private void axisColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            byte a = colorDialog.Color.A;
            byte r = colorDialog.Color.R;
            byte g = colorDialog.Color.G;
            byte b = colorDialog.Color.B;
            axisX.Stroke = axisY.Stroke = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        private void lineColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            byte a = colorDialog.Color.A;
            byte r = colorDialog.Color.R;
            byte g = colorDialog.Color.G;
            byte b = colorDialog.Color.B;
            lineAO.Stroke = lineAD.Stroke = lineCD.Stroke = lineBC.Stroke = lineCE.Stroke =
                lineDE.Stroke = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        private void blockColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            byte a = colorDialog.Color.A;
            byte r = colorDialog.Color.R;
            byte g = colorDialog.Color.G;
            byte b = colorDialog.Color.B;
            block.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        private void backgroundColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            byte a = colorDialog.Color.A;
            byte r = colorDialog.Color.R;
            byte g = colorDialog.Color.G;
            byte b = colorDialog.Color.B;
            field.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

    }
}

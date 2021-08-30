using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SV4_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int angle = 0;
        bool isGraphVisible = false;

        double top, left;

        public MainWindow()
        {
            polyLine = new Polyline();
            InitializeComponent();
            double a = 10, curX, curY, curX1;
            polyLine.Points.Clear();
            double i = 0.002;
            do
            {
                curX = CurXM(i, a);
                curY = CurYM(i, a);
                curX1 = CurXM(i + 0.002, a);
                polyLine.Points.Add(new Point(curX, curY));
                i += 0.002;
            }
            while (!double.IsNaN(curX1));
            top = Canvas.GetTop(polyLine);
            left = Canvas.GetLeft(polyLine);
            polyLine.Visibility = Visibility.Hidden;

        }

        private void ZoomPlusMenuItem_Click(object sender, RoutedEventArgs e)
        {
            graphArea.Width = 0.8 * graphArea.Width;
            graphArea.Height = 0.8 * graphArea.Height;
        }

        private void ZoomMinusMenuItem_Click(object sender, RoutedEventArgs e)
        {
            graphArea.Width = 1.25 * graphArea.Width;
            graphArea.Height = 1.25 * graphArea.Height;
        }

        private void ShiftUpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetTop(polyLine, Canvas.GetTop(polyLine) - 10);
        }

        private void ShiftDownMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetTop(polyLine, Canvas.GetTop(polyLine) + 10);
        }

        private void ShiftLeftMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(polyLine, Canvas.GetLeft(polyLine) - 10);
        }

        private void ShiftRightMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(polyLine, Canvas.GetLeft(polyLine) + 10);
        }

        private void ShiftDefaultMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetTop(polyLine, top);
            Canvas.SetLeft(polyLine, left);
        }

        private void TurnClockwiseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotate = new RotateTransform(angle + 5);
            polyLine.RenderTransform = rotate;
            angle = angle + 5;
        }

        private void TurnCounterclockwiseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotate = new RotateTransform(angle - 5);
            polyLine.RenderTransform = rotate;
            angle = angle - 5;
        }

        private void TurnDefaultMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rt = new RotateTransform();
            polyLine.RenderTransform = rt;
            angle = 0;
            rt.Freeze();
        }

        private void AxisRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            X.Opacity = 1;
            Y.Opacity = 1;
            o.Opacity = 1;
            osX.Opacity = 1;
            osY.Opacity = 1;
        }

        private void AxisRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            X.Opacity = 0;
            Y.Opacity = 0;
            o.Opacity = 0;
            osX.Opacity = 0;
            osY.Opacity = 0;
        }

        private void ShowOrHideGraphButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGraphVisible)
            {
                polyLine.Visibility = Visibility.Hidden;
                showOrHideGraphTextBlock.Text = "Показать график";
                ChangeButtonBackgroundColor(snhGraphButtonBgColor, 0x13, 0xBF, 0x23, 0x00, 0x74, 0x16);
            }
            else
            {
                polyLine.Visibility = Visibility.Visible;
                showOrHideGraphTextBlock.Text = "Скрыть график";
                ChangeButtonBackgroundColor(snhGraphButtonBgColor, 0xBF, 0x13, 0x23, 0x74, 0x00, 0x16);
            }
            isGraphVisible = !isGraphVisible;
            shiftMenuItem.IsEnabled = turnMenuItem.IsEnabled = graphColorMenuItem.IsEnabled
                = thicknessMenuItem.IsEnabled = zoomMenuItem.IsEnabled = isGraphVisible;
        }

        private void GraphColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            polyLine.Stroke = ((MenuItem)sender).Foreground;
        }

        private void BackgroundColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            graphArea.Background = ((MenuItem)sender).Background;
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            thicknessMenuItem.Header = "Толщина графика: " + thicknessSlider.Value.ToString();
            polyLine.StrokeThickness = thicknessSlider.Value;
        }

        double CurXM(double i, double a)
        {
            return a * (Math.Cos(Math.Tan(i)) + Math.Log(Math.Tan(Math.Tan(i) / 2)));
        }

        double CurYM(double i, double a)
        {
            return a * Math.Sin(Math.Tan(i));
        }

        private void BlockArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("");
        }

        void ChangeButtonBackgroundColor(Shape rect, byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
        {
            LinearGradientBrush lgb = new LinearGradientBrush();
            GradientStop x1 = new GradientStop();
            GradientStop x2 = new GradientStop();
            x1.Offset = 0; x2.Offset = 1;
            x1.Color = Color.FromRgb(r1, g1, b1);
            x2.Color = Color.FromRgb(r2, g2, b2);
            lgb.GradientStops.Add(x1);
            lgb.GradientStops.Add(x2);
            rect.Fill = lgb;
        }
    }
}

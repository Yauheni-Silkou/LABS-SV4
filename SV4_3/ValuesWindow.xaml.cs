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
using System.Windows.Shapes;

namespace SV4_3
{
    /// <summary>
    /// Логика взаимодействия для ValuesWindow.xaml
    /// </summary>
    public partial class ValuesWindow : Window
    {
        MainWindow mw;
        public ValuesWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            mw = mainWindow;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            mw.AO = Convert.ToInt32(ao.Text);
            mw.AD = Convert.ToInt32(ad.Text);
            mw.CE = Convert.ToInt32(ce.Text);
            mw.DE = Convert.ToInt32(de.Text);
            mw.CD = Convert.ToInt32(cd.Text);
            mw.BC = Convert.ToInt32(bc.Text);
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ao.Text = mw.AO.ToString();
            ad.Text = mw.AD.ToString();
            ce.Text = mw.CE.ToString();
            de.Text = mw.DE.ToString();
            cd.Text = mw.CD.ToString();
            bc.Text = mw.BC.ToString();
        }
    }
}

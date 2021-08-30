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
    public partial class MainWindow : Form
    {
        private int openDocuments;

        public Data data;

        public enum Mode
        {
            Graph, Table, Values
        }

        public MainWindow()
        {
            InitializeComponent();
            openDocuments = 0;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Wizard wizard = new Wizard(this);
            wizard.ShowDialog();
            Child child = new Child("Файл " + (++openDocuments).ToString(), this);
            child.MdiParent = this;
            child.Show();
        }
    }
}

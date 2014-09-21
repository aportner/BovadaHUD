using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace BovadaHUD
{
    public partial class Form1 : Form
    {
        TableFinder finder;
        Table table;

        public Form1()
        {
            InitializeComponent();

            finder = new TableFinder();
            table = finder.FindTable();

            if ( table == null )
            {
                Console.WriteLine("Could not find table.");
                return;
            }

            while ( true )
            {
                table.update();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Thread.Sleep(500);
            }
        }
    }
}

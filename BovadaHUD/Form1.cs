using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Tesseract;

namespace BovadaHUD
{
    public partial class Form1 : Form
    {
        private TableFinder finder;
        private Table table;

        System.Timers.Timer tableTimer;

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

            tableTimer = new System.Timers.Timer(500);
            tableTimer.Elapsed += OnTimedEvent;
            tableTimer.Start();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            table.update();

            Invoke((MethodInvoker)delegate
            {
                for ( int i = 0; i < table.Seats.Count; ++i )
                {
                    Console.WriteLine(i + ": " + table.Seats[i].Stack);
                }
            });
        }
    }
}

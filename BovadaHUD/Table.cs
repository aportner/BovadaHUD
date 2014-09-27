using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BovadaHUD
{
    class Table
    {
        private IntPtr hWnd;

        private TableCapture capture;

        private TableConfig config;

        private StackFont stackFont;

        private IList<Seat> seats;


        public Table ( IntPtr hWnd )
        {
            this.hWnd = hWnd;

            stackFont = new StackFont();
            config = new TableConfig();
            seats = new List<Seat>();

            for (int i = 0; i < 9; ++i )
            {
                seats.Add( new Seat( i, config, stackFont ) );
            }

            capture = new TableCapture(hWnd);
        }


        public IList<Seat> Seats
        {
            get
            {
                return seats;
            }
        }


        public void update()
        {
            Bitmap bitmap = capture.capture();

            foreach( Seat seat in seats )
            {
                seat.Process(bitmap);
            }

            bitmap.Dispose();
        }
    }
}

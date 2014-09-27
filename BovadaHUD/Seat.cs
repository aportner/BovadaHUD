using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BovadaHUD
{
    class Seat
    {
        private int index;

        private int stack;

        private TableConfig config;

        private StackFont stackFont;


        public Seat( int index, TableConfig config, StackFont stackFont )
        {
            this.index = index;
            this.config = config;
            this.stackFont = stackFont;
        }


        public int Stack
        {
            get { return stack; }
        }

        public void Process( Bitmap bmp )
        {
            RECT choppedStack = BitmapUtils.Crop(bmp, config.Stacks[index], 0xd0);

            IList<RECT> rectsStack = BitmapUtils.Chop( bmp, choppedStack, 0xd0);
            StringBuilder builder = new StringBuilder();

            int i = 0;

            foreach( RECT rectStack in rectsStack )
            {
                char letter = stackFont.GetCharacter( bmp, rectStack );

                if ( letter != '\0' && letter != '.' && letter != ',' && letter != '$' )
                {
                    builder.Append(letter);
                }

                if ( i >= 10 )
                {
                    break;
                }
            }

            // Console.WriteLine("{0} {1}", index, builder.ToString());

            if ( i < 10 && builder.Length > 0 )
            {
                try
                {
                    stack = Convert.ToInt32(builder.ToString());
                }
                catch (Exception e) {
                    stack = 0;
                }
            }
            else
            {
                stack = 0;
            }
        }
    }
}

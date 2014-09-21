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

        public void Process( Bitmap bmp )
        {
            Bitmap choppedStack = BitmapUtils.Crop(bmp, config.Stacks[index], 0xd0);
            IList<Bitmap> bmpsStack = BitmapUtils.Chop(choppedStack, 0xd0);
            StringBuilder builder = new StringBuilder();

            foreach( Bitmap bmpStack in bmpsStack )
            {
                char letter = stackFont.GetCharacter(bmpStack);

                if ( letter != '\0' && letter != '.' && letter != ',' && letter != '$' )
                {
                    builder.Append(letter);
                }

                bmpStack.Dispose();
            }

            Console.WriteLine("{0} {1}", index, builder.ToString());

            choppedStack.Dispose();
        }
    }
}

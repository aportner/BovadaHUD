using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BovadaHUD
{
    class StackFont
    {
        private Bitmap font;

        private IList<Bitmap> bitmaps;

        private char[] characters;


        public StackFont()
        {
            font = new Bitmap("stackfont.png");
            bitmaps = BitmapUtils.Chop(font, 0xd0);
            characters = new char[]{ '$', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            /*
            for (int i = 0; i < bitmaps.Count; ++i)
            {
                Bitmap bmpChop = bitmaps.ElementAt(i);

                StringBuilder builder = new StringBuilder("C:\\users\\Andrew\\Downloads\\stackfont");
                builder.Append(i);
                builder.Append(".jpg");

                bmpChop.Save(builder.ToString());
            }*/
        }


        public char GetCharacter( Bitmap bmp )
        {
            char result = '\0';
            int maxCertainty = 0;

            for (int i = 0; i < bitmaps.Count; ++i )
            {
                int certainty = Compare(bmp, bitmaps.ElementAt(i), 0xd0);

                if ( certainty > 90 && certainty > maxCertainty )
                {
                    maxCertainty = certainty;
                    result = characters[i];
                }
            }
               
            return result;
        }

        public int Compare( Bitmap bmp, Bitmap bmpFont, byte color )
        {
            int result = 0;

            for (int x = 0; x < bmp.Size.Width; ++x )
            {
                for ( int y = 0; y < bmp.Size.Height; ++y )
                {
                    Color cBmp = bmp.GetPixel(x, y);
                    Color cFont = Color.Black;

                    if ( x < bmpFont.Size.Width && y < bmpFont.Size.Height )
                    {
                        cFont = bmpFont.GetPixel(x, y);
                    }

                    if ( cBmp.R > color && cFont.R > color || cBmp.R < color && cFont.R < color )
                    {
                        ++result;
                    }
                }
            }

            // Console.WriteLine("{0} {1}", result, bmp.Size.Width * bmp.Size.Height );

            return result * 100 / ( bmp.Size.Width * bmp.Size.Height );
        }
    }
}

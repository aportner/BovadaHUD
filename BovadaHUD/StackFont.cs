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

        private IList<RECT> rects;

        private char[] characters;


        public StackFont()
        {
            font = new Bitmap("stackfont.png");
            rects = BitmapUtils.Chop(font, new RECT( 0, 0, font.Size.Width, font.Size.Height ), 0xd0);
            characters = new char[]{ '$', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            /*
            for (int i = 0; i < rects.Count; ++i)
            {
                RECT rectChop = rects[i];

                Bitmap bmpChop = font.Clone(rectChop, font.PixelFormat);
                bmpChop.Save("C:\\users\\Andrew\\Downloads\\stackfont" + i + ".png");
                bmpChop.Dispose();
            }
             */
        }


        public char GetCharacter( Bitmap bmp, RECT rect )
        {
            char result = '\0';
            int maxCertainty = 0;

            for (int i = 0; i < rects.Count; ++i )
            {
                int certainty = Compare( bmp, rect, font, rects[ i ], 0xd0);

                if ( certainty > 90 && certainty > maxCertainty )
                {
                    maxCertainty = certainty;
                    result = characters[i];
                }
            }
               
            return result;
        }

        public int Compare( Bitmap bmp, RECT rect, Bitmap bmpFont, RECT rectFont, byte color )
        {
            int result = 0;

            for (int x = 0; x < rect.Width; ++x )
            {
                for ( int y = 0; y < rect.Height; ++y )
                {
                    Color cBmp = bmp.GetPixel( rect.Left + x, rect.Top + y);
                    Color cFont = Color.Black;

                    if ( x < rectFont.Width && y < rectFont.Height )
                    {
                        cFont = bmpFont.GetPixel( rectFont.Left + x, rectFont.Top + y);
                    }

                    if ( cBmp.R > color && cFont.R > color || cBmp.R < color && cFont.R < color )
                    {
                        ++result;
                    }
                }
            }

            return result * 100 / ( rect.Width * rect.Height );
        }
    }
}

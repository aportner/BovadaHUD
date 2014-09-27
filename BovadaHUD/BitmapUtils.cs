using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BovadaHUD
{
    class BitmapUtils
    {
        public static RECT Crop( Bitmap bmp, RECT rect, byte color )
        {
            RECT reverse = new RECT(rect.Right, rect.Bottom, rect.Left, rect.Top);

            for( int x = rect.Left; x < rect.Right; ++x )
            {
                for ( int y = rect.Top; y < rect.Bottom; ++y )
                {
                    Color pixel = bmp.GetPixel(x, y);

                    if ( pixel.R >= color )
                    {
                        if ( x < reverse.Left )
                        {
                            reverse.Left = x;
                        }

                        if ( x >= reverse.Right )
                        {
                            reverse.Right = x + 1;
                        }

                        if ( y < reverse.Top )
                        {
                            reverse.Top = y;
                        }

                        if ( y >= reverse.Bottom )
                        {
                            reverse.Bottom = y + 1;
                        }
                    }
                }
            }

            return reverse;
        }

        public static IList<RECT> Chop( Bitmap bmp, RECT crop, byte color )
        {
            List<RECT> list = new List<RECT>();
            RECT rect = new RECT(crop.Left, crop.Top, crop.Right, crop.Bottom);

            Boolean found = false;
            Boolean oldFound = false;

            for (int x = crop.Left; x < crop.Right; ++x )
            {
                found = false;

                for ( int y = crop.Top; y < crop.Bottom; ++y )
                {
                    found = found || bmp.GetPixel(x, y).R >= color;
                }

                if ( found && !oldFound )
                {
                    rect.Left = x;
                }
                else if ( !found && oldFound )
                {
                    rect.Right = x;
                    list.Add( Crop( bmp, rect, color ) );
                }

                oldFound = found;
            }

            if ( found )
            {
                rect.Right = crop.Right;
                list.Add(Crop(bmp, rect, color));
            }
            
            return list;
        }
    }
}

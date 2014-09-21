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
        public static Bitmap Crop( Bitmap bmp, RECT rect, byte color )
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

            Bitmap bitmap = bmp.Clone(reverse, bmp.PixelFormat);
            return bitmap;
        }

        public static IList<Bitmap> Chop( Bitmap bmp, byte color )
        {
            List<Bitmap> list = new List<Bitmap>();
            RECT rect = new RECT(0, 0, bmp.Size.Width, bmp.Size.Height);

            Boolean found = false;
            Boolean oldFound = false;

            for (int x = 0; x < bmp.Size.Width; ++x )
            {
                found = false;

                for ( int y = 0; y < bmp.Size.Height; ++y )
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
                rect.Right = bmp.Size.Width;
                list.Add(Crop(bmp, rect, color));
            }
            
            return list;
        }
    }
}

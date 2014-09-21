using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BovadaHUD
{
    class TableConfig
    {
        private IList<RECT> stacks;

        public TableConfig()
        {
            stacks = new List<RECT>();

            stacks.Add( new RECT( 440, 115, 535, 130 ) );
            stacks.Add( new RECT( 635, 205, 730, 220 ) );
            stacks.Add( new RECT( 645, 340, 740, 355 ) );
            stacks.Add( new RECT( 540, 460, 635, 475 ) );
            stacks.Add( new RECT( 335, 475, 430, 490 ) );
            stacks.Add( new RECT( 175, 460, 265, 475 ) );
            stacks.Add( new RECT( 70, 340, 160, 355 ) );
            stacks.Add( new RECT( 80, 205, 165, 220 ) );
            stacks.Add( new RECT( 265, 115, 355, 130 ) );
        }


        public IList<RECT> Stacks
        {
            get
            {
                return stacks;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PACT.COMMON
{
    public class MenuItemData : SplitButtonData
    {
        public MenuItemData()
            : this(false)
        {
        }

        public MenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}

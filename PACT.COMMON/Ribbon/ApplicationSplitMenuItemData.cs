﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PACT.COMMON
{
    public class ApplicationSplitMenuItemData : SplitMenuItemData
    {
        public ApplicationSplitMenuItemData()
            : this(false)
        {
        }

        public ApplicationSplitMenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}

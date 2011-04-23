using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PACT.COMMON
{
    public class PactButtonData : PactControlData
    {
        public string DynamicCommand
        {
            get
            {
                return _DynamicCommand;
            }

            set
            {
                if (_DynamicCommand != value)
                {
                    _DynamicCommand = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DynamicCommand"));
                }
            }
        }
        private string _DynamicCommand;

    }
   
}

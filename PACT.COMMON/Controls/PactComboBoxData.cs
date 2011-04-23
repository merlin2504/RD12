using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PACT.COMMON
{
    public class PactComboBoxData : PactControlData
    {
        public List<string> ComboItems
        {
            get
            {

                if (_items == null)
                {
                    _items = new List<string>();
                }
               return _items;

            }

            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ComboItems"));
                }
            }
        }
        private List<string> _items;

        public string IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        private string _isSelected;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PACT.COMMON
{
    public class PactComboBoxData : PactControlData,IDataErrorInfo
    {
        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        public string[] ValidatedProperties { get; set; }

        string GetValidationError(string propertyName)
        {
            BusinessRules br = new BusinessRules();
            string error = null;
            switch (propertyName)
            {
                case "IsSelected":
                    Foreground = "Black";
                    BorderThickness = "5";
                    BorderBrush = "Black";
                    if (this.Mandatory.Equals("1"))
                    {
                        if (br.IsStringMissing(this.IsSelected))
                        {
                            BorderBrush = "Red";
                            ToolTip = "Cannot be blank";
                            BorderThickness = "10";
                        }
                        else
                        {
                            BorderBrush = "Black";
                            ToolTip = this.Label;
                        }
                    }
                    else
                    {
                        ToolTip = this.Label;
                        BorderBrush = "Black";
                    }
                    break;
                case "Background":
                    if (this.Mandatory.Equals("1"))
                    {
                        Background = "Cyan";
                    }
                    else
                    {
                        Background = "White";
                    }
                    break;
            }

            return error;
        }

        public List<ComboBoxItemData> ComboItems
        {
            get
            {

                if (_items == null)
                {
                    _items = new List<ComboBoxItemData>();
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
        private List<ComboBoxItemData> _items;

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

    public class ComboBoxItemData
    {
        public string ItemDisplayName { get; set; }
        public string ItemValue { get; set; }
        public ComboBoxItemData(string itemNm, string itemVal)
        {
            ItemDisplayName = itemNm;
            ItemValue=itemVal;
        }
    }
}

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
                case "SelectedValue":
                    Foreground = "Black";
                    BorderThickness = "1";
                    BorderBrush = "Black";
                    if (this.Mandatory)
                    {
                        if (br.IsStringMissing(this.SelectedValue))
                        {
                            BorderBrush = "Red";
                            ToolTip = "Cannot be blank";
                            BorderThickness = "2";
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
                    if (this.Mandatory)
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
        public string SelectedValue
        {
            get
            {
                return _selectedvalue;
            }

            set
            {
                if (_selectedvalue != value)
                {
                    _selectedvalue = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("SelectedValue"));
                }
            }
        }
        private string _selectedvalue;

        public int FeatureID
        {
            get
            {
                return _featureid;
            }

            set
            {
                if (_featureid != value)
                {
                    _featureid = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FeatureID"));
                }
            }
        }
        private int _featureid;

        public int CompanyIndex
        {
            get
            {
                return _companyindex;
            }

            set
            {
                if (_companyindex != value)
                {
                    _companyindex = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CompanyIndex"));
                }
            }
        }
        private int _companyindex;


        public bool IsPartiralData
        {
            get
            {
                return _ispartiraldata;
            }

            set
            {
                if (_ispartiraldata != value)
                {
                    _ispartiraldata = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsPartiralData"));
                }
            }
        }
        private bool _ispartiraldata;
    } 
     
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace PACT.COMMON
{
    public class PactControlData : INotifyPropertyChanged
    {
       
        public string ToolTip
        {
            get
            {
                return _tooltip;
            }

            set
            {
                if (_tooltip != value)
                {
                    _tooltip = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTip"));
                }
            }
        }
        private string _tooltip;


        
        public string Foreground
        {
            get
            {
                return _foreground;
            }

            set
            {
                if (_foreground != value)
                {
                    _foreground = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Foreground"));
                }
            }
        }
        private string _foreground;

        public string Background
        {
            get
            {
                return _background;
            }

            set
            {
                if (_background != value)
                {
                    _background = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Background"));
                }
            }
        }
        private string _background;

        public string BorderBrush
        {
            get
            {
                return _borderbrush;
            }

            set
            {
                if (_borderbrush != value)
                {
                    _borderbrush = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("BorderBrush"));
                }
            }
        }
        private string _borderbrush;

        public string BorderThickness
        {
            get
            {
                return _borderthickness;
            }

            set
            {
                if (_borderthickness != value)
                {
                    _borderthickness = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("BorderThickness"));
                }
            }
        }
        private string _borderthickness;      


        public string DBColumnName
        {
            get
            {
                return _dbcolNm;
            }

            set
            {
                if (_dbcolNm != value)
                {
                    _dbcolNm = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DBColumnName"));
                }
            }
        }
        private string _dbcolNm;

        public string DataType
        {
            get
            {
                return _datatype;
            }

            set
            {
                if (_datatype != value)
                {
                    _datatype = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DataType"));
                }
            }
        }
        private string _datatype;

        public string Mandatory
        {
            get
            {
                return _mandatory;
            }

            set
            {
                if (_mandatory != value)
                {
                    _mandatory = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Mandatory"));
                }
            }
        }
        private string _mandatory;

        public string Align
        {
            get
            {
                return _align;
            }

            set
            {
                if (_align != value)
                {
                    _align = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Align"));
                }
            }
        }
        private string _align;

        public string Height
        {
            get
            {
                return _height;
            }

            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Height"));
                }
            }
        }
        private string _height;


        public string Width
        {
            get
            {
                return _width;
            }

            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Width"));
                }
            }
        }
        private string _width;

        public string Enable
        {
            get
            {
                return _enable;
            }

            set
            {
                if (_enable != value)
                {
                    _enable = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Enable"));
                }
            }
        }
        private string _enable;

        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Label"));
                }
            }
        }
        private string _label;

        public string Left
        {
            get
            {
                return _left;
            }

            set
            {
                if (_left != value)
                {
                    _left = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Left"));
                }
            }
        }
        private string _left;

        public string Top
        {
            get
            {
                return _top;
            }

            set
            {
                if (_top != value)
                {
                    _top = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Top"));
                }
            }
        }
        private string _top;

        public string Style
        {
            get
            {
                return _style;
            }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Style"));
                }
            }
        }
        private string _style;


        public Uri LargeImage
        {
            get
            {
                return _largeImage;
            }

            set
            {
                if (_largeImage != value)
                {
                    _largeImage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LargeImage"));
                }
            }
        }
        private Uri _largeImage;

        public Uri SmallImage
        {
            get
            {
                return _smallImage;
            }

            set
            {
                if (_smallImage != value)
                {
                    _smallImage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("SmallImage"));
                }
            }
        }
        private Uri _smallImage;

        public string ToolTipTitle
        {
            get
            {
                return _toolTipTitle;
            }

            set
            {
                if (_toolTipTitle != value)
                {
                    _toolTipTitle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTipTitle"));
                }
            }
        }
        private string _toolTipTitle;

        public string ToolTipDescription
        {
            get
            {
                return _toolTipDescription;
            }

            set
            {
                if (_toolTipDescription != value)
                {
                    _toolTipDescription = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTipDescription"));
                }
            }
        }
        private string _toolTipDescription;

        public Uri ToolTipImage
        {
            get
            {
                return _toolTipImage;
            }

            set
            {
                if (_toolTipImage != value)
                {
                    _toolTipImage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTipImage"));
                }
            }
        }
        private Uri _toolTipImage;

        public string ToolTipFooterTitle
        {
            get
            {
                return _toolTipFooterTitle;
            }

            set
            {
                if (_toolTipFooterTitle != value)
                {
                    _toolTipFooterTitle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTipFooterTitle"));
                }
            }
        }
        private string _toolTipFooterTitle;

        public string ToolTipFooterDescription
        {
            get
            {
                return _toolTipFooterDescription;
            }

            set
            {
                if (_toolTipFooterDescription != value)
                {
                    _toolTipFooterDescription = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTipFooterDescription"));
                }
            }
        }
        private string _toolTipFooterDescription;

        public Uri ToolTipFooterImage
        {
            get
            {
                return _toolTipFooterImage;
            }

            set
            {
                if (_toolTipFooterImage != value)
                {
                    _toolTipFooterImage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ToolTipFooterImage"));
                }
            }
        }
        private Uri _toolTipFooterImage;

        public ICommand Command
        {
            get
            {
                return _command;
            }

            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Command"));
                }
            }
        }
        private ICommand _command;

        public string KeyTip
        {
            get
            {
                return _keyTip;
            }

            set
            {
                if (_keyTip != value)
                {
                    _keyTip = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KeyTip"));
                }
            }
        }
        private string _keyTip;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }

}

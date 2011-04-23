using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace PACT.COMMON
{
    public class MenuButtonData : ControlData
    {
        public MenuButtonData()
            : this(false)
        {
        }

        public MenuButtonData(bool isApplicationMenu)
        {
            _isApplicationMenu = isApplicationMenu;
        }

        public bool IsVerticallyResizable
        {
            get
            {
                return _isVerticallyResizable;
            }

            set
            {
                if (_isVerticallyResizable != value)
                {
                    _isVerticallyResizable = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsVerticallyResizable"));
                }
            }
        }

        public bool IsHorizontallyResizable
        {
            get
            {
                return _isHorizontallyResizable;
            }

            set
            {
                if (_isHorizontallyResizable != value)
                {
                    _isHorizontallyResizable = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsHorizontallyResizable"));
                }
            }
        }

        private bool _isVerticallyResizable, _isHorizontallyResizable;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<ControlData> ControlDataCollection
        {
            get
            {
                if (_controlDataCollection == null)
                {
                    _controlDataCollection = new ObservableCollection<ControlData>();
                }
                return _controlDataCollection;
            }
        }
        private ObservableCollection<ControlData> _controlDataCollection;
        private int _nestingDepth;
        private bool _isApplicationMenu;
    }
}

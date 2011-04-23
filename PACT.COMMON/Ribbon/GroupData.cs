using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PACT.COMMON
{
    public class GroupData : ControlData
    {
        public GroupData(string header)
        {
            Label = header;
        }

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

    }
}

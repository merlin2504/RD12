using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PACT.COMMON;

namespace PACT.MODEL
{
    public class PactControlDataCollection: INotifyPropertyChanged
    {
        public PactControlDataCollection()
        {
        }
        public ObservableCollection<PactControlData> PACTControlData
        {
            get
            {
                if (_controlDataCollection == null)
                {
                    _controlDataCollection = new ObservableCollection<PactControlData>();

                    //_controlDataCollection.Add(new PactTextBlockData()
                    //{
                    //    Text = "Code",
                    //    Left = "20",
                    //    Top = "30",
                    //    Width = "100",
                    //    Align = "Left"
                    //});
                    //_controlDataCollection.Add(new PactTextBoxData()
                    //{
                    //    Label = "ABC",
                    //    Left = "100",
                    //    Top = "30",
                    //    Width = "200",
                    //    Enable = true,

                    //});

                    //_controlDataCollection.Add(new PactTextBlockData()
                    // {
                    //     Text = "Name",
                    //     Left = "330",
                    //     Top = "30",
                    //     Width = "100",
                    //     Align = "Right"
                    // });

                    //_controlDataCollection.Add(new PactTextBoxData()
                    //{
                    //    Label = "123",
                    //    Left = "380",
                    //    Top = "30",
                    //    Width = "200",
                    //    Enable = false,

                    //});

                    //_controlDataCollection.Add(new PactTextBlockData()
                    //{
                    //    Text = "Status",
                    //    Left = "20",
                    //    Top = "60",
                    //    Width = "100",
                    //    Align = "Right"
                    //});

                    //PactComboBoxData CMB = new PactComboBoxData();
                    ////CMB.ComboItems.Add("Majeed");
                    ////CMB.ComboItems.Add("Majeed1");
                    ////CMB.ComboItems.Add("Majeed2");
                    //CMB.Left = "100";
                    //CMB.Top = "60";
                    //CMB.Width = "200";
                    //CMB.Enable = "True";
                    //_controlDataCollection.Add(CMB);

                    //_controlDataCollection.Add(new PactButtonData()
                    //{
                    //    Label = "Save",
                    //    Command = new RelayCommand(param => OnSaveRequest()),
                    //    Left = "20",
                    //    Top = "90",
                    //    Width = "100",
                    //    Align = "Right",
                    //    Height = "25",
                    //});
                }
                return _controlDataCollection;
            }
        }
        private ObservableCollection<PactControlData> _controlDataCollection;

        void OnSaveRequest()
        {
            PactTextBoxData a = (PactTextBoxData)_controlDataCollection[1];
            string strval = a.Text;
        }

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

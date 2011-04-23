using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls.Primitives;
//using PACT.MODEL; //Not Using Currently


namespace PACT.VIEWMODEL
{
    /// <summary>
    /// Interaction logic for SampleView.xaml
    /// </summary>
    public partial class DefaultScreenView : INotifyPropertyChanged
    {
        //public ObservableCollection<ControlDataCollection> PACTControlCollection
        //{
        //    get
        //    {
        //        if (_PACTControlCollection == null)
        //        {
        //            _PACTControlCollection = new ObservableCollection<ControlDataCollection>();

        //            _PACTControlCollection.Insert(0, new ControlDataCollection());

                    
        //        }
        //        return _PACTControlCollection;
        //    }
        //}
        //private ObservableCollection<ControlDataCollection> _PACTControlCollection;
            
            

        //public List<string> Items = new List<string>();

        
        //public string Header
        //{
        //    get
        //    {
        //        return _header;
        //    }

        //    set
        //    {
        //        if (_header != value)
        //        {
        //            _header = value;
        //            OnPropertyChanged(new PropertyChangedEventArgs("Header"));
        //        }
        //    }
        //}
        //private string _header;


        //public string Value
        //{
        //    get
        //    {
        //        return _value;
        //    }

        //    set
        //    {
        //        if (_value != value)
        //        {
        //            _value = value;
        //            OnPropertyChanged(new PropertyChangedEventArgs("Value"));
        //        }
        //    }
        //}
        //private string _value;


        //public List<string> Items
        //{
        //    get
        //    {
        //        _items = new List<string>();
        //        _items.Add("Majeed");
        //        _items.Add("Majeed1");
        //        _items.Add("Majeed2");


        //        return _items;
        //    }

        //    set
        //    {
        //        if (_items != value)
        //        {
        //            _items = value;
        //            OnPropertyChanged(new PropertyChangedEventArgs("Items"));
        //        }
        //    }
        //}
        //public List<string> _items;


        public DefaultScreenView()
        {
            //Header = "PACT HEADER";
            //Value = "PACT ROCKS!!!!";
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

    public class ControlDataCollection : INotifyPropertyChanged
    {

        public ControlDataCollection(string ScreenID)
        {
            
        }

        //public ObservableCollection<ControlData> PACTControlData
        //{
        //    get
        //    {



        //        if (_controlDataCollection == null)
        //        {
        //            _controlDataCollection = new ObservableCollection<ControlData>();

        //            _controlDataCollection.Add(new TextBoxData()
        //            {
        //                Label = "ABC",
        //                //ToolTipTitle = "ToolTip Title",
        //                //ToolTipDescription = "ToolTip Description",
        //                //Command = ViewModelData.DefaultCommand
        //            });

        //            _controlDataCollection.Add(new TextBoxData()
        //            {
        //                Label = "123",
        //                //ToolTipTitle = "ToolTip Title",
        //                //ToolTipDescription = "ToolTip Description",
        //                //Command = ViewModelData.DefaultCommand
        //            });

        //            //List<string> _items = new List<string>();
        //            //_items.Add("Majeed");
        //            //_items.Add("Majeed1");
        //            //_items.Add("Majeed2");

        //            ComboBoxData CMB = new ComboBoxData();
        //            CMB.ComboItems.Add("Majeed");
        //            CMB.ComboItems.Add("Majeed1");
        //            CMB.ComboItems.Add("Majeed2");

        //            _controlDataCollection.Add(CMB);

        //        }
        //        return _controlDataCollection;
        //    }
        //}


        //private ObservableCollection<ControlData> _controlDataCollection;

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

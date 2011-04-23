//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel;
//using System.Collections.ObjectModel;
//using Microsoft.Windows.Controls.Ribbon;
//using System.Windows;
//using System.Windows.Input;

//namespace PACT.MUKVIEWMODEL
//{
//    public class MUKRibbonTABS : INotifyPropertyChanged
//    {
//        public ObservableCollection<RibbonTab> Tabs
//        {
//            get
//            {
//                _tabs = new ObservableCollection<RibbonTab>()
//                {
//                    CreateR()
//                    //new RibbonTab(){Header="Test1", KeyTip="A"},
//                    //new RibbonTab(){Header="Test2", KeyTip="B"},
//                    //new RibbonTab(){Header="Test3", KeyTip="C"}
//                };
//                return _tabs;
//            }
//        }

//        RibbonTab CreateR()
//        {
//            RibbonTab tab1 = new RibbonTab
//            {
//            Header = "First",
//            };


//            RibbonGroup group1 = new RibbonGroup
//            {
//                Header = "Group1",
//            };

//            Style s = new Style(typeof(RibbonButton));
//            s.Setters.Add(new Setter
//            {
//                Property = RibbonControlService.LabelProperty,
//                Value = "Button1"
//            });

//            RibbonButton objRibbonButton = new RibbonButton
//            {
//                //Foreground = objFontBrush,
//                //Height = MENU_ITEM_HEIGHT,
//                Command = MukRibViewModel.DefaultCommand,

//                Style = s,
//                FlowDirection =
//                FlowDirection.LeftToRight
//            };

//            group1.Items.Add(objRibbonButton);
//            tab1.Items.Add(group1);

//            return tab1;
//        }



//        private ObservableCollection<RibbonTab> _tabs;
//        #region INotifyPropertyChanged Members

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, e);
//            }
//        }

//        #endregion
//    }
//}

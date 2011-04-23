using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;

namespace PACT.VIEWMODEL
{
    public class RibbonData : INotifyPropertyChanged
    {
        public static DelegateCommand<string> cmdLoadPage { get; set; }

        public RibbonData()
        {
            cmdLoadPage = new DelegateCommand<string>(OnLoadPage);
        }


        void OnLoadPage(string obj)
        {
            //TabItem tabitemobj = new TabItem();
            //tabitemobj.Header = obj;
            //tabitemobj.Content = obj;
            //tabitemobj.IsSelected = true;
            //Screens.Add(tabitemobj);
        }

        public ObservableCollection<TabData> TabDataCollection
        {
            get
            {
                if (_tabDataCollection == null)
                {
                    _tabDataCollection = new ObservableCollection<TabData>();
                    for (int i = 2, j = 0; i > 0; i--, j--)
                    {
                        if (j > 0)
                        {
                            //string contextualTabGroupHeader = ContextualTabGroupDataCollection[j - 1].Header;
                            //_tabDataCollection.Insert(0, new TabData("Tab " + i) { ContextualTabGroupHeader = contextualTabGroupHeader });
                        }
                        else
                        {
                            _tabDataCollection.Insert(0, new TabData("Tab " + i));
                        }
                    }
                }
                return _tabDataCollection;
            }
        }
        private ObservableCollection<TabData> _tabDataCollection;

        //public ObservableCollection<ContextualTabGroupData> ContextualTabGroupDataCollection
        //{
        //    get
        //    {
        //        if (_contextualTabGroupDataCollection == null)
        //        {
        //            _contextualTabGroupDataCollection = new ObservableCollection<ContextualTabGroupData>();
        //            for (int i = 0; i < ViewModelData.ContextualTabGroupCount; i++)
        //            {
        //                _contextualTabGroupDataCollection.Add(new ContextualTabGroupData("Grp " + i) { IsVisible = true });
        //            }
        //        }
        //        return _contextualTabGroupDataCollection;
        //    }
        //}
        //private ObservableCollection<ContextualTabGroupData> _contextualTabGroupDataCollection;

        //public MenuButtonData ApplicationMenuData
        //{
        //    get
        //    {
        //        if (_applicationMenuData == null)
        //        {
        //            Uri smallImage = new Uri("/RibbonWindowSample;component/Images/Paste_16x16.png", UriKind.Relative);
        //            _applicationMenuData = new MenuButtonData(true)
        //            {
        //                Label = "AppMenu ",
        //                SmallImage = smallImage,
        //                ToolTipTitle = "ToolTip Title",
        //                ToolTipDescription = "ToolTip Description",
        //                ToolTipImage = smallImage
        //            };
        //        }
        //        return _applicationMenuData;
        //    }
        //}
        //private MenuButtonData _applicationMenuData;

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

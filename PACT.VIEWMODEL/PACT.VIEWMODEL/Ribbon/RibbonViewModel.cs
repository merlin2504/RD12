using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using PACT.MODEL;
using PACT.COMMON;

namespace PACT.VIEWMODEL
{
    public class RibbonViewModel : INotifyPropertyChanged
    {
        ShellWindowViewModel objMainWindowViewModel = new ShellWindowViewModel();
        RibbonGenerator objRibbonGenerator = new RibbonGenerator();
        public static DelegateCommand<string> cmdLoadPage { get; set; }

        public RibbonViewModel()
        {
            cmdLoadPage = new DelegateCommand<string>(OncmdLoadPage);
        }

        void OncmdLoadPage(string obj)
        {
            Logger.InfoLog("RibbonViewModel:: Inside OncmdLoadPage  ID=" + obj.ToString());
            //ScreenViewModel workspace;
            //WorkspaceViewModel wsvm;
            //int _screenID = -1;
            //if (objMainWindowViewModel.Workspaces.Count > 0)
            //{
            //    for (int i = 0; i < objMainWindowViewModel.Workspaces.Count; i++)
            //    {
            //        wsvm = objMainWindowViewModel.Workspaces[i];
            //        if (wsvm.PactScreenID != null && wsvm.PactScreenID.Equals(obj))
            //        {
            //            _screenID = i;
            //            break;
            //        }
            //    }
            //    if (_screenID != -1)
            //    {
            //        workspace = (ScreenViewModel)objMainWindowViewModel.Workspaces[_screenID];
            //        objMainWindowViewModel.SetActiveWorkspace(workspace);
            //    }
            //    else
            //    {
            //        workspace = new ScreenViewModel(obj);
            //        //workspace.DisplayName = obj;
            //        workspace.PactScreenID = obj;
            //        objMainWindowViewModel.Workspaces.Add(workspace);
            //        objMainWindowViewModel.SetActiveWorkspace(workspace);
            //    }
            //}
            //else
            //{
            //    workspace = new ScreenViewModel(obj);
            //    //workspace.DisplayName = obj;
            //    workspace.PactScreenID = obj;
            //    objMainWindowViewModel.Workspaces.Add(workspace);
            //    objMainWindowViewModel.SetActiveWorkspace(workspace);
            //}
            if (obj == "1")
            {
                AddAccountScreenViewModel AddAccountWorkspace = new AddAccountScreenViewModel();
                AddAccountWorkspace.PactScreenID = "1";
                objMainWindowViewModel.Workspaces.Add(AddAccountWorkspace);
                objMainWindowViewModel.SetActiveWorkspace(AddAccountWorkspace);
            }

            if (obj == "2")
            {
                
                ChartOfAccountsScreenViewModel ChartofAccountsWorkspace = new ChartOfAccountsScreenViewModel(1, 1, 1);
                ChartofAccountsWorkspace.altColor1 = Colors.Aqua;
                ChartofAccountsWorkspace.SelectedItemColor = Colors.Lime;
                ChartofAccountsWorkspace.altColor2 = Colors.Aquamarine;


                ChartofAccountsWorkspace.MovingItemColor = Colors.Red;
                ChartofAccountsWorkspace.GroupItemColor = Colors.Black;
                ChartofAccountsWorkspace.ItemColor = Colors.Gray;
                ChartofAccountsWorkspace.RowHeight = 20;
                ChartofAccountsWorkspace.VerticalLine = true;
                ChartofAccountsWorkspace.HorizontalLine = true;
                ChartofAccountsWorkspace.PactScreenID = "2";
                objMainWindowViewModel.Workspaces.Add(ChartofAccountsWorkspace);
                objMainWindowViewModel.SetActiveWorkspace(ChartofAccountsWorkspace);

                //AddProductViewModel AddAccountWorkspace = new AddProductViewModel();
                //AddAccountWorkspace.PactScreenID = "2";
                //objMainWindowViewModel.Workspaces.Add(AddAccountWorkspace);
                //objMainWindowViewModel.SetActiveWorkspace(AddAccountWorkspace);

            }


        }
        //void OnLoadPage(string obj)
        //{
        //    ScreenViewModel workspace = new ScreenViewModel(obj);
        //    objMainWindowViewModel.Workspaces.Add(workspace);
        //    objMainWindowViewModel.SetActiveWorkspace(workspace);
        //}

        public ObservableCollection<TabData> TabDataCollection
        {
            get
            {
                _tabDataCollection = objRibbonGenerator.GetControls();
                return _tabDataCollection;
            }
            
        }
        private ObservableCollection<TabData> _tabDataCollection;

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

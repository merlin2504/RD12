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
            cmdLoadPage = new DelegateCommand<string>(OnLoadPage);
        }

        void OnLoadPage(string obj)
        {
            ScreenViewModel workspace;
            WorkspaceViewModel wsvm;
            int _screenID = -1;
            if (objMainWindowViewModel.Workspaces.Count > 0)
            {
                for (int i = 0; i < objMainWindowViewModel.Workspaces.Count; i++)
                {
                    wsvm = objMainWindowViewModel.Workspaces[i];
                    if (wsvm.PactScreenID != null && wsvm.PactScreenID.Equals(obj))
                    {
                        _screenID = i;
                        break;
                    }
                }
                if (_screenID != -1)
                {
                    workspace = (ScreenViewModel)objMainWindowViewModel.Workspaces[_screenID];
                    objMainWindowViewModel.SetActiveWorkspace(workspace);
                }
                else
                {
                    workspace = new ScreenViewModel(obj);
                    //workspace.DisplayName = obj;
                    workspace.PactScreenID = obj;
                    objMainWindowViewModel.Workspaces.Add(workspace);
                    objMainWindowViewModel.SetActiveWorkspace(workspace);
                }
            }
            else
            {
                workspace = new ScreenViewModel(obj);
                //workspace.DisplayName = obj;
                workspace.PactScreenID = obj;
                objMainWindowViewModel.Workspaces.Add(workspace);
                objMainWindowViewModel.SetActiveWorkspace(workspace);
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

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;


//using DemoApp.DataAccess;
//using DemoApp.Model;
//using DemoApp.Properties;

namespace PACT.VIEWMODEL
{
    /// <summary>
    /// The ViewModel for the application's main window.
    /// </summary>
    public class ShellWindowViewModel : WorkspaceViewModel
    {
        #region Fields
        private static volatile object mLock = new object(); 
        private static ShellWindowViewModel _Instance = null;       
        private static ObservableCollection<WorkspaceViewModel> _workspaces;
        public static string CompanyIndex;

        #endregion // Fields

        #region Constructor

        private static void LoadApplicationConfig()
        {
            
                CompanyIndex = "1";
            
        }

        public ShellWindowViewModel()
        {
            LoadApplicationConfig();
            //base.DisplayName = Strings.MainWindowViewModel_DisplayName;
        }

        public static ShellWindowViewModel Instance()
        {
            if (_Instance == null)
            {
                lock (mLock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new ShellWindowViewModel();
                    }
                }
            }
            return _Instance;
        }

        #endregion // Constructor

        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                    //AddAccountScreenViewModel HomeScreen = new AddAccountScreenViewModel("1");
                    //_workspaces.Add(HomeScreen);
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

        public void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}
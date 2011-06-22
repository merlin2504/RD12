using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using System.Windows.Resources;
using System.Windows;
using System.Data;
using PACT.MODEL;
using PACT.COMMON;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;

namespace PACT.VIEWMODEL
{
    /// <summary>
    /// A UI-friendly wrapper for a Customer object.
    /// </summary>
    public class AddAccountScreenViewModel : WorkspaceViewModel,IDataErrorInfo
    {
        private string ScreenName = "";
        string IDataErrorInfo.Error
        {
            get { return (_PACTControlData as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (_PACTControlData as IDataErrorInfo)[propertyName];

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }


        #region Fields

        private string _ScreenID;
        ControlGenerator objControlGenerator = new ControlGenerator();
        public DelegateCommand<string> DynamicCommand { get; set; }

        #endregion // Fields

        #region Constructor

        public AddAccountScreenViewModel()
        {
            BackgroundWorker worker = new BackgroundWorker();
            _ScreenID = "1";           
            switch (_ScreenID)
            {
                case "1":
                    PactScreenID = "1";
                    ScreenName = "Create Account";
                    //Calling Build control on non-UI thread using BackgroundWorker
                    
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    // Configure the function to run when completed
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    // Launch the worker
                    worker.RunWorkerAsync();                    

                    ////Calling Build control on non-UI thread using BiginInvoke
                    //DisplayName = "Loading...";
                    //AsyncMethodHandler caller = default(AsyncMethodHandler);
                    //caller = new AsyncMethodHandler(this.BuildControls);
                    //// open new thread with callback method 
                    //caller.BeginInvoke(CallBackBuildControls, null);
                  
                    //Calling BuildControls on UI Thread
                    //this.BuildControls
                    break;
                case "4":
                    PactScreenID = "4";
                    ScreenName = "Depreciation";
                      worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    // Configure the function to run when completed
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    // Launch the worker
                    worker.RunWorkerAsync();
                    break;
                case "2000":
                    _DisplayName = "Create Product"; ;
                    break;
                default:
                    //_DisplayName = ScreenID;
                    break;
            }

            DynamicCommand = new DelegateCommand<string>(CommandController);
        }

       

        #endregion // Constructor

        #region Background Thread
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DisplayName = "Loading...";
            _ScreenData = BuildControls();

        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DisplayName = ScreenName;
            //PACTControlData = _PACTControlData;
            
        }

        #endregion

        #region Calling Async Method on non-ui thread
        delegate ObservableCollection<PactControlData> AsyncMethodHandler();
        delegate void UpdateUIHandler(ObservableCollection<PactControlData> _pcd);
        public void UpdateUI(ObservableCollection<PactControlData> _pcd)
        {
            // Get back to primary thread to update ui 
            UpdateUIHandler uiHandler = new UpdateUIHandler(UpdateUIIndicators);
            _PACTControlData = _pcd;

            // Run new thread off Dispatched (primary thread) 
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, _PACTControlData);
        }

        public void UpdateUIIndicators(ObservableCollection<PactControlData> _pcd)
        {
            PACTControlData = _pcd;
            DisplayName = "Account Create";

        }


        protected void CallBackBuildControls(IAsyncResult ar)
        {
            try
            {
                // Retrieve the delegate. 
                AsyncResult result = (AsyncResult)ar;
                AsyncMethodHandler caller = (AsyncMethodHandler)result.AsyncDelegate;

                // Call EndInvoke to retrieve the results. 
                _PACTControlData = caller.EndInvoke(ar);

                // Still on secondary thread, must update ui on primary thread 
                UpdateUI(_PACTControlData);
            }
            catch (Exception ex)
            {
                string exMessage = null;
                exMessage = "Error: " + ex.Message;
                //UpdateUI(exMessage);
            }
        }

        #endregion

        public void CommandController(object sender)
        {
            DataTable dt;
            string strMsg;
            BusinessRules objUIRules = new BusinessRules();
            Util objCommonUtil = new Util();
            switch ((string)sender)
            {
                case "AccountCreate":
                    //UI Validation
                    strMsg = objUIRules.ValidateUIInfo(PACTControlData);
                    if (strMsg != null && !strMsg.Equals(""))
                    {
                        objCommonUtil.InfoMessageBox(strMsg, "Validations");
                        break;
                    }
                    //Building collection to post values to DB
                    dt = objCommonUtil.GetDBValues(PACTControlData);
                    if (dt != null)
                    {
                        System.IO.StringWriter writer = new System.IO.StringWriter();
                        dt.WriteXml(writer);
                        int retVal = objControlGenerator.PostData(writer.ToString(), _ScreenID, ShellWindowViewModel.CompanyIndex);
                        if (retVal > 0)
                            objCommonUtil.InfoMessageBox("Record added sucessfully.", "Validations");
                    }
                    break;
                case "SaveDepreciation":
                    //UI Validation
                    strMsg = objUIRules.ValidateUIInfo(PACTControlData);
                    if (strMsg != null && !strMsg.Equals(""))
                    {
                        objCommonUtil.InfoMessageBox(strMsg, "Validations");
                        break;
                    }
                    //Building collection to post values to DB
                    dt = objCommonUtil.GetDBValues(PACTControlData);
                    if (dt != null)
                    {
                        System.IO.StringWriter writer = new System.IO.StringWriter();
                        dt.WriteXml(writer);
                        int retVal = objControlGenerator.PostData(writer.ToString(), _ScreenID, ShellWindowViewModel.CompanyIndex);
                        if (retVal > 0)
                            objCommonUtil.InfoMessageBox("Record added sucessfully.", "Validations");
                    }
                    break;
            }
        }


        public ObservableCollection<PactControlData> PACTControlData
        {
            get
            {
                return _PACTControlData;
            }
            set
            {
                //if (_PACTControlData != value)
                //{
                _PACTControlData = value;
                base.OnPropertyChanged("PACTControlData");
                // }
            }
        }
        private ObservableCollection<PactControlData> _PACTControlData;

        private DataSet _ScreenData;

        public DataSet ScreenData
        {
            get
            {
                return _ScreenData;
            }
            set
            {
                //if (_PACTControlData != value)
                //{
                _ScreenData = value;
                base.OnPropertyChanged("ScreenData");
                // }
            }
        }


       


        //public ObservableCollection<PactControlData> BuildControls()
        public DataSet BuildControls()
        {
            DataSet ds = objControlGenerator.GetControls(_ScreenID, ShellWindowViewModel.CompanyIndex);
            //if (_PACTControlData != null && _PACTControlData.Count > 0)
            //{

            //    for (int i = 0; i < _PACTControlData.Count; i++)
            //    {
            //        if (_PACTControlData[i].GetType().FullName.Equals("PACT.COMMON.PactTextBlockData"))
            //        {
            //            PactTextBlockData tb = (PactTextBlockData)_PACTControlData[i];

            //            tb.Text = tb.Text;
            //        }
            //    }
            //}
          
            return ds;
        }
        

        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                if (value == _DisplayName)
                    return;

                _DisplayName = value;

                base.OnPropertyChanged("DisplayName");
            }
        }
        private string _DisplayName;


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
        
        


        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

    }
}
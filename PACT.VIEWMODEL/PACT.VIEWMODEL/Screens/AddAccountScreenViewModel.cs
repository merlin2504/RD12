using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using System.Windows.Resources;
using System.Windows;
using System.Data;
using System.Linq;
using PACT.MODEL;
using PACT.COMMON;
using Microsoft.Practices.Prism.Commands;
using System.Collections;
using System.Collections.Generic;
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
        public DelegateCommand<string> SaveAccount { get; set; }
        #endregion // Fields

        #region Constructor
        public AddAccountScreenViewModel()
        {
            Logger.InfoLog("AddAccountScreenViewModel::Entering AddAccountScreenViewModel");
            _AccountTypes = new ObservableCollection<ComboBoxItems>();
            _AccountStatuses = new ObservableCollection<ComboBoxItems>();
            _AccountGroups = new ObservableCollection<ComboBoxItems>();
            _SalesAccounts = new ObservableCollection<ComboBoxItems>();
            _PurchaseAccounts = new ObservableCollection<ComboBoxItems>();
            _AccountCurrency = new ObservableCollection<ComboBoxItems>();
            _BillWise = new ObservableCollection<ComboBoxItems>();

            _SelectedAccountType = "1";

            _MessageVisibility = false;
            _DisplayMessage = "Account";
            _UserID = "1";
            SaveAccount = new DelegateCommand<string>(OnSaveAccount, onCanSaveAccount);

            PactTextBlockData TB = new PactTextBlockData();

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

            Logger.InfoLog("AddAccountScreenViewModel::Exiting AddAccountScreenViewModel");
        }
        #endregion // Constructor

        public void OnSaveAccount(object sender)
        {
            Logger.InfoLog("AddAccountScreenViewModel::Entering OnSaveAccount");
            try
            {
                ArrayList ParamValues = new ArrayList();
                ParamValues.Add(0);
                ParamValues.Add(_AccountCode);
                ParamValues.Add(_AccountName);
                ParamValues.Add(_AliasName);
                ParamValues.Add(_SelectedAccountType);
                ParamValues.Add(_SelectedAccountStatus);
                ParamValues.Add(_SelectedAccountGroup);
                ParamValues.Add(false);
                ParamValues.Add(_CreditDays);
                ParamValues.Add(_CreditLimit);
                ParamValues.Add(_SelectedPurchaseAccount[0]); //_SelectedPurchaseAccount
                ParamValues.Add(_SelectedSalesAccount[0]); //_SelectedSalesAccount
                if (_SelectedBillWise == "1")
                {
                    ParamValues.Add(true);
                }
                else
                {
                    ParamValues.Add(false);
                }
                ParamValues.Add("GUID");
                ParamValues.Add("asdfasdfasdf");
                ParamValues.Add("Mukaram");
                ParamValues.Add("asdfasfdasFD");

                string contacts = @"<ContactsXML><Row AddressTypeID =""1"" ContactName =""name"" Address1=""addr1""  Address2 =""Address2"" ContactName =""name"" Address3=""Address3""  City =""City"" State =""State"" Zip=""Zip"" Country=""Country"" Phone1=""Phone1"" Phone2=""Phone2"" @Fax=""Fax""  Email1=""Email1"" Email2=""Email2"" />";

                string attachments = @"<XML><Row FilePath=""FilePath"" ActualFileName=""ActualFileName"" RelativeFileName=""RelativeFileName"" FileExtension=""FileExtension"" IsProductImage=""IsProductImage"" /></XML>";

                ParamValues.Add("");
                ParamValues.Add("");
                ParamValues.Add(1);
                long ret = objControlGenerator.AddAccountDetails(ParamValues, 1);

                if (ret > 0)
                {
                    _MessageVisibility = true;
                    _DisplayMessage = "Account Added!  " + ret.ToString();
                }
                else
                {
                    _MessageVisibility = true;
                    _DisplayMessage = ret.ToString();

                }
                
                OnPropertyChanged("MessageVisibility");
                OnPropertyChanged("DisplayMessage");
                OnPropertyChanged("AccountName");
                Logger.InfoLog("AddAccountScreenViewModel::Exiting OnSaveAccount");

            }
            catch (Exception ex)
            {
                Logger.ErrorLog("AddAccountScreenViewModel:: OnSaveAccount" + ex.StackTrace.ToString());
            }
        }
        private bool onCanSaveAccount(object sender)
        {
            return true;
        }

        #region Background Thread
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.InfoLog("AddAccountScreenViewModel::Entering worker_DoWork");
            DisplayName = "Loading...";
            _ScreenData = BuildControls();
            Logger.InfoLog("AddAccountScreenViewModel::BuildControls Success");
            //_AccountTypes = _ScreenData.Tables[0].AsEnumerable().ToList();

            foreach (DataRow dr in _ScreenData.Tables[0].Rows)
            {
                _AccountTypes.Add(new ComboBoxItems(dr[2].ToString(), dr[0].ToString()));
            }

            foreach (DataRow dr in _ScreenData.Tables[1].Rows)
            {
                _AccountStatuses.Add(new ComboBoxItems(dr[1].ToString(), dr[0].ToString()));
            }

            foreach (DataRow dr in _ScreenData.Tables[2].Rows)
            {
                _AccountGroups.Add(new ComboBoxItems(dr[2].ToString(), dr[0].ToString()));
            }
            
            foreach (DataRow dr in _ScreenData.Tables[3].Rows)
            {
                _SalesAccounts.Add(new ComboBoxItems(dr[2].ToString(), dr[0].ToString()));
            }
            
            foreach (DataRow dr in _ScreenData.Tables[4].Rows)
            {
                _PurchaseAccounts.Add(new ComboBoxItems(dr[2].ToString(), dr[0].ToString()));
            }

            foreach (DataRow dr in _ScreenData.Tables[5].Rows)
            {
                _AccountCurrency.Add(new ComboBoxItems(dr[1].ToString(), dr[0].ToString()));
            }

            _BillWise.Add(new ComboBoxItems("No", "0"));
            _BillWise.Add(new ComboBoxItems("Yes", "1"));
            Logger.InfoLog("AddAccountScreenViewModel::Exiting worker_DoWork");

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

        private DataRowView _Selected;
        public DataRowView Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                base.OnPropertyChanged("Selected");
                // do whatever you need to do
            }
        }


        private ObservableCollection<PactControlData> _PACTControlData;
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
        
        private DataSet _ScreenData;
       

        private ObservableCollection<ComboBoxItems> _AccountTypes;
        public ObservableCollection<ComboBoxItems> AccountTypes
        {
            get
            {
                return _AccountTypes;
            }
            set
            {
                _AccountTypes = value;
                base.OnPropertyChanged("AccountTypes");
            }
        }

        private ObservableCollection<ComboBoxItems> _AccountStatuses;
        public ObservableCollection<ComboBoxItems> AccountStatuses
        {
            get
            {
                return _AccountStatuses;
            }
            set
            {
                _AccountStatuses = value;
                base.OnPropertyChanged("AccountStatuses");
            }
        }

        private ObservableCollection<ComboBoxItems> _AccountGroups;
        public ObservableCollection<ComboBoxItems> AccountGroups
        {
            get
            {
                return _AccountGroups;
            }
            set
            {
                _AccountGroups = value;
                base.OnPropertyChanged("AccountGroups");
            }
        }

        private ObservableCollection<ComboBoxItems> _SalesAccounts;
        public ObservableCollection<ComboBoxItems> SalesAccounts
        {
            get
            {
                return _SalesAccounts;
            }
            set
            {
                _SalesAccounts = value;
                base.OnPropertyChanged("SalesAccounts");
            }
        }

        private ObservableCollection<ComboBoxItems> _PurchaseAccounts;
        public ObservableCollection<ComboBoxItems> PurchaseAccounts
        {
            get
            {
                return _PurchaseAccounts;
            }
            set
            {
                _PurchaseAccounts = value;
                base.OnPropertyChanged("PurchaseAccounts");
            }
        }

        private ObservableCollection<ComboBoxItems> _AccountCurrency;
        public ObservableCollection<ComboBoxItems> AccountCurrency
        {
            get
            {
                return _AccountCurrency;
            }
            set
            {
                _AccountCurrency = value;
                base.OnPropertyChanged("AccountCurrency");
            }
        }

        private ObservableCollection<ComboBoxItems> _BillWise;
        public ObservableCollection<ComboBoxItems> BillWise
        {
            get
            {
                return _BillWise;
            }
            set
            {
                _BillWise = value;
                base.OnPropertyChanged("BillWise");
            }
        }

        private string _DisplayName;
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (value == _DisplayName)
                    return;

                _DisplayName = value;

                base.OnPropertyChanged("DisplayName");
            }
        }

        private string _UserID;
        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                if (value == _UserID)
                    return;

                _UserID = value;

                base.OnPropertyChanged("UserID");
            }
        }


        private string _SelectedAccountType;
        public string SelectedAccountType
        {
            get
            {
                return _SelectedAccountType;
            }
            set
            {
                if (value == _SelectedAccountType)
                    return;

                _SelectedAccountType = value;

                base.OnPropertyChanged("SelectedAccountType");
            }
        }

        private string _SelectedAccountStatus;
        public string SelectedAccountStatus
        {
            get
            {
                return _SelectedAccountStatus;
            }
            set
            {
                if (value == _SelectedAccountStatus)
                    return;

                _SelectedAccountStatus = value;

                base.OnPropertyChanged("SelectedAccountStatus");
            }
        }

        private string _SelectedAccountGroup;
        public string SelectedAccountGroup
        {
            get
            {
                return _SelectedAccountGroup;
            }
            set
            {
                if (value == _SelectedAccountGroup)
                    return;

                _SelectedAccountGroup = value;

                base.OnPropertyChanged("SelectedAccountGroup");
            }
        }

        private DataRowView _SelectedSalesAccount;
        public DataRowView SelectedSalesAccount
        {
            get
            {
                return _SelectedSalesAccount;
            }
            set
            {
                if (value == _SelectedSalesAccount)
                    return;

                _SelectedSalesAccount = value;

                base.OnPropertyChanged("SelectedSalesAccount");
            }
        }

        private DataRowView _SelectedPurchaseAccount;
        public DataRowView SelectedPurchaseAccount
        {
            get
            {
                return _SelectedPurchaseAccount;
            }
            set
            {
                if (value == _SelectedPurchaseAccount)
                    return;

                _SelectedPurchaseAccount = value;

                base.OnPropertyChanged("SelectedPurchaseAccount");
            }
        }
               
        private string _SelectedCurrency;
        public string SelectedCurrency
        {
            get
            {
                return _SelectedCurrency;
            }
            set
            {
                if (value == _SelectedCurrency)
                    return;

                _SelectedCurrency = value;

                base.OnPropertyChanged("SelectedCurrency");
            }
        }

        private string _SelectedBillWise;
        public string SelectedBillWise
        {
            get
            {
                return _SelectedBillWise;
            }
            set
            {
                if (value == _SelectedBillWise)
                    return;

                _SelectedBillWise = value;

                base.OnPropertyChanged("SelectedBillWise");
            }
        }

        private string _AccountName;
        public string AccountName
        {
            get
            {
                return _AccountName;
            }
            set
            {
                if (value == _AccountName)
                    return;

                _AccountName = value;

                base.OnPropertyChanged("AccountName");
            }
        }

        private string _AccountCode;
        public string AccountCode
        {
            get
            {
                return _AccountCode;
            }
            set
            {
                if (value == _AccountCode)
                    return;

                _AccountCode = value;

                base.OnPropertyChanged("AccountCode");
            }
        }

        private string _AliasName;
        public string AliasName
        {
            get
            {
                return _AliasName;
            }
            set
            {
                if (value == _AliasName)
                    return;

                _AliasName = value;

                base.OnPropertyChanged("AliasName");
            }
        }

        private string _CreditLimit;
        public string CreditLimit
        {
            get
            {
                return _CreditLimit;
            }
            set
            {
                if (value == _CreditLimit)
                    return;

                _CreditLimit = value;

                base.OnPropertyChanged("CreditLimit");
            }
        }

        private string _CreditDays;
        public string CreditDays
        {
            get
            {
                return _CreditDays;
            }
            set
            {
                if (value == _CreditDays)
                    return;

                _CreditDays = value;

                base.OnPropertyChanged("CreditDays");
            }
        }

        private bool _MessageVisibility;
        public bool MessageVisibility
        {
            get
            {
                return _MessageVisibility;
            }
            set
            {
                if (value == _MessageVisibility)
                    return;
                _MessageVisibility = value;
                base.OnPropertyChanged("MessageVisibility");

            }
        }

        private string _DisplayMessage;
        public string DisplayMessage
        {
            get
            {
                return _DisplayMessage;
            }
            set
            {
                if (value == _DisplayMessage)
                    return;
                _DisplayMessage = value;
                base.OnPropertyChanged("DisplayMessage");
            }
        }

        private DataSet BuildControls()
        {
            Logger.InfoLog("AddAccountScreenViewModel::Entering BuildControls");

            try
            {
                DataSet ds = objControlGenerator.GetControls(_ScreenID, ShellWindowViewModel.CompanyIndex);
                return ds;
            }
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
            catch (Exception ex)
            {
                Logger.ErrorLog("AddAccountScreenViewModel:: BuildControls" + ex.StackTrace.ToString());
                return null;
            }

        }
        



        #region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(PropertyChangedEventArgs e)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, e);
        //    }
        //}

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

    public class ComboBoxItems
    {
        #region Properties

        private string _name = string.Empty;
        private string _value = string.Empty;

        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
        }
        public string Value 
        { 
            get 
            { 
                return _value; 
            } 
        }

        #endregion Properties

        public ComboBoxItems(string name, string value)
        {
            this._name = name;
            this._value = value;
        }
    }

}
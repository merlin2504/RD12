﻿using System;
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

namespace PACT.VIEWMODEL
{
    /// <summary>
    /// A UI-friendly wrapper for a Customer object.
    /// </summary>
    public class ScreenViewModel : WorkspaceViewModel
    {
        #region Fields
        
        private string _ScreenID;
        ControlGenerator objControlGenerator = new ControlGenerator();
        public DelegateCommand<string> DynamicCommand { get; set; }
        
        #endregion // Fields

        #region Constructor

        public ScreenViewModel(string ScreenID)
        {
            _ScreenID = ScreenID;
            switch (_ScreenID)
            {
                case "1000":
                    PactScreenID = "1000";
                    _DisplayName = "Create Account";
                    break;
                case "2000":
                    _DisplayName = "Create Product";;
                    break;
                default:
                     _DisplayName = ScreenID;
                     break; 
            }
          
            DynamicCommand = new DelegateCommand<string>(CommandController);
        }

        #endregion // Constructor

        public void CommandController(object sender)
        {
                BusinessRules objUIRules = new BusinessRules();
                Util objCommonUtil = new Util();
                switch ((string)sender)
                {
                    case "AccountCreate":
                    //UI Validation
                    string strMsg = objUIRules.ValidateUIInfo(PACTControlData);
                    if (strMsg != null && !strMsg.Equals(""))
                    {
                        objCommonUtil.InfoMessageBox(strMsg, "Validations");
                    }
                    //Building collection to post values to DB
                    DataTable dt = objCommonUtil.GetDBValues(PACTControlData);
                    if (dt != null)
                    {
                        System.IO.StringWriter writer = new System.IO.StringWriter();
                        dt.WriteXml(writer);
                        int retVal=objControlGenerator.PostData(writer.ToString(), _ScreenID, ShellWindowViewModel.CompanyIndex);
                        if(retVal>0)
                            objCommonUtil.InfoMessageBox("Record added sucessfully.", "Validations");
                    }
                    break;
                }
        }


        public ObservableCollection<PactControlData> PACTControlData
        {
            get
            {

                _PACTControlData = objControlGenerator.GetControls(_ScreenID, ShellWindowViewModel.CompanyIndex);
                
                return _PACTControlData;

            }
        }

        private ObservableCollection<PactControlData> _PACTControlData;
        
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Specialized;

namespace PACT.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICommon" in both code and config file together.
    [ServiceContract]
    public interface ICommon
    {
        [OperationContract]
        string GetCommonData(int value);

        [OperationContract]
        DataSet GetScreenInfoByID(int ScreenID,string strCulture, string CompanyIndex);

        [OperationContract]
        DataTable ExecuteQuery(string Query, string CompanyIndex);

        [OperationContract]
        int CreateAccount(string XMLControlData, string CompanyIndex);

        [OperationContract]
        int CreateDepreciation(string XMLControlData, string CompanyIndex);

        [OperationContract]
        int CreateProduct(string XMLControlData, string CompanyIndex);

        [OperationContract]
        DataTable GetDataTable_Search(SearchCriteria objSearch, string CompanyIndex);

        [OperationContract]
        DataTable GetDataTable(string strQuery, string CompanyIndex);

        [OperationContract]
        DataSet GetDataSet_Search(SearchCriteria objSearch, string CompanyIndex);
      
    } 
    [DataContract]
    public class SearchCriteria
    {
        private string _Query;
        [DataMember]
        public string Query
        {
            get { return _Query; }
            set { _Query = value; }
        }

        private string _selectedValueQuery;
        [DataMember]
        public string SelectedValueQuery
        {
            get { return _selectedValueQuery; }
            set { _selectedValueQuery = value; }
        }

        private string _WhereString;
        [DataMember]
        public string WhereString
        {
            get { return _WhereString; }
            set { _WhereString = value; }
        }

        private string _GroupBy;
        [DataMember]
        public string GroupBy
        {
            get { return _GroupBy; }
            set { _GroupBy = value; }
        }
        [DataMember]
        public string PrimaryKey;
        [DataMember]
        public int PrimaryValue;

        private string _SearchOn;
        [DataMember]
        public string SearchOn
        {
            get { return _SearchOn; }
            set { _SearchOn = value; }
        }

        private string _SearchValue;
        [DataMember]
        public string SearchValue
        {
            get { return _SearchValue; }
            set { _SearchValue = value; }
        }

        private int _MaximumRows;
        [DataMember]
        public int MaximumRows
        {
            get { return _MaximumRows; }
            set { _MaximumRows = value; }
        }

        private int _ProductNo;
        [DataMember]
        public int ProductNo
        {
            get { return _ProductNo; }
            set { _ProductNo = value; }
        }

        private string _ProductCode;
        [DataMember]
        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }

        private int _CustomerNo;
        [DataMember]
        public int CustomerNo
        {
            get { return _CustomerNo; }
            set { _CustomerNo = value; }
        }

        private int _Location;
        [DataMember]
        public int Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        private bool _IsTagWise;
        [DataMember]
        public bool IsTagWise
        {
            get { return _IsTagWise; }
            set { _IsTagWise = value; }
        }

        private bool _IsMoveUp;
        [DataMember]
        public bool IsMoveUp
        {
            get { return _IsMoveUp; }
            set { _IsMoveUp = value; }
        }

    }

}

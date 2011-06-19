using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace PACT.DBHandler
{
    public interface IDBHandler
    {

        #region Accounting

        DataSet GetAccountDetails(int CompanyIndex, ArrayList Param);

        DataSet GetAddAccountScreenDetails(int CompanyIndex, ArrayList Param);

        string DeleteAccount(int CompanyIndex, ArrayList Param, out long AccountID);

        string SetAccountDetails(int CompanyIndex, ArrayList Param, out long AccountID);


        string SetDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long DepreciationID);


        DataSet GetDepreciationOfAccount(int CompanyIndex, ArrayList Param);


        string RemoveDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long DepreciationID);


        string AddCustomAccountFields(int CompanyIndex, ArrayList Param, out long CustomColID);



        string SetAccountPreferences(int CompanyIndex, ArrayList Param, out long AccountID);



        string MapAccountToCostCenter(int CompanyIndex, ArrayList Param, out long AccountID);


        DataSet GetCostCenterAccounts(int CompanyIndex, ArrayList Param);


        DataSet GetAccountCostCenters(int CompanyIndex, ArrayList Param);




        #endregion

        #region Inventory



        #endregion

        #region Common

        DataSet GetCostCenterSummary(int CompanyIndex, ArrayList Param);


        string DefineCustomFields(int CompanyIndex, ArrayList Param, out long CustomColID);

        DataSet GetCostCenterNodeNotes(int CompanyIndex, ArrayList Param);

        string SetCostCenterNodeNotes(int CompanyIndex, ArrayList Param, out long NodeID);


        DataSet GetCostCenterNodeFiles(int CompanyIndex, ArrayList Param);

        string SetCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long NodeID);

        string DeleteCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long NodeID);


        DataSet GetCostCenterNodeContacts(int CompanyIndex, ArrayList Param);

        string SetCostCenterNodeContacts(int CompanyIndex, ArrayList Param, out long NodeID);

        string DeleteCostCenterNodeContacts(int CompanyIndex, ArrayList Param, out long NodeID);



        DataSet GetCostCenterStatuses(int CompanyIndex, ArrayList Param);

        string SetCostCenterStatuses(int CompanyIndex, ArrayList Param, out long StatusID);

        string DeleteCostCenterStatuses(int CompanyIndex, ArrayList Param, out long StatusID);


        #endregion

        #region Administration

        string SetCostCenterGridViewDefault(int CompanyIndex, ArrayList Param, out long GridViewID);

        DataSet GetCostCenterGridViewList(int CompanyIndex, ArrayList Param);


        string DefineCostCenterGridView(int CompanyIndex, ArrayList Param, out long GridViewID);


        DataSet GetCostCenterListView(int CompanyIndex, ArrayList Param);


        string DefineCostCenterListView(int CompanyIndex, ArrayList Param, out long ListViewID);


        DataSet GetCostCenterListViewData(int CompanyIndex, ArrayList Param);

        #endregion

        #region TestClient
        DataSet GetMetaData(int CompanyIndex, ArrayList Param);


        DataSet Getter(int CompanyIndex, ArrayList Param, string SpName);


        string settter(int CompanyIndex, ArrayList Param, string SpName, out long ID);

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using PACT.Service;
using System.Collections;

namespace PACT.DBHandler
{
    public class DBHandler : IDBHandler
    {

        private static string DBAdapterAccess = "";

        /// <summary>
        /// Constuctor of DBHandler
        /// </summary>
        public DBHandler()
        {
            if (DBAdapterAccess == null || DBAdapterAccess.Equals(""))
            {
                DBAdapterAccess = System.Configuration.ConfigurationManager.AppSettings["DBAdapterAccess"];
            }
        }
        
        #region Accounting

        public DataSet GetAddAccountScreenDetails(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetAddAccountScreenDetails");
        }

        public DataSet GetAccountDetails(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetAccountDetails");
        }

        public string DeleteAccount(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_DeleteAccount", out AccountID);

        }
        public string SetAccountDetails(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_SetAccount", out AccountID);
        }

        public string SetDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long DepreciationID)
        {
            return Set(CompanyIndex, Param, "spACC_SetDepreciationOnAccount", out DepreciationID);
        }

        public DataSet GetDepreciationOfAccount(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetDepreciationOfAccount");
        }

        public string RemoveDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long DepreciationID)
        {
            return Set(CompanyIndex, Param, "spACC_RemoveDepreciationOnAccount", out DepreciationID);
        }

        public string AddCustomAccountFields(int CompanyIndex, ArrayList Param, out long CustomColID)
        {
            return Set(CompanyIndex, Param, "spACC_AddCustomAccountFields", out CustomColID);
        }


        public string SetAccountPreferences(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_SetAccountPreferences", out AccountID);
        }


        public string MapAccountToCostCenter(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_MapAccountToCostCenter", out AccountID);
        }

        public DataSet GetCostCenterAccounts(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetCostCenterAccounts");
        }

        public DataSet GetAccountCostCenters(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetAccountCostCenters");
        }



        #endregion

        #region Inventory



        #endregion

        #region Common

        public DataSet GetCostCenterSummary(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetCostCenterSummary");
        }

        public string DefineCustomFields(int CompanyIndex, ArrayList Param, out long CustomColID)
        {
            return Set(CompanyIndex, Param, "spCOM_DefineCustomFields", out CustomColID);
        }

        public DataSet GetCostCenterNodeNotes(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterNodeNotes");
        }
        public string SetCostCenterNodeNotes(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCostCenterNodeNotes", out NodeID);
        }

        public DataSet GetCostCenterNodeFiles(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterNodeFiles");
        }
        public string SetCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCostCenterNodeFiles", out NodeID);
        }
        public string DeleteCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_DeleteCostCenterNodeFiles", out NodeID);
        }

        public DataSet GetCostCenterNodeContacts(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterNodeContacts");
        }
        public string SetCostCenterNodeContacts(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCostCenterNodeContacts", out NodeID);
        }
        public string DeleteCostCenterNodeContacts(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_DeleteCostCenterNodeContacts", out NodeID);
        }


        public DataSet GetCostCenterStatuses(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterStatuses");
        }
        public string SetCostCenterStatuses(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCostCenterStatuses", out StatusID);
        }
        public string DeleteCostCenterStatuses(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_DeleteCostCenterStatuses", out StatusID);
        }

        #endregion

        #region Administration

        public string SetCostCenterGridViewDefault(int CompanyIndex, ArrayList Param, out long GridViewID)
        {
            return Set(CompanyIndex, Param, "spADM_SetCostCenterGridViewDefault", out GridViewID);
        }
        public DataSet GetCostCenterGridViewList(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spADM_GetCostCenterGridViewList");
        }

        public string DefineCostCenterGridView(int CompanyIndex, ArrayList Param, out long GridViewID)
        {
            return Set(CompanyIndex, Param, "spADM_DefineCostCenterGridView", out GridViewID);
        }


        public DataSet GetCostCenterListView(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spADM_GetCostCenterListView");
        }

        public string DefineCostCenterListView(int CompanyIndex, ArrayList Param, out long ListViewID)
        {
            return Set(CompanyIndex, Param, "spADM_DefineCostCenterListView", out ListViewID);
        }

        public DataSet GetCostCenterListViewData(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spADM_GetCostCenterListViewData");
        }

        #endregion

        #region TestClient
        public DataSet GetMetaData(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spUTL_GetProcedureMetaData");
        }

        public DataSet Getter(int CompanyIndex, ArrayList Param, string SpName)
        {
            return Get(CompanyIndex, Param, SpName);
        }

        public string settter(int CompanyIndex, ArrayList Param, string SpName, out long ID)
        {
            return Set(CompanyIndex, Param, SpName, out  ID);
        }
        #endregion
         
        #region Private Methods

        string Set(int CompanyIndex, ArrayList Param, string SpName, out long ReturnValue)
        {
            ReturnValue = 0;
            if (DBAdapterAccess.Equals("LOCAL"))
            {
                return new General().Set(CompanyIndex, Param, SpName, out ReturnValue);
            }
            else if (DBAdapterAccess.Equals("REMOTE"))
            {
                return new PactWebService.PactWebServiceClient().Set(out ReturnValue, CompanyIndex, Param.ToArray(), SpName);
            }
            return string.Empty;
        }

        DataSet Get(int CompanyIndex, ArrayList Param, string SpName)
        {
            DataSet ds = new DataSet();

            if (DBAdapterAccess.Equals("LOCAL"))
            {
                ds = new General().Get(CompanyIndex, Param, SpName);
            }
            else if (DBAdapterAccess.Equals("REMOTE"))
            {
                ds = new PactWebService.PactWebServiceClient().Get(CompanyIndex, Param.ToArray(), SpName);
            }
            return ds;
        }

        #endregion

    }
}

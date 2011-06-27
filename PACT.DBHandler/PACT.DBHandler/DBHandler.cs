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

        public string MoveAccount(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_MoveAccount", out AccountID);

        }

        public string  SetDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_SetDepreciationOnAccount", out AccountID);

        }


        
        public DataSet  GetAddAccountScreenDetails(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetAddAccountScreenDetails");
        }
            
        public DataSet  GetDepreciationOfAccount(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetDepreciationOfAccount");
        }

        public string  RemoveDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_RemoveDepreciationOnAccount", out AccountID);

        }

        public string  AddCustomAccountFields(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_AddCustomAccountFields", out AccountID);

        }

        public string DefineCustomFields(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spCOM_DefineCustomFields", out AccountID);

        }

        public string  MapAccountToCostCenter(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_MapAccountToCostCenter", out AccountID);

        }

        public DataSet GetCostCenterNodeNotes(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterNodeNotes");
        }

        public string SetCostCenterNodeNotes(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCostCenterNodeNotes", out AccountID);

        }



        public DataSet GetCostCenterNodeFiles(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterNodeFiles");
        }

        public string  SetCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCostCenterNodeFiles", out AccountID);

        }

        public string  DeleteCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spCOM_DeleteCostCenterNodeFiles", out AccountID);

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

       


        public string SetAccountPreferences(int CompanyIndex, ArrayList Param, out long AccountID)
        {
            return Set(CompanyIndex, Param, "spACC_SetAccountPreferences", out AccountID);
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
        /// <summary>
        /// covers create/new & edit Product or group (main & extended tables)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public string SetProduct(int CompanyIndex, ArrayList objParam, out long ProductID)
        {
            return Set(CompanyIndex, objParam, "spINV_SetProduct", out ProductID);
        }
        /// <summary>
        /// read product or group
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetProductDetails(int CompanyIndex, ArrayList objParam)
        {
            return Get(CompanyIndex, objParam, "spINV_GetProductDetails");
        }
        /// <summary>
        /// deletes a product or group
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string DeleteProduct(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_DeleteProduct", out NodeID);
        }

        /// <summary>
        /// moves a product or group under a different group
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string MoveProduct(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_MoveProduct", out NodeID);
        }
        /// <summary>
        ///generates DDL to change extended table; called from DefineCustomFields SP
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string AddCustomProductFields(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_AddCustomProductFields", out NodeID);
        }
        /// <summary>
        /// SP to set or clear product level preferences
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string SetProductPreferences(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_SetProductPreferences", out NodeID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string MapProductToCostCenter(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_MapProductToCostCenter", out NodeID);
        }
        /// <summary>
        /// Get a list of products/groups mapped to the cost center set (incl rates)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetCostCenterProducts(int CompanyIndex, ArrayList objParam)
        {
            return Get(CompanyIndex, objParam, "spINV_GetCostCenterProducts");
        }
        /// <summary>
        /// Get a list of cost center associated with product/group (incl rates)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public DataSet GetProductCostCenters(int CompanyIndex, ArrayList objParam)
        {
            return Get(CompanyIndex, objParam, "spINV_GetProductCostCenters");
        }
        /// <summary>
        /// Get a list of product substitutes for a specific product
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public DataSet GetProductSubstitutes(int CompanyIndex, ArrayList objParam)
        {
            return Get(CompanyIndex, objParam, "spINV_GetProductSubstitutes");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string SetProductSubstitutes(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_SetProductSubstitutes", out NodeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string RemoveProductSubstitutes(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_RemoveProductSubstitutes", out NodeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public DataSet GetProductVendor(int CompanyIndex, ArrayList objParam)
        {
            return Get(CompanyIndex, objParam, "spINV_GetProductVendor");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string SetProductVendor(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_SetProductVendor", out NodeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string RemoveProductVendor(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_RemoveProductVendor", out NodeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public DataSet GetProductBundle(int CompanyIndex, ArrayList objParam)
        {
            return Get(CompanyIndex, objParam, "spINV_GetProductBundle");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string SetProductBundle(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_SetProductBundle", out NodeID);
        }
        /// <summary>
        /// Remove a single/group of product(s) from a bundle or remove complete bundle 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string RemoveProductFromBundle(int CompanyIndex, ArrayList objParam, out long NodeID)
        {
            return Set(CompanyIndex, objParam, "spINV_RemoveProductFromBundle", out NodeID);
        }
        /// <summary>
        /// Assign/Remove Atrributes to a Product and group them and give a group name
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string MapProductAttributes(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_MapProductAttributes", out StatusID);
        }
        /// <summary>
        /// Get a list of attributes / values assigned to this product
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetProductAttributes(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spINV_GetProductAttributes");
        }
        /// <summary>
        /// covers create/new & edit a Batch
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string SetBatch(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_SetBatch", out StatusID);
        }
        /// <summary>
        /// get batch details for that specific batch including Product/Account mapped
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetBatchDetails(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spINV_GetBatchDetails");
        }
        /// <summary>
        /// Assign/Remove batch to a Product 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string MapBatchtoProduct(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_MapBatchtoProduct", out StatusID);
        }
        /// <summary>
        /// Assign/Remove batch to an Account
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string MapBatchtoAccount(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_MapBatchtoAccount", out StatusID);
        }
        /// <summary>
        /// Assign/Remove batch to an Cost Center/NodeID and also define overriding rates for that specific map
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string MapBatchtoCostCenter(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_MapBatchtoCostCenter", out StatusID);
        }
        /// <summary>
        /// Get a list of all Barcodes defined for that product and product/cost center map
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetBarcodes(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spINV_GetBarcodes");
        }
        /// <summary>
        /// Add new or Edit existing barcode and also assign it either to a product or a cost center set or both
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string SetBarcode(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_SetBarcode", out StatusID);
        }
        /// <summary>
        /// Delete existing barcode 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string RemoveBarcode(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_RemoveBarcode", out StatusID);
        }
        /// <summary>
        /// covers create/new & edit an existing scheme (defined on a product) - impacts Scheme, Promotion, Frequency tables
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string SetScheme(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_SetScheme", out StatusID);
        }
        /// <summary>
        /// get list of schemes valid for that Product - from Scheme, Promotion, Frequency tables
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetSchemes(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spINV_GetSchemes");
        }
        /// <summary>
        /// Assign/Remove scheme to a cost center
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string MapCostCenterScheme(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spINV_MapCostCenterScheme", out StatusID);
        }
        #endregion

        #region Common

        public string DefineCostCenter(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_DefineCostCenter", out NodeID);
        }

        public string SetCostCenter(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_DefineCostCenter", out NodeID);
        }

        public DataSet GetCostCenterDetails(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterDetails");
        }

        public string DeleteCostCenter(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_DeleteCostCenter", out NodeID);
        }

        public string MoveCostCenter(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_MoveCostCenter", out NodeID);
        }

        public string  MapCostCenterToCostCenter(int CompanyIndex, ArrayList Param, out long NodeID)
        {
            return Set(CompanyIndex, Param, "spCOM_MapCostCenterToCostCenter", out NodeID);
        }

        public DataSet  GetCostCenterScreenDetails(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCostCenterScreenDetails");
        }

        public DataSet GetCostCenterSummary(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spACC_GetCostCenterSummary");
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

        /// <summary>
        /// Get a list of all defined currencies
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetCurrencies(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetCurrencies");
        }
        /// <summary>
        /// Add new or Edit existing currency
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string SetCurrencies(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetCurrencies", out StatusID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string RemoveCurrencies(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_RemoveCurrencies", out StatusID);
        }
        /// <summary>
        /// Get a list of all exchange rates based on currencyId, date range or all defined ex rates
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetExchangeRates(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetExchangeRates");
        }
        /// <summary>
        /// Add new or Edit exchange rate
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string SetExchangeRates(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetExchangeRates", out StatusID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataSet GetUOM(int CompanyIndex, ArrayList Param)
        {
            return Get(CompanyIndex, Param, "spCOM_GetUOM");
        }
        /// <summary>
        /// Add new or Edit existing Unit of Measure
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string SetUOM(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_SetUOM", out StatusID);
        }
        /// <summary>
        /// Remove existing Unit of Measure(s)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public string RemoveUOM(int CompanyIndex, ArrayList Param, out long StatusID)
        {
            return Set(CompanyIndex, Param, "spCOM_RemoveUOM", out StatusID);
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
            return Get(CompanyIndex, Param, "spADM_GetCostCenterListViewDef");
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

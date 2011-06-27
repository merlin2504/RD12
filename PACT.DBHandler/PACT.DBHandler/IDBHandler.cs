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

        DataSet GetAddAccountScreenDetails(int CompanyIndex, ArrayList Param);

        string MoveAccount(int CompanyIndex, ArrayList Param, out long AccountID);

        string SetDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long AccountID);

        DataSet GetDepreciationOfAccount(int CompanyIndex, ArrayList Param);

        string RemoveDepreciationOnAccount(int CompanyIndex, ArrayList Param, out long AccountID);

        string AddCustomAccountFields(int CompanyIndex, ArrayList Param, out long AccountID);

        string DefineCustomFields(int CompanyIndex, ArrayList Param, out long AccountID);

        string MapAccountToCostCenter(int CompanyIndex, ArrayList Param, out long AccountID);

        DataSet GetCostCenterNodeNotes(int CompanyIndex, ArrayList Param);

        string SetCostCenterNodeNotes(int CompanyIndex, ArrayList Param, out long AccountID);

        string SetCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long AccountID);

        DataSet GetCostCenterNodeFiles(int CompanyIndex, ArrayList Param);

       

        string DeleteCostCenterNodeFiles(int CompanyIndex, ArrayList Param, out long AccountID);

        DataSet GetAccountDetails(int CompanyIndex, ArrayList Param);


        string DeleteAccount(int CompanyIndex, ArrayList Param, out long AccountID);

        string SetAccountDetails(int CompanyIndex, ArrayList Param, out long AccountID);


       


        string SetAccountPreferences(int CompanyIndex, ArrayList Param, out long AccountID);



       


        DataSet GetCostCenterAccounts(int CompanyIndex, ArrayList Param);


        DataSet GetAccountCostCenters(int CompanyIndex, ArrayList Param);




        #endregion

        #region Inventory
        /// <summary>
        /// covers create/new & edit Product or group (main & extended tables)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        string SetProduct(int CompanyIndex, ArrayList objParam, out long ProductID);
       
        /// <summary>
        /// read product or group
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetProductDetails(int CompanyIndex, ArrayList objParam);
        
        /// <summary>
        /// deletes a product or group
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string DeleteProduct(int CompanyIndex, ArrayList objParam, out long NodeID);
    

        /// <summary>
        /// moves a product or group under a different group
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string MoveProduct(int CompanyIndex, ArrayList objParam, out long NodeID);
        
        /// <summary>
        ///generates DDL to change extended table; called from DefineCustomFields SP
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string AddCustomProductFields(int CompanyIndex, ArrayList objParam, out long NodeID);
      
        /// <summary>
        /// SP to set or clear product level preferences
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string SetProductPreferences(int CompanyIndex, ArrayList objParam, out long NodeID);
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string MapProductToCostCenter(int CompanyIndex, ArrayList objParam, out long NodeID);
       
        /// <summary>
        /// Get a list of products/groups mapped to the cost center set (incl rates)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetCostCenterProducts(int CompanyIndex, ArrayList objParam);
       
        /// <summary>
        /// Get a list of cost center associated with product/group (incl rates)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        DataSet GetProductCostCenters(int CompanyIndex, ArrayList objParam);
       
        /// <summary>
        /// Get a list of product substitutes for a specific product
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        DataSet GetProductSubstitutes(int CompanyIndex, ArrayList objParam);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string SetProductSubstitutes(int CompanyIndex, ArrayList objParam, out long NodeID);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string RemoveProductSubstitutes(int CompanyIndex, ArrayList objParam, out long NodeID);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        DataSet GetProductVendor(int CompanyIndex, ArrayList objParam);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string SetProductVendor(int CompanyIndex, ArrayList objParam, out long NodeID);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string RemoveProductVendor(int CompanyIndex, ArrayList objParam, out long NodeID);
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <returns></returns>
        DataSet GetProductBundle(int CompanyIndex, ArrayList objParam);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string SetProductBundle(int CompanyIndex, ArrayList objParam, out long NodeID);
      
        /// <summary>
        /// Remove a single/group of product(s) from a bundle or remove complete bundle 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="objParam"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        string RemoveProductFromBundle(int CompanyIndex, ArrayList objParam, out long NodeID);
       
        /// <summary>
        /// Assign/Remove Atrributes to a Product and group them and give a group name
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
         string MapProductAttributes(int CompanyIndex, ArrayList Param, out long StatusID);
      
        /// <summary>
        /// Get a list of attributes / values assigned to this product
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetProductAttributes(int CompanyIndex, ArrayList Param);
        
        /// <summary>
        /// covers create/new & edit a Batch
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string SetBatch(int CompanyIndex, ArrayList Param, out long StatusID);
        
        /// <summary>
        /// get batch details for that specific batch including Product/Account mapped
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetBatchDetails(int CompanyIndex, ArrayList Param);
      
        /// <summary>
        /// Assign/Remove batch to a Product 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
         string MapBatchtoProduct(int CompanyIndex, ArrayList Param, out long StatusID);
       
        /// <summary>
        /// Assign/Remove batch to an Account
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string MapBatchtoAccount(int CompanyIndex, ArrayList Param, out long StatusID);
        
        /// <summary>
        /// Assign/Remove batch to an Cost Center/NodeID and also define overriding rates for that specific map
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string MapBatchtoCostCenter(int CompanyIndex, ArrayList Param, out long StatusID)
      ;
        /// <summary>
        /// Get a list of all Barcodes defined for that product and product/cost center map
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
         DataSet GetBarcodes(int CompanyIndex, ArrayList Param);
       
        /// <summary>
        /// Add new or Edit existing barcode and also assign it either to a product or a cost center set or both
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
         string SetBarcode(int CompanyIndex, ArrayList Param, out long StatusID);
       
        /// <summary>
        /// Delete existing barcode 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
         string RemoveBarcode(int CompanyIndex, ArrayList Param, out long StatusID);
       
        /// <summary>
        /// covers create/new & edit an existing scheme (defined on a product) - impacts Scheme, Promotion, Frequency tables
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
         string SetScheme(int CompanyIndex, ArrayList Param, out long StatusID);
        
        /// <summary>
        /// get list of schemes valid for that Product - from Scheme, Promotion, Frequency tables
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
         DataSet GetSchemes(int CompanyIndex, ArrayList Param);
        
        /// <summary>
        /// Assign/Remove scheme to a cost center
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string MapCostCenterScheme(int CompanyIndex, ArrayList Param, out long StatusID);
      
        #endregion

        #region Common
        /// <summary>
        /// Get a list of all defined currencies
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetCurrencies(int CompanyIndex, ArrayList Param);
       
        /// <summary>
        /// Add new or Edit existing currency
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string SetCurrencies(int CompanyIndex, ArrayList Param, out long StatusID);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string RemoveCurrencies(int CompanyIndex, ArrayList Param, out long StatusID);
      
        /// <summary>
        /// Get a list of all exchange rates based on currencyId, date range or all defined ex rates
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetExchangeRates(int CompanyIndex, ArrayList Param);
       
        /// <summary>
        /// Add new or Edit exchange rate
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string SetExchangeRates(int CompanyIndex, ArrayList Param, out long StatusID);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataSet GetUOM(int CompanyIndex, ArrayList Param);
      
        /// <summary>
        /// Add new or Edit existing Unit of Measure
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string SetUOM(int CompanyIndex, ArrayList Param, out long StatusID);
       
        /// <summary>
        /// Remove existing Unit of Measure(s)
        /// </summary>
        /// <param name="CompanyIndex"></param>
        /// <param name="Param"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        string RemoveUOM(int CompanyIndex, ArrayList Param, out long StatusID);
        
        string DefineCostCenter(int CompanyIndex, ArrayList Param, out long NodeID);

        string SetCostCenter(int CompanyIndex, ArrayList Param, out long NodeID);

        DataSet GetCostCenterDetails(int CompanyIndex, ArrayList Param);

        string DeleteCostCenter(int CompanyIndex, ArrayList Param, out long NodeID);

        string MoveCostCenter(int CompanyIndex, ArrayList Param, out long NodeID);

        string  MapCostCenterToCostCenter(int CompanyIndex, ArrayList Param, out long NodeID);

        DataSet  GetCostCenterScreenDetails(int CompanyIndex, ArrayList Param);

        DataSet GetCostCenterSummary(int CompanyIndex, ArrayList Param);

         


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

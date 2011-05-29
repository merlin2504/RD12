using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using PACT.COMMON;
using System.Collections.Specialized;
using System.Xml;

namespace PACT.Service
{
    public class CommonSqlDal
    {

        public DataSet ExecuteQuery(string Query, string CompanyIndex)
        {
            ArrayList param = new ArrayList();
            param.Add(Query);
            try
            {
                DbUtilResult dbResult = DBUtil.Execute("SPExecuteQuery", param, DBUtil.GetConnection(CompanyIndex));
                return dbResult.Contents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CreateAccount(string XMLControlData, string CompanyIndex)
        {
            ArrayList Params = SetCreateAccountParams(XMLControlData);
            int returnValue = 0;
            try
            {
                DbUtilResult dbResult = DBUtil.Execute("SaveAccount", Params, DBUtil.GetConnection(CompanyIndex));
                returnValue = Convert.ToInt32(dbResult.Parameters["@RETURN_VALUE"]);
                return returnValue;
            }           
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CreateDepreciation(string XMLControlData, string CompanyIndex)
        {
            ArrayList Params = SetCreateDepreciationParams(XMLControlData);
            int returnValue = 0;
            try
            {
                DbUtilResult dbResult = DBUtil.Execute("SaveDepreciation", Params, DBUtil.GetConnection(CompanyIndex));
                returnValue = Convert.ToInt32(dbResult.Parameters["@RETURN_VALUE"]);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CreateProduct(string XMLControlData, string CompanyIndex)
        {
            ArrayList Params = new ArrayList();
            int returnValue = 0;
            try
            {
                DbUtilResult dbResult = DBUtil.Execute("SPCreateProduct", Params, DBUtil.GetConnection(CompanyIndex));
                returnValue = Convert.ToInt32(dbResult.Parameters["@RETURN_VALUE"]);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private object GetValue(XmlNode _xnd, string _ColNm)
        {
            object obj = null;
           if(_xnd.SelectSingleNode(_ColNm)!=null)
                obj = (object)_xnd.SelectSingleNode(_ColNm).InnerXml.Trim();
            return obj;
        }
        private ArrayList SetCreateAccountParams(string XMLControlData)
        {
            ArrayList spParms = new ArrayList();
            
            //Account oAccount = Account.FromXml(XMLControlData);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(XMLControlData);
            XmlNode xnd = xDoc.DocumentElement.SelectSingleNode("UIVALUES");
            spParms.Add(0);
            spParms.Add(1);
            spParms.Add(GetValue(xnd, "AccountCode"));
            spParms.Add(GetValue(xnd, "AccountName"));
            spParms.Add(GetValue(xnd, "AliasName"));
            spParms.Add(GetValue(xnd, "AccountStatus"));
            spParms.Add(GetValue(xnd, "AccountType"));
            spParms.Add(GetValue(xnd, "CreditDays"));
            spParms.Add(GetValue(xnd, "CreditLimit"));
            spParms.Add(GetValue(xnd, "PurchaseAccount"));
            spParms.Add(GetValue(xnd, "SalesAccount"));
            spParms.Add(GetValue(xnd, "IsBillwise "));
            spParms.Add(1); //GUID
            spParms.Add(1);//CREATED BY
            //spParms.Add("@strNewGUID");
            return spParms;
        }
        private ArrayList SetCreateDepreciationParams(string XMLControlData)
        {
            ArrayList spParms = new ArrayList();

            //Account oAccount = Account.FromXml(XMLControlData);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(XMLControlData);
            XmlNode xnd = xDoc.DocumentElement.SelectSingleNode("UIVALUES");
            spParms.Add(0);
            spParms.Add(GetValue(xnd, "PLAccountID"));
            spParms.Add(GetValue(xnd, "BSAccountID"));
            spParms.Add(GetValue(xnd, "DepreciationMethodID"));
            spParms.Add(GetValue(xnd, "PurchaseValue"));
            spParms.Add(GetValue(xnd, "CurrentValue"));
            spParms.Add(GetValue(xnd, "DepreciationRate"));
            spParms.Add(GetValue(xnd, "FirstUsageDate"));
            spParms.Add(1); //GUID
            spParms.Add(1);//CREATED BY
            //spParms.Add("@strNewGUID");
            return spParms;
        }
        private ArrayList SetCreateProductParams(NameValueCollection _data)
        {
            ArrayList spParms = new ArrayList();
            return spParms;
        }


        public DataSet GetScreenInfoByID(int ScreenID, string strCulture, string CompanyIndex)
        {
            ArrayList param = new ArrayList();
            param.Add(ScreenID);
            param.Add(strCulture);
            try
            {
                DbUtilResult dbResult = DBUtil.Execute("SPGetScreenByFeatureID", param, DBUtil.GetConnection(CompanyIndex));
                return dbResult.Contents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }

}

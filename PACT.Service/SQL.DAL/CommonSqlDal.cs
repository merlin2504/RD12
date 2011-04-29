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
                DbUtilResult dbResult = DBUtil.Execute("SPCreateAccount", Params, DBUtil.GetConnection(CompanyIndex));
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

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(XMLControlData);
            XmlNode xnd = xDoc.DocumentElement.SelectSingleNode("UIVALUES");
            spParms.Add(-10);//NodePositionSeqno
            spParms.Add(GetValue(xnd, "Code"));
            spParms.Add(GetValue(xnd, "Name"));
            spParms.Add(GetValue(xnd, "Type1"));            
            //spParms.Add(GetValue(xnd, "Status"));
            //spParms.Add(GetValue(xnd, "Address1"));
            //spParms.Add(GetValue(xnd, "Address2"));
            //spParms.Add(GetValue(xnd, "Address3"));
            spParms.Add(GetValue(xnd, "Alias"));
            //spParms.Add(GetValue(xnd, "ContactPerson"));
            //spParms.Add(GetValue(xnd, "CreditDays"));
            //spParms.Add(GetValue(xnd, "CreditLimit"));
            //spParms.Add(GetValue(xnd, "DebitDays"));
            //spParms.Add(GetValue(xnd, "DebitLimit"));
            //spParms.Add(GetValue(xnd, "Email"));
            //spParms.Add(0); //ParentNo
            //spParms.Add(GetValue(xnd, "PhoneNo"));
            //spParms.Add(GetValue(xnd, "Faxno"));
            //spParms.Add(GetValue(xnd, "SalesAccount"));
            //spParms.Add(GetValue(xnd, "PurchaseAccount"));
            //spParms.Add(1);//CreatedBy
            //spParms.Add("");//ExtraFieldUpdateQuery
            //spParms.Add("Accounts");//TableName
            //spParms.Add("");//ExtendedPrimaryColumn
            //spParms.Add(0);//iSeqno
            //spParms.Add("");//PrimaryColumn
            //spParms.Add("");//strGuid
            spParms.Add("@strNewGUID");
            return spParms;
        }
        private ArrayList SetCreateProductParams(NameValueCollection _data)
        {
            ArrayList spParms = new ArrayList();
            return spParms;
        }

      
        public DataSet GetScreenInfoByID(int ScreenID,string CompanyIndex)
        {
            ArrayList param = new ArrayList();
            param.Add(ScreenID);
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

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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Common" in code, svc and config file together.
    public class Common : ICommon
    {
        public string GetCommonData(int value)
        {
            return string.Format("Message from pact common wcf service: {0}", value);
        }

        public int CreateAccount(string XMLControlData, string CompanyIndex)
        {
            int returnValue = 0;
            string DbType = System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];
            if (!String.IsNullOrEmpty(DbType) && DbType.Equals("SQL"))
            {
                CommonSqlDal objCommon = new CommonSqlDal();
                returnValue = objCommon.CreateAccount(XMLControlData,CompanyIndex);
            }
            return returnValue;
        }


        public int CreateProduct(string XMLControlData, string CompanyIndex)
        {
            int returnValue = 0;
            string DbType = System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];
            if (!String.IsNullOrEmpty(DbType) && DbType.Equals("SQL"))
            {
                CommonSqlDal objCommon = new CommonSqlDal();
                returnValue = objCommon.CreateProduct(XMLControlData, CompanyIndex);
            }
            return returnValue;
        }


        public DataTable ExecuteQuery(string Query, string CompanyIndex)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string DbType = System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];
            if (!String.IsNullOrEmpty(DbType) && DbType.Equals("SQL"))
            {
                CommonSqlDal objCommon = new CommonSqlDal();
                ds = objCommon.ExecuteQuery(Query, CompanyIndex);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
            
        }

        public DataSet GetScreenInfoByID(int ScreenID,string CompanyIndex)
        {
            DataSet ds = new DataSet();
            string DbType=System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];
            if (!String.IsNullOrEmpty(DbType) && DbType.Equals("SQL"))
            {
                CommonSqlDal objCommon = new CommonSqlDal();
                ds = objCommon.GetScreenInfoByID(ScreenID, CompanyIndex);
            }
            return ds;
        }
    }
}

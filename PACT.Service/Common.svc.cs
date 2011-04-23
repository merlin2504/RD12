using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace PACT.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Common" in code, svc and config file together.
    public class Common : ICommon
    {
        public string GetCommonData(int value)
        {
            return string.Format("Message from pact common wcf service: {0}", value);
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

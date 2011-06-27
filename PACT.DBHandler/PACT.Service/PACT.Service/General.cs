using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

namespace PACT.Service
{
    public class General
    {
        private static string DataBase = "";
        public General()
        {
            if (DataBase == null || DataBase.Equals(""))
            {

                DataBase = System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];

            }
        }
        public DataSet Get(int CompanyIndex, ArrayList param, string SPName)
        {
            DataSet ds = null;

            try
            {
                if (DataBase.Equals("SQL"))
                {
                    SQLDBResult dbResult = SQLAdapter.Execute(SPName, param, SQLAdapter.GetConnection(CompanyIndex));
                    ds = dbResult.Contents;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }

        public string Set(int CompanyIndex, ArrayList param, string SPName, out long ReturnValue)
        {
            ReturnValue = 0;
            try
            {
                if (DataBase.Equals("SQL"))
                {
                    SQLDBResult dbResult = SQLAdapter.Execute(SPName, param, SQLAdapter.GetConnection(CompanyIndex));
                    ReturnValue = Convert.ToInt64(dbResult.Parameters["@RETURN_VALUE"]);
                    if (ReturnValue > 0)
                        return "Success";
                    else
                    {
                        if (dbResult.Contents.Tables.Count > 0 && dbResult.Contents.Tables[0].Rows.Count > 0)
                            return "Error Message :" + dbResult.Contents.Tables[0].Rows[0]["ErrorMessage"].ToString()
                                + "\n Error Number :" + dbResult.Contents.Tables[0].Rows[0]["ErrorNumber"].ToString()
                                + "\n ProcedureName :" + dbResult.Contents.Tables[0].Rows[0]["ProcedureName"].ToString()
                                + "\n ErrorLine :" + dbResult.Contents.Tables[0].Rows[0]["ErrorLine"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return string.Empty;
        }



    }
}
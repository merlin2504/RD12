using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace PACT.Service
{
    public class CommonSqlDal
    {
      
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PACT.DBHandler;
using System.Data;
using System.Collections;

namespace PACT.COMMON
{
    public class Ptree
    {
        public DataSet GetCostCenterGridViewList(int CompanyIndex, int CostCenterID, int UserID)
        {
            ArrayList Param = new ArrayList();
            Param.Add(CostCenterID);
            Param.Add(UserID);

            return new PACT.DBHandler.DBHandler().GetCostCenterGridViewList(CompanyIndex, Param);
        }

        public DataSet GetCostCenterSummary(int CompanyIndex, int GridViewID, string strCols, int startPosition, int pagesize)
        {
            ArrayList Param = new ArrayList();
            Param.Add(GridViewID);
            Param.Add(strCols);
            Param.Add(startPosition);
            Param.Add(pagesize);
            Param.Add(1);

            return new PACT.DBHandler.DBHandler().GetCostCenterSummary(CompanyIndex, Param);
        }

    }
   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Collections;

namespace PACT.Service
{
    public class PactWebService : IPactWebService
    {
        public DataSet Get(int CompanyIndex, ArrayList param, string spName)
        {
            return new General().Get(CompanyIndex, param, spName);
        }

        public string Set(int CompanyIndex, ArrayList param, string spName,out long  ReturnValue)
        {
            return new General().Set(CompanyIndex, param, spName, out ReturnValue);
        }


    }
}

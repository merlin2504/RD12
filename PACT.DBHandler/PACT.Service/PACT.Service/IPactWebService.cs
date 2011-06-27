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
    [ServiceContract]
    public interface IPactWebService
    {
        [OperationContract]
        DataSet Get(int CompanyIndex, ArrayList param, string spName);

        [OperationContract]
        string Set(int CompanyIndex, ArrayList param, string spName,out long ReturnValue);
    }



}

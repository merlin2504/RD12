using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace PACT.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICommon" in both code and config file together.
    [ServiceContract]
    public interface ICommon
    {
        [OperationContract]
        string GetCommonData(int value);

        [OperationContract]
        DataSet GetScreenInfoByID(int ScreenID, string CompanyIndex);

    }
}

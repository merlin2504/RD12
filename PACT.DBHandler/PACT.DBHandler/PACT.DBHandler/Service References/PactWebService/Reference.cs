﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PACT.DBHandler.PactWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PactWebService.IPactWebService")]
    public interface IPactWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPactWebService/Get", ReplyAction="http://tempuri.org/IPactWebService/GetResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        System.Data.DataSet Get(int CompanyIndex, object[] param, string spName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPactWebService/Set", ReplyAction="http://tempuri.org/IPactWebService/SetResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        string Set(out long ReturnValue, int CompanyIndex, object[] param, string spName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPactWebServiceChannel : PACT.DBHandler.PactWebService.IPactWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PactWebServiceClient : System.ServiceModel.ClientBase<PACT.DBHandler.PactWebService.IPactWebService>, PACT.DBHandler.PactWebService.IPactWebService {
        
        public PactWebServiceClient() {
        }
        
        public PactWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PactWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PactWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PactWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet Get(int CompanyIndex, object[] param, string spName) {
            return base.Channel.Get(CompanyIndex, param, spName);
        }
        
        public string Set(out long ReturnValue, int CompanyIndex, object[] param, string spName) {
            return base.Channel.Set(out ReturnValue, CompanyIndex, param, spName);
        }
    }
}

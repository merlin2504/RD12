using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.Practices.Prism.Commands;
using PACT.COMMON;
using PACT.DBHandler;
using System.Windows.Input;
using System.ServiceModel;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using System.Configuration;
using PACT.COMMON;

namespace PACT.MODEL
{
    public class ControlGenerator
    {
        private ObservableCollection<PactControlData> _PactControlData;
        private DataSet _ds;
        public class Field
        {
            public string ID
            {
                get;
                set;
            }
            public string FType
            {
                get;
                set;
            }
            public string Control
            {
                get;
                set;
            }

            public string Label
            {
                get;
                set;
            }
            public bool IsMandatory
            {
                get;
                set;
            }
            public bool IsReadOnly
            {
                get;
                set;
            }
            public string DataType
            {
                get;
                set;
            }
            public string Db
            {
                get;
                set;
            }
            public int FeatureID
            {
                get;
                set;
            }
            //IsPartiralData
            [System.ComponentModel.Browsable(false)]
            public virtual string Xml
            {
                get
                {
                    return PACTSerializer.ToXml(this, this.GetType(), false);
                }
            }

            public static Field FromXml(string Xml)
            {
                return ((Field)(PACTSerializer.FromXml(Xml, typeof(Field))));
            }
        }

        private DataSet GetScreenInfo(int _ScreenID, string _CompanyIndex)
        {
            Logger.InfoLog("ControlGenerator:: Inside GetScreenInfo");
            try
            {
                PACT.DBHandler.DBHandler DBH = new DBHandler.DBHandler();
                ArrayList AL = new ArrayList();
                AL.Add(1);
                AL.Add("Majeed");
                AL.Add(0);

                DataSet ds = DBH.GetAddAccountScreenDetails(1, AL);

                return ds;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog("ControlGenerator:: GetScreenInfo" + ex.StackTrace.ToString());
                return null;
            }
        }

        //public ObservableCollection<PactControlData> GetControls(string ScreenID, string CompanyIndex)
        public DataSet GetControls(string ScreenID, string CompanyIndex)
        {
            Logger.InfoLog("ControlGenerator:: Inside GetControls");
            try
            {
                if (_ds == null)
                {              
                    _ds = GetScreenInfo(Convert.ToInt32(ScreenID), CompanyIndex);
                }
                return _ds;
            }
            catch(Exception ex)
            {
                Logger.ErrorLog("ControlGenerator:: GetControls" + ex.StackTrace.ToString());
                return null;
            }
        }

        public int PostData(string XMLControlData, string ScreenID, string CompanyIndex)
        {
            int ReturnDBVal = -1;
            CommonService.CommonClient wcfService = null;
            try
            {
                wcfService = new CommonService.CommonClient();
                wcfService.Open();
                switch (ScreenID)
                {
                    case "1":
                        ReturnDBVal = wcfService.CreateAccount(XMLControlData, CompanyIndex);
                        break;
                    case "4":
                        ReturnDBVal = wcfService.CreateDepreciation(XMLControlData, CompanyIndex);
                        break;
                    case "2000":
                        ReturnDBVal = wcfService.CreateProduct(XMLControlData, CompanyIndex);
                        break;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Error in ControlGenerator::GetLookupData::-->" + ex.StackTrace);
                Logger.ErrorLog("ControlGenerator:: PostData" + ex.StackTrace.ToString());
            }
            finally
            {
                if (wcfService != null)
                {
                    wcfService.Close();
                }
            }
            return ReturnDBVal;
        }

        public long AddAccountDetails(ArrayList AddAccountAL, int _CompanyIndex)
        {
            Logger.InfoLog("ControlGenerator:: Inside AddAccountDetails");
            long ReturnDBVal = -1;

            try
            {
                PACT.DBHandler.DBHandler DBH = new DBHandler.DBHandler();
                string s = DBH.SetAccountDetails(_CompanyIndex, AddAccountAL, out ReturnDBVal);
                Logger.InfoLog("ControlGenerator:: Success: DBH.SetAccountDetails " + ReturnDBVal.ToString());
                return ReturnDBVal;
                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog("ControlGenerator:: AddAccount Details" + ex.StackTrace.ToString());
                return ReturnDBVal;
            }

        }

        private static Dictionary<string, string> UIDbResources = new Dictionary<string, string>();

        //public string GetResource(string strKey)
        //{
        //    if (ViewResourceManager.IsKeyExists(strKey))
        //    {
        //        return ViewResourceManager.GetResource(strKey);
        //    }
        //    else
        //    {
        //        return strKey;
        //    }
        //}

    }
}

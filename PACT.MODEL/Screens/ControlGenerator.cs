using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.Practices.Prism.Commands;
using PACT.COMMON;
using System.Windows.Input;
using System.ServiceModel;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using System.Configuration;

namespace PACT.MODEL
{
    public class ControlGenerator
    {
        private ObservableCollection<PactControlData> _PactControlData;

        private XmlDocument GetScreenInfo(int _ScreenID, string _CompanyIndex)
        {
            XmlDocument xDoc = new XmlDocument();
            CommonService.CommonClient wcfService = null;
            try
            {

                var endpoint = new EndpointAddress(new Uri(ConfigurationManager.AppSettings["SERVICEURL"].ToString()));
                var binding = new WSHttpBinding();
                wcfService = new CommonService.CommonClient(binding, endpoint);
                //wcfService= new CommonService.CommonClient();
                wcfService.Open();
                DataSet ds = wcfService.GetScreenInfoByID(_ScreenID, _CompanyIndex);
                if (ds != null && ds.Tables.Count > 0)
                {

                    xDoc.LoadXml(ds.Tables[0].Rows[0]["ScreenXML"].ToString());
                }
                return xDoc;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ControlGenerator::GetScreenInfo::-->" + ex.StackTrace);
            }
            finally
            {
                if (wcfService != null)
                {
                    wcfService.Close();
                }
            }
        }
        private PactComboBoxData GetLookupData(PactComboBoxData _pcb,string LookupName, string CompanyIndex)
        {
            _pcb.ComboItems.Clear();
            string sqlQuery = "";
            switch (LookupName)
            {
                case "drpAccountType":
                    sqlQuery = "Select ID,AccountType from AccountTypes WITH(NOLOCK) order by ID";
                    break;
                case "drpAccountStatus":
                    sqlQuery = "Select ID,AccountStatus from AccountStatus WITH(NOLOCK) order by ID";
                    break;
                    
            }
            if (!sqlQuery.Equals(""))
            {
                CommonService.CommonClient wcfService = null;
                try
                {
                    wcfService = new CommonService.CommonClient();
                    wcfService.Open();
                    DataTable dt = wcfService.ExecuteQuery(sqlQuery, CompanyIndex);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _pcb.ComboItems.Add(new ComboBoxItemData(dt.Rows[i][1].ToString(),dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in ControlGenerator::GetLookupData::-->" + ex.StackTrace);
                }
                finally
                {
                    if (wcfService != null)
                    {
                        wcfService.Close();
                    }
                }
            }
            return _pcb;
        }
        public ObservableCollection<PactControlData> GetControls(string ScreenID, string CompanyIndex)
        {
            if (_PactControlData == null)
            {
                string strID = "";
                string strLable = "";
                    string strCommand = "";
                string strNm = "";
                    string IsMandatory = "";
                    string IsReadOnly = "";                    
                    string DataType = "";
                    string ColumnName = "";                    
                    
                XmlDocument xDoc = GetScreenInfo(Convert.ToInt32(ScreenID), CompanyIndex);
                _PactControlData = new ObservableCollection<PactControlData>();
                for (int iRow = 0; iRow < xDoc.DocumentElement.ChildNodes.Count; iRow++)
                {
                    XmlNode xSec = xDoc.DocumentElement.ChildNodes[iRow];
                    foreach (XmlNode xRow in xSec.ChildNodes)
                    {
                        foreach (XmlNode xTemp in xRow.ChildNodes)
                        {
                            foreach (XmlNode xRowInfo in xTemp.ChildNodes)
                            {
                                    IsReadOnly="True";
                                    IsMandatory = "0";
                                    DataType = "NA";
                                    ColumnName = "NA";
                                    if(xRowInfo.SelectSingleNode("ID")!=null)
                                        strID = xRowInfo.SelectSingleNode("ID").InnerText.ToString();
                                    if (xRowInfo.SelectSingleNode("Label") != null)
                                        strLable = xRowInfo.SelectSingleNode("Label").InnerText.ToString();
                                    if (xRowInfo.SelectSingleNode("Control") != null)
                                strNm = xRowInfo.SelectSingleNode("Control").InnerText.ToString();
                                    if (xRowInfo.SelectSingleNode("IsMandatory") != null)
                                        IsMandatory = xRowInfo.SelectSingleNode("IsMandatory").InnerText.ToString();
                                    if (xRowInfo.SelectSingleNode("DataType") != null)
                                        DataType = xRowInfo.SelectSingleNode("DataType").InnerText.ToString();
                                    if (xRowInfo.SelectSingleNode("Db") != null)
                                        ColumnName = xRowInfo.SelectSingleNode("Db").InnerText.ToString();
                                    if (xRowInfo.SelectSingleNode("IsReadOnly") != null)
                                    {
                                        IsReadOnly = xRowInfo.SelectSingleNode("IsReadOnly").InnerText.ToString();
                                        if (IsReadOnly.Equals("0"))
                                            IsReadOnly = "True";
                                        else
                                            IsReadOnly = "False";
                                    }
                                switch (strNm)
                                {
                                    case "TextBox":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = strLable,
                                            Style = "PACTTextBlockStyle"
                                        });
                                        _PactControlData.Add(new PactTextBoxData()
                                        {
                                                Label=strLable,
                                            Text = strID,
                                                Mandatory = IsMandatory,
                                                DataType = DataType,
                                                DBColumnName = ColumnName,
                                                Enable = IsReadOnly,
                                        });
                                        break;

                                    case "ComboBox":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = strLable,
                                        });

                                            PactComboBoxData CMB = new PactComboBoxData()
                                            {
                                                Label = strLable,
                                                Mandatory = IsMandatory,
                                                DataType = DataType,
                                                DBColumnName = ColumnName,
                                                Enable = IsReadOnly,
                                            };
                                  

                                            if(strID!=null)
                                                CMB = GetLookupData(CMB, strID, CompanyIndex);
                                            
                                            if(CMB.ComboItems.Count==0)
                                                CMB.ComboItems.Add(new ComboBoxItemData("0",strID));

                                        _PactControlData.Add(CMB);
                                        break;

                                    case "Button":
                                            strCommand = xRowInfo.SelectSingleNode("Command").InnerText.ToString();
                                        _PactControlData.Add(new PactButtonData()
                                        {
                                            Label = strLable,
                                                DynamicCommand = strCommand,
                                        });
                                        break;
                                    case "ListDrop":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = strLable,
                                            Style = "PACTTextBlockStyle"
                                        });
                                        _PactControlData.Add(new PactIntelliBoxData()
                                        {
                                            Text = strID,
                                            ListDropColumnResults = new ListDropResultsProvider()
                                        });
                                        
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return _PactControlData;
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
                    case "1000":
                        ReturnDBVal = wcfService.CreateAccount(XMLControlData, CompanyIndex);
                        break;
                    case "2000":
                        ReturnDBVal = wcfService.CreateProduct(XMLControlData, CompanyIndex);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ControlGenerator::GetLookupData::-->" + ex.StackTrace);
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
       
    
    }
}

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

namespace PACT.MODEL
{
    public class ControlGenerator
    {
        private ObservableCollection<PactControlData> _PactControlData;

        private XmlDocument GetScreenInfo(int _ScreenID,string _CompanyIndex,string _ServiceUrl)
        {
            XmlDocument xDoc = new XmlDocument();
            CommonService.CommonClient wcfService = null ;
            
            try
            {
                var endpoint = new EndpointAddress(new Uri(_ServiceUrl+"Common.svc"));
                var binding = new WSHttpBinding();
                wcfService= new CommonService.CommonClient(binding, endpoint);
                wcfService.Open();
                DataSet ds = wcfService.GetScreenInfoByID(_ScreenID,_CompanyIndex);
                if(ds!=null && ds.Tables.Count>0)
                {

                    xDoc.LoadXml(ds.Tables[0].Rows[0]["ScreenXML"].ToString());
                }
                return xDoc;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in ControlGenerator::GetScreenInfo::-->"+ex.StackTrace);
            }
            finally
            {
                if(wcfService!=null)
                {
                    wcfService.Close();
                }
            
            }
        }

        public ObservableCollection<PactControlData> GetControls(string ScreenID,string CompanyIndex,string ServiceUrl)
        {
                if (_PactControlData == null)
                {
                    string strID = "";
                    string strLable = "";
                    string strVal = "";
                    string strNm = "";
                    XmlDocument xDoc = GetScreenInfo(Convert.ToInt32(ScreenID),CompanyIndex,ServiceUrl);
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
                                    strID = xRowInfo.SelectSingleNode("ID").InnerText.ToString();
                                    strLable = xRowInfo.SelectSingleNode("Label").InnerText.ToString();
                                    strNm = xRowInfo.SelectSingleNode("Control").InnerText.ToString();
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
                                                Text = strID,
                                            });
                                            break;

                                        case "ComboBox":
                                            _PactControlData.Add(new PactTextBlockData()
                                            {
                                                Text = strLable,
                                            });
                                            PactComboBoxData CMB = new PactComboBoxData();
                                            CMB.ComboItems.Add(strID);
                                            _PactControlData.Add(CMB);
                                            break;

                                        case "Button":
                                            _PactControlData.Add(new PactButtonData()
                                            {
                                                Label = strLable,
                                                DynamicCommand = "OnSave",
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
    }
}

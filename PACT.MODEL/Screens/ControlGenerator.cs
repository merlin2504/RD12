using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.Practices.Prism.Commands;
using PACT.COMMON;
using System.Windows.Input;

namespace PACT.MODEL
{
    public class ControlGenerator
    {
        private ObservableCollection<PactControlData> _PactControlData;

        public ObservableCollection<PactControlData> GetControls(string ScreenID)
        {
            if (ScreenID == "123")
            {
                if (_PactControlData == null)
                {
                    _PactControlData = new ObservableCollection<PactControlData>();

                    _PactControlData.Add(new PactTextBoxData()
                    {
                        Label = "ABC",
                    });

                    _PactControlData.Add(new PactTextBoxData()
                    {
                        Label = "123",
                    });

                    _PactControlData.Add(new PactTextBoxData()
                    {
                        Label = "RED",
                    });

                    _PactControlData.Add(new PactTextBoxData()
                    {
                        Label = "Green",
                    });

                    PactComboBoxData CMB = new PactComboBoxData();
                    CMB.ComboItems.Add("Majeed");
                    CMB.ComboItems.Add("Majeed1");
                    CMB.ComboItems.Add("Majeed2");

                    _PactControlData.Add(CMB);

                }
            }
            else
            {
                if (_PactControlData == null)
                {
                    string strID = "";
                    string strLable = "";
                    string strVal = "";
                    string strNm = "";
                    XmlDocument xDoc = new XmlDocument();
                    _PactControlData = new ObservableCollection<PactControlData>();
                    //DisplayName = TempScreenID;
                    switch (ScreenID)
                    {
                        case "1000":
                            //DisplayName = "New Accounts";
                            xDoc.Load("AccountsScreen.xml");
                            break;
                        case "2000":
                            //DisplayName = "New Product";
                            xDoc.Load("ProductsScreen.xml");
                            break;
                        default:
                            //DisplayName = "Un-Mapped Screen";
                            xDoc.Load("DefaultScreen.xml");
                            break;
                    }
                      
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
            }
            return _PactControlData;
           
        }
    }
}

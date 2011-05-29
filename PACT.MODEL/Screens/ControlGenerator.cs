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

        public class Field
        {
            public string ID { get; set; }
            public string FType { get; set; }
            public string Control { get; set; }

            public string Label { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsReadOnly { get; set; }
            public string DataType { get; set; }
            public string Db { get; set; }
            public int FeatureID { get; set; }
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
                DataSet ds = wcfService.GetScreenInfoByID(_ScreenID, ViewResourceManager.strCulture, _CompanyIndex);
                if (ds != null && ds.Tables.Count > 0)
                {
                    xDoc.LoadXml(ds.Tables[0].Rows[0]["ScreenXML"].ToString());
                    UIDbResources.Clear();
                    if (ds.Tables.Count > 1)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            UIDbResources.Add(ds.Tables[1].Rows[i][0].ToString(), ds.Tables[1].Rows[i][1].ToString());
                        }
                    }

                }


                //UIDbResources
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

        //NO Need
        //private PactComboBoxData GetLookupData(PactComboBoxData _pcb, string LookupName, string CompanyIndex)
        //{
        //    _pcb.ComboItems.Clear();
        //    string sqlQuery = "";
        //    switch (LookupName)
        //    {
        //        case "drpAccountType":
        //            sqlQuery = "Select ID,AccountType from AccountTypes WITH(NOLOCK) order by ID";
        //            break;
        //        case "drpAccountStatus":
        //            sqlQuery = "Select ID,AccountStatus from AccountStatus WITH(NOLOCK) order by ID";
        //            break;

        //    }
        //    if (!sqlQuery.Equals(""))
        //    {
        //        CommonService.CommonClient wcfService = null;
        //        try
        //        {
        //            wcfService = new CommonService.CommonClient();
        //            wcfService.Open();
        //            DataTable dt = wcfService.ExecuteQuery(sqlQuery, CompanyIndex);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    _pcb.ComboItems.Add(new ComboBoxItemData(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error in ControlGenerator::GetLookupData::-->" + ex.StackTrace);
        //        }
        //        finally
        //        {
        //            if (wcfService != null)
        //            {
        //                wcfService.Close();
        //            }
        //        }
        //    }
        //    return _pcb;
        //}


        public ObservableCollection<PactControlData> GetControls(string ScreenID, string CompanyIndex)
        {
            if (_PactControlData == null)
            {              
                int FeatureID = 0;
                bool IsPartiralData = false;
                PactTextBoxData oText;

                XmlDocument xDoc = GetScreenInfo(Convert.ToInt32(ScreenID), CompanyIndex);


                _PactControlData = new ObservableCollection<PactControlData>();
                #region Sections
                Field oField;
                for (int iRow = 0; iRow < xDoc.DocumentElement.SelectNodes("Section").Count; iRow++)
                {
                    XmlNode xSec = xDoc.DocumentElement.ChildNodes[iRow];
                    foreach (XmlNode xRow in xSec.ChildNodes)
                    {
                        foreach (XmlNode xTemp in xRow.ChildNodes)
                        {
                            foreach (XmlNode xRowInfo in xTemp.ChildNodes)
                            {
                                oField = Field.FromXml(xRowInfo.OuterXml);
                                if (xRowInfo.SelectSingleNode("FeatureID") != null)
                                    FeatureID = Convert.ToInt32(xRowInfo.SelectSingleNode("FeatureID").InnerText.ToString());
                                else
                                    FeatureID = 0;
                                if (xRowInfo.SelectSingleNode("IsPartiralData") != null)
                                    IsPartiralData = Convert.ToBoolean(xRowInfo.SelectSingleNode("IsPartiralData").InnerText.ToString());
                                else
                                    IsPartiralData = false;
                                switch (oField.Control)
                                {
                                    case "TextBox":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = ViewResourceManager.GetResource(oField.Label, UIDbResources),
                                            Style = "PACTTextBlockStyle"
                                        });
                                        oText = new PactTextBoxData()
                                        {
                                            Label = oField.Label,
                                            //Text = strID,
                                            Mandatory = oField.IsMandatory,
                                            DataType = oField.DataType,
                                            DBColumnName = oField.Db,
                                            Enable = !oField.IsReadOnly,
                                            
                                        };
                                        if (oText.DataType == "INT") { oText.Text = "0"; oText.Align = "Right"; }
                                        else if (oText.DataType == "FLOAT") { oText.Text = "0.00"; oText.Align = "Right"; }

                                        _PactControlData.Add(oText);
                                        break;
                                    case "CheckBox":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = "",
                                            Style = "PACTTextBlockStyle"
                                        });
                                        _PactControlData.Add(new PactCheckBoxData()
                                        {
                                            Label = oField.Label,
                                            Mandatory = oField.IsMandatory,
                                            DataType = oField.DataType,
                                            DBColumnName = oField.Db,
                                            Enable = !oField.IsReadOnly,
                                        });
                                        break;
                                    case "DatePicker":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = ViewResourceManager.GetResource(oField.Label, UIDbResources),
                                            Style = "PACTTextBlockStyle"
                                        });
                                        _PactControlData.Add(new PactDatePickerData()
                                        {
                                            Date = System.DateTime.Now.AddDays(-20),
                                            Label = oField.Label,
                                            Text = "HI",
                                            Mandatory = oField.IsMandatory,
                                            DataType = oField.DataType,
                                            DBColumnName = oField.Db,
                                            Enable = !oField.IsReadOnly,
                                        });
                                        break;
                                    case "ComboBox":
                                        _PactControlData.Add(new PactTextBlockData()
                                        {
                                            Text = ViewResourceManager.GetResource(oField.Label, UIDbResources),
                                        });

                                        PactComboBoxData CMB = new PactComboBoxData()
                                        {
                                            IsPartiralData=IsPartiralData,
                                            FeatureID = FeatureID,
                                            CompanyIndex = Convert.ToInt32(CompanyIndex),
                                            Label = oField.Label,
                                            Mandatory = oField.IsMandatory,
                                            DataType = oField.DataType,
                                            DBColumnName = oField.Db,
                                            Enable = !oField.IsReadOnly,
                                        };
                                        _PactControlData.Add(CMB);
                                        break;

                                    case "Button":
                                       string strCommand = xRowInfo.SelectSingleNode("Command").InnerText.ToString();
                                        _PactControlData.Add(new PactButtonData()
                                        {
                                            Label = ViewResourceManager.GetResource(oField.Label, UIDbResources),
                                            DynamicCommand = strCommand,
                                        });
                                        break;
                                }
                            }
                        }
                    }
                }
                #endregion Sections
                #region Grid
                XmlNode xGrd = xDoc.DocumentElement.SelectSingleNode("Grid//Columns");
                if (xGrd != null)
                {
                    DataTable GridDt = new DataTable();
                    PactGridData PactGrid = new PactGridData();
                    DgColumn PactGridCols = new DgColumn();
                    PactGrid.Columns = new ObservableCollection<DgColumn>();
                    int BlankRows = 0;
                    XmlNode xProp = xDoc.DocumentElement.SelectSingleNode("Grid//Properties");
                    if (xProp.SelectSingleNode("Rows") != null)
                        BlankRows = int.Parse(xProp.SelectSingleNode("Rows").InnerText.ToString());


                    foreach (XmlNode xCols in xGrd.ChildNodes)
                    {
                        DgColumn oCol = DgColumn.FromXml(xCols.OuterXml);
                        oCol.DisplayMember = oCol.ID;
                        PactGrid.Columns.Add(oCol);

                        switch (oCol.DataType)
                        {
                            case "INT":
                                GridDt.Columns.Add(oCol.ID, typeof(int));
                                break;
                            case "FLOAT":
                                GridDt.Columns.Add(oCol.ID, typeof(float));
                                break;
                            case "PACTCOMBO":
                                GridDt.Columns.Add(oCol.ID);
                                GridDt.Columns.Add(oCol.ID + "_Key", typeof(int));
                                break;
                            case "TEXT":
                                GridDt.Columns.Add(oCol.ID);
                                break;
                            case "DATE":
                                GridDt.Columns.Add(oCol.ID, typeof(DateTime));
                                break;
                        }
                    }

                    if (BlankRows != 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < BlankRows; i++)
                        {
                            dr = GridDt.NewRow();
                            dr["Sno"] = i + 1;
                            GridDt.Rows.Add(dr);
                        }
                    }

                    //                CommonService.CommonClient wcfService = new CommonService.CommonClient();
                    //                GridDt = wcfService.ExecuteQuery(@"SELECT Top 100 ROW_NUMBER() OVER(ORDER BY NodeNo DESC) AS Sno,A.Name AccountName,A.NodeNo AccountName_Key,1000 Amount
                    //,'Nar' As Narration, T.AccountType AccountType, T.ID AccountType_Key
                    // FROM Accounts A INNER JOIN AccountTypes T ON T.ID=A.Type1 WHERE A.NodeNo>1000", CompanyIndex);
                    //                wcfService.Close();
                    PactGrid.DataSource = GridDt;
                    _PactControlData.Add(PactGrid);
                }
               
                #endregion Grid
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

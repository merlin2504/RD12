using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Data;

namespace PACT.COMMON
{
    public class Util
    {
        public void InfoMessageBox(string Message, string Caption)
        {
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage image = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(Message, Caption, buttons, image);
        }
        private DataTable BuildDBColumns(ICollection<PactControlData> _controlData)
        {
            DataTable dt = new DataTable();
            PactControlData objPactCtrl;
            PactTextBoxData objPactTB;
            PactComboBoxData objPactCmb;
            PactCheckBoxData objPactChk;
            PactDatePickerData objPactDp;
            if (_controlData != null && _controlData.Count > 0)
            {
                for (int i = 0; i < _controlData.Count; i++)
                {
                    objPactCtrl = _controlData.ElementAt<PactControlData>(i);
                    if (objPactCtrl.GetType().Name.ToString().Equals("PactTextBoxData"))
                    {
                        objPactTB = (PactTextBoxData)objPactCtrl;
                        if (objPactTB.DBColumnName != null)
                        {
                            dt.Columns.Add(objPactTB.DBColumnName.Trim());
                        }

                    }
                    else if (objPactCtrl.GetType().Name.ToString().Equals("PactComboBoxData"))
                    {
                        objPactCmb = (PactComboBoxData)objPactCtrl;
                        if (objPactCmb.DBColumnName != null)
                        {
                            dt.Columns.Add(objPactCmb.DBColumnName.Trim());
                        }
                    }
                    else if (objPactCtrl.GetType().Name.ToString().Equals("PactCheckBoxData"))
                    {
                        objPactChk = (PactCheckBoxData)objPactCtrl;
                        if (objPactChk.DBColumnName != null)
                        {
                            dt.Columns.Add(objPactChk.DBColumnName.Trim());
                        }
                    }
                    else if (objPactCtrl.GetType().Name.ToString().Equals("PactDatePickerData"))
                    {
                        objPactDp = (PactDatePickerData)objPactCtrl;
                        if (objPactDp.DBColumnName != null)
                        {
                            dt.Columns.Add(objPactDp.DBColumnName.Trim());
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable GetDBValues(ICollection<PactControlData> _controlData)
        {
            DataTable DbValues = BuildDBColumns(_controlData);
            DbValues.TableName = "UIVALUES";
            PactControlData objPactCtrl;
            PactTextBoxData objPactTB;
            PactComboBoxData objPactCmb;
            PactCheckBoxData objPactChk;
            PactDatePickerData objPactDp;
            DataRow dr = DbValues.NewRow();
            if (_controlData != null && _controlData.Count > 0)
            {
                for (int i = 0; i < _controlData.Count; i++)
                {
                    objPactCtrl = _controlData.ElementAt<PactControlData>(i);
                    if (objPactCtrl.GetType().Name.ToString().Equals("PactTextBoxData"))
                    {
                        objPactTB = (PactTextBoxData)objPactCtrl;
                        if (objPactTB.DBColumnName != null)
                        {
                            if (objPactTB.Text != null)
                            {
                                dr[objPactTB.DBColumnName.Trim()] = objPactTB.Text.Trim();
                            }
                        }
                    }
                    else if (objPactCtrl.GetType().Name.ToString().Equals("PactComboBoxData"))
                    {
                        objPactCmb = (PactComboBoxData)objPactCtrl;
                        if (objPactCmb.DBColumnName != null)
                        {
                            if (objPactCmb.SelectedValue != null)
                            {
                                dr[objPactCmb.DBColumnName.Trim()] = objPactCmb.SelectedValue.Trim();
                            }
                        }
                    }
                    else if (objPactCtrl.GetType().Name.ToString().Equals("PactCheckBoxData"))
                    {
                        objPactChk = (PactCheckBoxData)objPactCtrl;
                        if (objPactChk.DBColumnName != null)
                        {
                            dr[objPactChk.DBColumnName.Trim()] = objPactChk.IsChecked;
                        }
                    }
                    else if (objPactCtrl.GetType().Name.ToString().Equals("PactDatePickerData"))
                    {
                        objPactDp = (PactDatePickerData)objPactCtrl;
                        if (objPactDp.DBColumnName != null)
                        {
                            dr[objPactDp.DBColumnName.Trim()] = objPactDp.Date;
                        }
                    }     
                }
            }
            DbValues.Rows.Add(dr);
            return DbValues;
        }
    }
}

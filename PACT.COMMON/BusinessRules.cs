using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.ObjectModel;

namespace PACT.COMMON
{
    public class BusinessRules
    {
        public bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        public bool ValidateInt(string val) 
        { 
            try 
            { 
                int i = int.Parse(val); 
                return (true); 
            } 
            catch (Exception) 
            { 
                return (false); 
            } 
        }

        public bool ValidateDate(string val) 
        { 
            try 
            { 
                DateTime d = DateTime.Parse(val);
                return (true); 
            } 
            catch (Exception) 
            {
                return (false); 
            } 
        }

        public bool ValidateDecimal(string val)
        { 
            try 
            { 
                float val1 = float.Parse(val);
                return true;
            } 
            catch (Exception)
            { 
                return (false); 
            } 
        }
        public bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public string ValidateUIInfo(ICollection<PactControlData> UIControls)
        {
            string strReturn= "";
            PactControlData objPactCtrl;
            PactTextBoxData objPactTB;
            PactComboBoxData objPactCmb;
            if (UIControls != null && UIControls.Count > 0)
            {
                for (int i = 0; i < UIControls.Count; i++)
                {
                    objPactCtrl = UIControls.ElementAt<PactControlData>(i);
                    if (objPactCtrl.GetType().Name.ToString().Equals("PactTextBoxData"))
                    {
                        objPactTB = (PactTextBoxData)objPactCtrl;
                        //Mandatory Check
                        if (objPactTB.Mandatory!=null && objPactTB.Mandatory.Equals("1"))
                        {
                            if (IsStringMissing(objPactTB.Text))
                            {
                                strReturn = strReturn + objPactTB.Label + "  :: Cannot be blank. \n";
                            }
                        }

                        //Data Check
                        if (objPactTB.DataType != null && objPactTB.DataType.Equals("INT"))
                        {
                            if (!ValidateInt(objPactTB.Text))
                            {
                                strReturn = strReturn + objPactTB.Label + "  :: Invalid data, accepts only numeric [Integer]. \n";
                            }
                        }
                        if (objPactTB.DataType != null && objPactTB.DataType.Equals("FLOAT"))
                        {
                            if (!ValidateDecimal(objPactTB.Text))
                            {
                                strReturn = strReturn + objPactTB.Label + "  :: Invalid data, accepts only numeric [Decimal]. \n";
                            }
                        }
                        if (objPactTB.DataType != null && objPactTB.DataType.Equals("DATE"))
                        {
                            if (!ValidateDate(objPactTB.Text))
                            {
                                strReturn = strReturn + objPactTB.Label + "  :: Invalid data, accepts only date. \n";
                            }
                        }
                        if (objPactTB.DataType != null && objPactTB.DataType.Equals("EMAIL"))
                        {
                            if (!IsValidEmailAddress(objPactTB.Text))
                            {
                                strReturn = strReturn + objPactTB.Label + "  :: Invalid data, accepts only email. \n";
                            }
                        }
                    }

                    if (objPactCtrl.GetType().Name.ToString().Equals("PactComboBoxData"))
                    {
                        objPactCmb = (PactComboBoxData)objPactCtrl;
                        //Mandatory Check
                        if (objPactCmb.Mandatory != null && objPactCmb.Mandatory.Equals("1"))
                        {
                            if (IsStringMissing(objPactCmb.IsSelected))
                            {
                                strReturn = strReturn + objPactCmb.Label + "  :: Cannot be blank. \n";
                            }
                        }
                    }

                    if (objPactCtrl.GetType().Name.ToString().Equals("PactGridData"))
                    {
                        PactGridData objGrid = (PactGridData)objPactCtrl;
                        DataTable dt = objGrid.DataSource;
                    }
                }
            }
            return strReturn;

        }
    }
}

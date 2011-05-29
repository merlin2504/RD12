using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls;
using System.Windows;

namespace PACT.COMMON
{
    public class Account
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AliasName { get; set; }
        public int AccountStatus { get; set; }
        public int AccountType { get; set; }
        public int CreditDays { get; set; }
        public double CreditLimit { get; set; }
        public int PurchaseAccount { get; set; }
        public int SalesAccount { get; set; }
        public int ParentAccountID { get; set; }
        public bool IsBillwise { get; set; }
        public int CreatedBy { get; set; }
        public string GUID { get; set; }

        [Browsable(false)]
        public virtual string Xml
        {
            get
            {
                return PACTSerializer.ToXml(this, this.GetType(), false);
            }
        }

        public static Account FromXml(string Xml)
        {
            return ((Account)(PACTSerializer.FromXml(Xml, typeof(Account))));
        }
    }
   
}

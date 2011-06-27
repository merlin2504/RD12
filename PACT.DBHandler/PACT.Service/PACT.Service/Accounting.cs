using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace PACT.Service
{
    public class Accounting
    {
        private static string DataBase = "";
        public Accounting()
        {
            if (DataBase == null || DataBase.Equals(""))
            {
                DataBase = System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];
            }
        } 

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace PACT.Service
{
    public class Common
    {
        private static string DataBase = "";
        public Common()
        {
            if (DataBase == null || DataBase.Equals(""))
            {
                DataBase = System.Configuration.ConfigurationManager.AppSettings["DBTYPE"];
            }
        }     

    }
}

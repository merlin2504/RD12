using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PACT.VIEWMODEL;
using FeserWard.Controls;
using Microsoft.Windows.Controls;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for DefaultScreen.xaml
    /// </summary>
    public partial class DefaultScreen : UserControl
    {

        //public IIntelliboxResultsProvider SingleColumnResults
        //{
        //    get;
        //    private set;
        //}

        public DefaultScreen()
        {
            //SingleColumnResults = new SingleColumnResultsProvider();
            this.InitializeComponent();


//            System.Data.SqlClient.SqlConnection oCon = new System.Data.SqlClient.SqlConnection();
//            oCon.ConnectionString = @"Data Source=VINAY\\SQLEXPRESS;Initial Catalog=master;Persist Security Info=True;
//User ID=sa;Password=Sql2008";
//            oCon.Open();

        }
    }
}
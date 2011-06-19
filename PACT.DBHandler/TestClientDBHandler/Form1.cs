using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PACT.DBHandler;
using System.Collections;

namespace TestClientDBHandler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList param = new ArrayList();
            param.Add(19);
            DataSet ds = new DBHandler().GetAccountDetails(1, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            ArrayList param = new ArrayList();
            param.Add(0);
            param.Add("001");
            param.Add("Ikram");
            param.Add("Hussain");
            param.Add("1");
            param.Add("2");
            param.Add(19);
            param.Add(0);
            param.Add(0);
            param.Add(0);
            param.Add(0);
            param.Add(0);
            param.Add(0);
            param.Add(0);
            param.Add("Sample");
            param.Add("ikmra");
            param.Add(1);
            param.Add("UserCol1='asdfasdf'");

            try
            {
                DBHandler db = new DBHandler();
                long accountId;
                string Message = db.SetAccountDetails(1, param, out accountId);

                MessageBox.Show(Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList param = new ArrayList();
                param.Add(19);

                DBHandler db = new DBHandler();
                long accountId;
                string Message = db.DeleteAccount(1, param, out accountId);
                MessageBox.Show(Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TestSP obj = new TestSP();
            obj.Show();
        }
    }
}

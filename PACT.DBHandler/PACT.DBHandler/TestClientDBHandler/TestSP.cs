using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using PACT.DBHandler;

namespace TestClientDBHandler
{
    public partial class TestSP : Form
    {
        public TestSP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DBHandler().Getter(1, GetParams(), textBox1.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                dataGridView1.Visible = true;
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            long ID;
            try
            {
                string Message = new DBHandler().settter(1, GetParams(), textBox1.Text, out ID);
                MessageBox.Show(Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        ArrayList GetParams()
        {
            ArrayList param = new ArrayList();
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                param.Add(((TextBox)tableLayoutPanel1.Controls[(i * 2) + 1]).Text);
            }
            return param;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();

            ArrayList param = new ArrayList();
            param.Add(textBox1.Text);
            DataSet ds = new DBHandler().GetMetaData(1, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                tableLayoutPanel1.RowCount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Label lbl = new Label();
                    lbl.Text = ds.Tables[0].Rows[i]["Parameter_name"].ToString().Substring(1) + "  (" + ds.Tables[0].Rows[i]["Type"].ToString() + ")";
                    lbl.Size = new System.Drawing.Size(300, 30);
                    tableLayoutPanel1.Controls.Add(lbl, 0, i);


                    TextBox txt = new TextBox();
                    if (ds.Tables[0].Rows[i]["Type"].ToString().ToLower() == "bigint" || ds.Tables[0].Rows[i]["Type"].ToString().ToLower() == "int" || ds.Tables[0].Rows[i]["Type"].ToString().ToLower() == "float")
                        txt.Text = "0";
                    if (ds.Tables[0].Rows[i]["Type"].ToString().ToLower() == "bit")
                        txt.Text = "false";
                    tableLayoutPanel1.Controls.Add(txt, 1, i);
                }
            }
        }
    }
}

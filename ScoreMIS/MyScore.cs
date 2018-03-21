using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScoreMIS.App_Code;
using System.Data.SqlClient;

namespace ScoreMIS
{
    public partial class MyScore : Form
    {
        public MyScore()
        {
            InitializeComponent();
        }

        private void MyScore_Load(object sender, EventArgs e)
        {
            //初始化学期
            cmbTerm.Items.Clear();
            cmbTerm.Items.Add("2012-2013第一学期");
            cmbTerm.Items.Add("2012-2013第二学期");
            cmbTerm.Items.Add("2013-2014第一学期");
            cmbTerm.Items.Add("2013-2014第二学期");
            //初始化学号
            txtID.Text = ShareClass.Name;

        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();
        DataSet myds = null;
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            mycommand = mycon.CreateCommand();
            if (cmbTerm.SelectedIndex == -1)
            {
                mycommand.CommandText = "DisplayScore '" + ShareClass.ID + "','0'";
            }
            else
            {                
                mycommand.CommandText = "DisplayScore '" + ShareClass.ID + "','" + cmbTerm.SelectedItem + "'";
            }
            myds = conclass.GetDataSet(mycommand.CommandText, "Score");
            dataGridView1.DataSource = myds.Tables["Score"];

        }
    }
}

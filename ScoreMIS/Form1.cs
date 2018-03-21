using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ScoreMIS.App_Code;

namespace ScoreMIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            //// TODO: 这行代码将数据加载到表“lookSharpDataSet.customer”中。您可以根据需要移动或移除它。
            //this.customerTableAdapter.Fill(this.lookSharpDataSet.customer);
            //SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
            //SqlCommand mycommand = null;
            //ConnectionClass conclass = new ConnectionClass();
            //DataSet myds = null;
            //mycommand = mycon.CreateCommand();
            //mycommand.CommandText = "select * from student";
            //myds = conclass.GetDataSet(mycommand.CommandText, "student");
            //dataGridView1.DataSource = myds.Tables["student"];
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Text = "触发Form的Click事件";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login l = new Login();
            l.ShowDialog();
        }
    }
}

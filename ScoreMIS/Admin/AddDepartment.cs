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

namespace ScoreMIS.Admin
{
    public partial class AddDepartment : Form
    {
        public AddDepartment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDepartID.Text.Trim()))
            {
                MessageBox.Show("请输入分院编号");
            }
            else
            {
                if (string.IsNullOrEmpty(txtDepartName.Text.Trim()))
                {
                    MessageBox.Show("请输入分院名称");
                }
                else
                {
                    SqlConnection mycon = null;
                    try
                    {
                        mycon = new SqlConnection(ConnectionClass.GetConStr);
                        SqlCommand mycommand = mycon.CreateCommand();
                        mycommand.CommandText = "insert into Department values(@DepartID,@DepartName)";
                        SqlParameter DepartID = new SqlParameter("@DepartID", SqlDbType.Int);
                        SqlParameter DepartName = new SqlParameter("@DepartName", SqlDbType.NVarChar);
                        mycommand.Parameters.Add(DepartID);
                        mycommand.Parameters.Add(DepartName);
                        DepartID.Value =txtDepartID.Text.Trim();
                        DepartName.Value = txtDepartName.Text.Trim();
                        mycon.Open();
                        mycommand.ExecuteNonQuery();
                        MessageBox.Show("添加完成");
                        txtDepartID.SelectAll();
                        txtDepartID.Focus();
                        //if (i != 0)
                        //{
                        //    MessageBox.Show("添加完成");
                        //}
                        //else
                        //{
                        //    MessageBox.Show("未添加");
                        //}
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("未添加成功，因为："+e1.Message);
                        //MessageBox.Show("数据库问题");
                    }
                    finally
                    {
                        mycon.Close();
                    }

                }
            }
        }
    }
}

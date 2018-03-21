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
    public partial class ModifyPwd : Form
    {
        public ModifyPwd()
        {
            InitializeComponent();
        }

        private void ModifyPwd_Load(object sender, EventArgs e)
        {
            txtID.Text = ShareClass.ID;            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
               MessageBox.Show("请输入新密码");
            }
            else
            {                
                if (txtPwd.Text.Trim() != txtPwd2.Text.Trim())
                {
                    MessageBox.Show("两次密码不一致");
                }
                else
                {
                    try
                    {
                        SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
                        SqlCommand mycommand = mycon.CreateCommand();
                        string sqlstr = "";
                        if (ShareClass.Type == "1")
                        {
                            sqlstr = "update Teacher";
                        }
                        else if (ShareClass.Type == "2")
                        {
                            sqlstr = "update Student";
                        }
                        mycommand.CommandText = sqlstr + " set Password=@pwd where ID=@ID";
                        SqlParameter Id = new SqlParameter("@ID", SqlDbType.NVarChar);
                        SqlParameter Pwd = new SqlParameter("@pwd", SqlDbType.NVarChar);
                        mycommand.Parameters.Add(Pwd);
                        mycommand.Parameters.Add(Id);                                           
                        Id.Value = txtID.Text.Trim();
                        Pwd.Value = txtPwd.Text.Trim();
                        mycon.Open();
                        int i = mycommand.ExecuteNonQuery();
                        mycon.Close();
                        if (i != 0)
                        {
                            MessageBox.Show("密码修改完成");
                        }
                        else
                        {
                            MessageBox.Show("密码修改错误");
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("数据库问题");
                    }

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

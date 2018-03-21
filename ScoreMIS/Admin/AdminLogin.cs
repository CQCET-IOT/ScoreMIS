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


namespace ScoreMIS.Admin
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }


        public void AdminShow()
        {
            AdminForm af = new AdminForm();
            Application.Run(af);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection mycon;
            mycon = new SqlConnection(App_Code.ConnectionClass.GetConStr);
            try
            {
               
                SqlCommand mycommand = mycon.CreateCommand();
                SqlDataReader mydr;
                mycon.Open();
                mycommand.CommandText = "select * from Admin where ID=@name and Password=@pwd";
                SqlParameter TName = new SqlParameter("@name", SqlDbType.NVarChar);
                SqlParameter TPwd = new SqlParameter("@pwd", SqlDbType.NVarChar);
                mycommand.Parameters.Add(TName);
                mycommand.Parameters.Add(TPwd);
                TName.Value = txtID.Text.Trim();
                TPwd.Value = txtPwd.Text.Trim();
                mydr = mycommand.ExecuteReader();
                if (mydr.HasRows)
                {
                    mydr.Read();
                    ShareClass.ID = mydr["ID"].ToString();
                    //ShareClass.Name = mydr["TeacherName"].ToString();
                    ShareClass.Type = "1";
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(AdminShow));
                    t.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名密码不匹配，请重新输入。");
                    txtPwd.Clear();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("连接问题");
            }
            finally
            {
                mycon.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

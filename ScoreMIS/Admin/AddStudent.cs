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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("请输入学号");
            }
            else
            {
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select ID from Student";
                SqlDataReader mydr = conclass.GetDataReader(mycommand.CommandText);
                if (mycon.State == ConnectionState.Closed)
                {
                    mycon.Open();
                }
                
                bool IDExists = false;
                while (mydr.Read())
                {
                    if (mydr["ID"].ToString().ToLower() == txtID.Text.Trim().ToLower())
                    {
                        IDExists = true;
                        mycon.Close();
                        break;
                    }
                }
                if (IDExists)
                {
                    MessageBox.Show("该学号已经存在，请重新输入");
                    txtID.SelectAll();
                    txtID.Focus();
                }
                else
                {
                    if(string.IsNullOrEmpty(txtName.Text))
                    {
                        MessageBox.Show("请输入姓名");
                    }
                    else
                    {
                        if (cmbClassID.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择班级");
                        }
                        else
                        {
                            string firstID=txtID.Text.Trim().Substring(0, 6);
                            if (cmbClassID.SelectedValue.ToString().Trim().ToLower() != firstID.ToLower())
                            {
                                MessageBox.Show("学号与班级号不对应，请检查");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtPhone.Text))
                                {
                                    MessageBox.Show("请输入联系方式");
                                }
                                else
                                    if (string.IsNullOrEmpty(txtIdenID.Text))
                                    {
                                        MessageBox.Show("请输入身份证号码");
                                    }
                                    else if (string.IsNullOrEmpty(txtAddress.Text))
                                    {
                                        MessageBox.Show("请输入通讯地址");
                                    }
                                    else
                                    {
                                        string sex = "";
                                        if (rbMale.Checked)
                                        {
                                            sex = "男";
                                        }
                                        else
                                        {
                                            sex = "女";
                                        }
                                       
                                        try
                                        {
                                            mycommand = mycon.CreateCommand();
                                            mycommand.CommandText = "insert into Student(ID,StudentName,ClassID,Sex,Phone,IdentityID,Address) values( @ID,@StudentName,@ClassID,@Sex,@Phone,@IdentityID,@Address)";
                                            SqlParameter Id = new SqlParameter("@ID", SqlDbType.NVarChar);
                                            SqlParameter StudentName = new SqlParameter("@StudentName", SqlDbType.NVarChar);
                                            SqlParameter ClassID = new SqlParameter("@ClassID", SqlDbType.NVarChar);
                                            SqlParameter Sex = new SqlParameter("@Sex", SqlDbType.NVarChar);
                                            SqlParameter Phone = new SqlParameter("@Phone", SqlDbType.NVarChar);
                                            SqlParameter IdentityID = new SqlParameter("@IdentityID", SqlDbType.NVarChar);
                                            SqlParameter Address = new SqlParameter("@Address", SqlDbType.NVarChar);
                                            mycommand.Parameters.Add(Id);
                                            mycommand.Parameters.Add(StudentName);
                                            mycommand.Parameters.Add(ClassID);
                                            mycommand.Parameters.Add(Sex);
                                            mycommand.Parameters.Add(Phone);
                                            mycommand.Parameters.Add(IdentityID);
                                            mycommand.Parameters.Add(Address);
                                            
                                            Id.Value = txtID.Text.Trim();
                                            StudentName.Value = txtName.Text.Trim();
                                            ClassID.Value = cmbClassID.SelectedValue.ToString().Trim();
                                            Sex.Value = sex;
                                            Phone.Value = txtPhone.Text.Trim();
                                            IdentityID.Value = txtIdenID.Text.Trim();
                                            Address.Value = txtAddress.Text.Trim();
                                            if (mycon.State== ConnectionState.Closed)
                                            {
                                                mycon.Open();
                                            }
                                            mycommand.ExecuteNonQuery();
                                            MessageBox.Show("添加成功");
                                            //初始化界面
                                            txtID.Clear();
                                            txtName.Clear();
                                            txtPhone.Clear();
                                            txtIdenID.Clear();
                                            txtAddress.Clear();
                                            txtID.Focus();
                                        }
                                        catch (Exception e1)
                                        {

                                            MessageBox.Show("未添加成功，原因："+e1.Message);
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
            }
                //else
                //    if (string.IsNullOrEmpty(txtIdenID.Text))
                //    {
                //        MessageBox.Show("请输入身份证号码");
                //    }
                //    else
                //        if (string.IsNullOrEmpty(txtAddress.Text))
                //        {
                //            MessageBox.Show("请输入通讯地址");
                //        }
                //        else
                //        {
            


                //        }
                //}
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            try
            {
                cmbClassID.Items.Clear();
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select * from Class";
                // ConnectionClass conclass = new ConnectionClass();
                DataSet myds = conclass.GetDataSet(mycommand.CommandText, "Class");
                cmbClassID.DataSource = myds.Tables["Class"];
                cmbClassID.ValueMember = "ClassID";
                cmbClassID.DisplayMember = "ClassID";
            }
            catch (Exception e1)
            {
                MessageBox.Show("初始化错误！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

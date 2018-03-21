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
    public partial class AddTeacher : Form
    {
        public AddTeacher()
        {
            InitializeComponent();
        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("请输入工号");
            }
            else
            {
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select ID from Teacher";
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
                    MessageBox.Show("该工号已经存在，请重新输入");
                    txtID.SelectAll();
                    txtID.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        MessageBox.Show("请输入姓名");
                    }
                    else
                    {
                        if (cmbDepartID.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择所属分院");
                        }
                        else
                        {
                            if (cmbProfessional.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择职称");
                            }
                            else
                                if (string.IsNullOrEmpty(txtPhone.Text))
                                {
                                    MessageBox.Show("请输入联系方式");
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
                                        mycommand.CommandText = "insert into Teacher(ID,TeacherName,DepartID,Sex,Professional,Phone) values( @ID,@TeacherName,@DepartID,@Sex,@Professional,@Phone)";
                                        SqlParameter Id = new SqlParameter("@ID", SqlDbType.NVarChar);
                                        SqlParameter TeacherName = new SqlParameter("@TeacherName", SqlDbType.NVarChar);
                                        SqlParameter DepartID = new SqlParameter("@DepartID", SqlDbType.NVarChar);
                                        SqlParameter Sex = new SqlParameter("@Sex", SqlDbType.NVarChar);
                                        SqlParameter Professional = new SqlParameter("@Professional", SqlDbType.NVarChar);
                                        SqlParameter Phone = new SqlParameter("@Phone", SqlDbType.NVarChar);
                                        mycommand.Parameters.Add(Id);
                                        mycommand.Parameters.Add(TeacherName);
                                        mycommand.Parameters.Add(DepartID);
                                        mycommand.Parameters.Add(Sex);
                                        mycommand.Parameters.Add(Professional);
                                        mycommand.Parameters.Add(Phone);
                                        Id.Value = txtID.Text.Trim();
                                        TeacherName.Value = txtName.Text.Trim();
                                        DepartID.Value = cmbDepartID.SelectedValue.ToString().Trim();
                                        Sex.Value = sex;
                                        Professional.Value = cmbProfessional.SelectedValue.ToString().Trim();
                                        Phone.Value = txtPhone.Text.Trim();
                                        
                                        if (mycon.State == ConnectionState.Closed)
                                        {
                                            mycon.Open();
                                        }
                                        mycommand.ExecuteNonQuery();
                                        MessageBox.Show("添加成功");
                                        txtID.Clear();
                                        txtName.Clear();
                                        txtPhone.Clear();
                                        txtID.Focus();
                                    }
                                    catch (Exception e1)
                                    {

                                        MessageBox.Show("未添加成功，原因：" + e1.Message);
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


        private void AddTeacher_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化教学分院信息
                cmbDepartID.Items.Clear();
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select * from Department";
                // ConnectionClass conclass = new ConnectionClass();
                DataSet myds = conclass.GetDataSet(mycommand.CommandText, "Depart");
                cmbDepartID.DataSource = myds.Tables["Depart"];
                cmbDepartID.ValueMember = "DepartID";
                cmbDepartID.DisplayMember = "DepartName";

                //初始化职称信息
                cmbProfessional.Items.Clear();
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select * from Professional";
                // ConnectionClass conclass = new ConnectionClass();
                myds = conclass.GetDataSet(mycommand.CommandText, "Professional");
                cmbProfessional.DataSource = myds.Tables["Professional"];
                cmbProfessional.ValueMember = "ProfessionalName";
                cmbProfessional.DisplayMember = "ProfessionalName";
            }
            catch (Exception e1)
            {
                MessageBox.Show("初始化错误！");
            }
        }
    }
}


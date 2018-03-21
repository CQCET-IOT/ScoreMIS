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
    public partial class modifyTeacher : Form
    {
        public modifyTeacher()
        {
            InitializeComponent();
        }

        private void Init()
        {
            string sqlstr = "select * from Teacher where ID='" + txtID.Text.Trim() + "'";
            ConnectionClass myconnection = new ConnectionClass();
            DataSet myDataSet = myconnection.GetDataSet(sqlstr, "Teacher");
            txtName.Text = myDataSet.Tables["Teacher"].Rows[0]["TeacherName"].ToString();
            txtPhone.Text = myDataSet.Tables["Teacher"].Rows[0]["Phone"].ToString();
            cmbProfessional.Text = myDataSet.Tables["Teacher"].Rows[0]["Professional"].ToString();
            string sex = myDataSet.Tables["Teacher"].Rows[0]["Sex"].ToString();
            if (sex == "男")
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("请输入姓名");
            }
            else
                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("请输入联系方式");
                }
                else
                    if (string.IsNullOrEmpty(cmbProfessional.Text))
                    {
                        MessageBox.Show("请选择职称");
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
                            SqlConnection mycon = null;
                            try
                            {
                                mycon = new SqlConnection(ConnectionClass.GetConStr);
                                SqlCommand mycommand = mycon.CreateCommand();
                                mycommand.CommandText = "update Teacher set TeacherName=@TeacherName, Sex=@Sex,Phone=@Phone,Professional=@Professional where ID=@ID";
                                SqlParameter Id = new SqlParameter("@ID", SqlDbType.NVarChar);
                                SqlParameter TeacherName = new SqlParameter("@TeacherName", SqlDbType.NVarChar);
                                SqlParameter Sex = new SqlParameter("@Sex", SqlDbType.NVarChar);
                                SqlParameter Phone = new SqlParameter("@Phone", SqlDbType.NVarChar);
                                SqlParameter Professional = new SqlParameter("@Professional", SqlDbType.NVarChar);
                                mycommand.Parameters.Add(TeacherName);
                                mycommand.Parameters.Add(Sex);
                                mycommand.Parameters.Add(Phone);
                                mycommand.Parameters.Add(Professional);                              
                                mycommand.Parameters.Add(Id);
                                Id.Value = txtID.Text.Trim();
                                TeacherName.Value = txtName.Text.Trim();
                                Sex.Value = sex;
                                Phone.Value = txtPhone.Text.Trim();
                                Professional.Value = cmbProfessional.Text.Trim();
                                mycon.Open();
                                mycommand.ExecuteNonQuery();
                                MessageBox.Show("修改成功");
                                }
                            catch (Exception)
                            {

                                MessageBox.Show("数据库问题");
                            }
                            finally
                            {
                                mycon.Close();
                            }


                        }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modifyTeacher_Load(object sender, EventArgs e)
        {
            txtID.Text = ShareClass.ID;
            Init();
        }
    }
}

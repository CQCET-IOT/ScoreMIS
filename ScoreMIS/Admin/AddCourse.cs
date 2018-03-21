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
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCourseID.Text))
            {
                MessageBox.Show("请输入课程号");
            }
            else
            {
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select CourseID from Course";
                SqlDataReader mydr = conclass.GetDataReader(mycommand.CommandText);
                if (mycon.State == ConnectionState.Closed)
                {
                    mycon.Open();
                }

                bool IDExists = false;
                while (mydr.Read())
                {
                    if (mydr["CourseID"].ToString().ToLower() == txtCourseID.Text.Trim().ToLower())
                    {
                        IDExists = true;
                        mycon.Close();
                        break;
                    }
                }
                if (IDExists)
                {
                    MessageBox.Show("该课程号已经存在，请重新输入");
                    txtCourseID.SelectAll();
                    txtCourseID.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtCourseName.Text))
                    {
                        MessageBox.Show("请输入课程名称");
                    }
                    else
                    {
                        if (cmbType.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择课程类型");
                        }
                        else
                        {
                            if (cmbMark.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择课程学分");
                            }
                            else
                            {

                                try
                                {
                                    mycommand = mycon.CreateCommand();
                                    mycommand.CommandText = "insert into Course(CourseID,CourseName,Type,Mark) values( @CourseID,@CourseName,@Type,@Mark)";
                                    SqlParameter CourseID = new SqlParameter("@CourseID", SqlDbType.NVarChar);
                                    SqlParameter CourseName = new SqlParameter("@CourseName", SqlDbType.NVarChar);
                                    SqlParameter CourseType = new SqlParameter("@Type", SqlDbType.NVarChar);
                                    SqlParameter Mark = new SqlParameter("@Mark", SqlDbType.NVarChar);
                                   
                                    mycommand.Parameters.Add(CourseID);
                                    mycommand.Parameters.Add(CourseName);
                                    mycommand.Parameters.Add(CourseType);
                                    mycommand.Parameters.Add(Mark);
                            
                                    CourseID.Value = txtCourseID.Text.Trim();
                                    CourseName.Value = txtCourseName.Text.Trim();
                                    CourseType.Value = cmbType.SelectedItem.ToString().Trim();
                                    Mark.Value = cmbMark.SelectedItem.ToString().Trim(); ;
                                   
                                    if (mycon.State == ConnectionState.Closed)
                                    {
                                        mycon.Open();
                                    }
                                    mycommand.ExecuteNonQuery();
                                    MessageBox.Show("添加成功");
                                    txtCourseID.Clear();
                                    txtCourseName.Clear();
                                    txtCourseID.Focus();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

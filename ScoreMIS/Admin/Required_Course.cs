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
    public partial class Required_Course : Form
    {
        public Required_Course()
        {
            InitializeComponent();
        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();
        DataSet myds = null;

        private void Required_Course_Load(object sender, EventArgs e)
        {
            //try
            //{
            //初始化学期
            cmbTerm.Items.Clear();
            cmbTerm.Items.Add("2012-2013第一学期");
            cmbTerm.Items.Add("2012-2013第二学期");
            cmbTerm.Items.Add("2013-2014第一学期");
            cmbTerm.Items.Add("2013-2014第二学期");

            //初始化班级信息
            cmbClassID.Items.Clear();
            mycommand = mycon.CreateCommand();
            mycommand.CommandText = "select * from Class";
            DataSet myds = conclass.GetDataSet(mycommand.CommandText, "Class");
            cmbClassID.DataSource = myds.Tables["Class"];
            cmbClassID.ValueMember = "ClassID";
            cmbClassID.DisplayMember = "ClassID";
            cmbClassID.SelectedIndex = -1;

            //初始化课程信息
            cmbCourse.Items.Clear();
            mycommand = mycon.CreateCommand();
            mycommand.CommandText = "select CourseID,CourseName from Course";
            myds = conclass.GetDataSet(mycommand.CommandText, "Course");
            cmbCourse.DataSource = myds.Tables["Course"];
            cmbCourse.ValueMember = "CourseID";
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.SelectedIndex = -1;
            //cmbCourse.Items.Insert(0, "");

            //初始化教师信息
            cmbTeacher.Items.Clear();
            mycommand = mycon.CreateCommand();
            mycommand.CommandText = "select ID,TeacherName from Teacher";
            myds = conclass.GetDataSet(mycommand.CommandText, "Teacher");
            cmbTeacher.DataSource = myds.Tables["Teacher"];
            cmbTeacher.ValueMember = "ID";
            cmbTeacher.DisplayMember = "TeacherName";
            cmbTeacher.SelectedIndex = -1;
            // cmbTeacher.Items.Insert(0, "");

            //}

            //catch (Exception e1)
            //{
            //    MessageBox.Show("初始化错误！");
            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbTerm.SelectedIndex == -1)
            {
                MessageBox.Show("请选择学期");
            }
            else
                if (cmbClassID.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择班级");
                }
                else
                    if (cmbCourse.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择课程");
                    }
                    else
                        if (cmbTeacher.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择教师");
                        }
                        else
                        {
                            try
                            {

                                //完成填充数据库
                                mycommand = mycon.CreateCommand();
                                mycommand.CommandType = CommandType.StoredProcedure;
                                mycommand.CommandText = "Insert_SelCourse_Score ";
                                                    //+ cmbCourse.SelectedValue + "','" 
                                                    //+ cmbTeacher.SelectedValue + "','" 
                                                    //+ cmbClassID.SelectedValue + "','" 
                                                    //+ cmbTerm.SelectedItem+"'";

                                SqlParameter CourseID = new SqlParameter("@CourseID", SqlDbType.NVarChar);
                                SqlParameter TeacherID = new SqlParameter("@TeacherID", SqlDbType.NVarChar);
                                SqlParameter ClassID = new SqlParameter("@ClassID", SqlDbType.NVarChar);
                                SqlParameter Term = new SqlParameter("@Term", SqlDbType.NVarChar);

                                mycommand.Parameters.Add(CourseID);
                                mycommand.Parameters.Add(TeacherID);
                                mycommand.Parameters.Add(ClassID);
                                mycommand.Parameters.Add(Term);

                                CourseID.Value = cmbCourse.SelectedValue;
                                TeacherID.Value = cmbTeacher.SelectedValue;
                                ClassID.Value = cmbClassID.SelectedValue;
                                Term.Value = cmbTerm.SelectedItem;

                                if (mycon.State == ConnectionState.Closed)
                                {
                                    mycon.Open();
                                }
                                mycommand.ExecuteNonQuery();

                                //将SelCourse表中信息显示到下方表格中
                                DisplaySelCourseByAll();
                                //MessageBox.Show("添加成功");
                                //cmbClassID.SelectedIndex = -1;
                                cmbCourse.SelectedIndex = -1;
                                cmbTeacher.SelectedIndex = -1;
                                //cmbTerm.SelectedIndex = -1;

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

        private void DisplaySelCourseByAll()
        {
            mycommand.CommandType = CommandType.StoredProcedure;
            mycommand.CommandText = "DisplaySelCourse '" + cmbTerm.SelectedItem + "','" + cmbClassID.SelectedValue + "'";

            myds = conclass.GetDataSet(mycommand.CommandText, "Class_Course");
            dataGridView1.DataSource = myds.Tables["Class_Course"];
        }

        private void cmbClassID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTerm.SelectedIndex == -1 || cmbClassID.SelectedIndex == -1)
            {
                //MessageBox.Show("请选择学期");
            }
            else
            {               
                mycommand.CommandType = CommandType.StoredProcedure;
                mycommand.CommandText = "DisplaySelCourse '" + cmbTerm.SelectedItem + "','" + cmbClassID.SelectedValue+"'";
                
                myds = conclass.GetDataSet(mycommand.CommandText, "Class_Course");
                dataGridView1.DataSource = myds.Tables["Class_Course"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result=MessageBox.Show("确认要删除吗？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                string CourseID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string ClassID = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string TeacherID = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string Term = dataGridView1.CurrentRow.Cells[5].Value.ToString();


                mycommand = mycon.CreateCommand();
                mycommand.CommandType = CommandType.StoredProcedure;
                mycommand.CommandText = "Delete_SelCourse_Score";
                //+ CourseID + "','" + TeacherID + "','" + ClassID + "','" + Term + "'";

                SqlParameter CourseIDParameter = new SqlParameter("@CourseID", SqlDbType.NVarChar);
                SqlParameter TeacherIDParameter = new SqlParameter("@TeacherID", SqlDbType.NVarChar);
                SqlParameter ClassIDParameter = new SqlParameter("@ClassID", SqlDbType.NVarChar);
                SqlParameter TermParameter = new SqlParameter("@Term", SqlDbType.NVarChar);

                mycommand.Parameters.Add(CourseIDParameter);
                mycommand.Parameters.Add(TeacherIDParameter);
                mycommand.Parameters.Add(ClassIDParameter);
                mycommand.Parameters.Add(TermParameter);

                CourseIDParameter.Value = CourseID;
                TeacherIDParameter.Value = TeacherID;
                ClassIDParameter.Value = ClassID;
                TermParameter.Value = Term;

                if (mycon.State == ConnectionState.Closed)
                {
                    mycon.Open();
                }
                mycommand.ExecuteNonQuery();
                DisplaySelCourseByAll();
            }
            else
            {
               
            }
             
        }
    }
}

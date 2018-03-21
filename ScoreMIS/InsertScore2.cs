using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScoreMIS.App_Code;

namespace ScoreMIS
{
    public partial class InsertScore2 : Form
    {
        public InsertScore2()
        {
            InitializeComponent();
        }
        private string _ClassID;

        public string ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
        private string _CourseID;

        public string CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }
        private string _Term;

        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();
        DataSet myds = null;

        private void InsertScore2_Load(object sender, EventArgs e)
        {
            //显示课程名称
            //lblCourseName.Text =this.CourseID;
            mycommand = mycon.CreateCommand();
            mycommand.CommandText = "select coursename from course where courseid='" + this.CourseID + "'";
            myds = conclass.GetDataSet(mycommand.CommandText, "Course");
            lblCourseName.Text = myds.Tables["Course"].Rows[0][0].ToString();

            //显示学生列表
            mycommand = mycon.CreateCommand();
            mycommand.CommandText = "GetStudentByClassIDCourseID '" + this.ClassID + "','" + this.CourseID + "'";
            myds.Clear();
            myds = conclass.GetDataSet(mycommand.CommandText, "StudentList");
            dataGridView1.DataSource = myds.Tables["StudentList"];
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "学号";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[2].HeaderText = "成绩";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ScoreIsNull = false;
            for (int i = 0; i < myds.Tables["StudentList"].Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(myds.Tables["StudentList"].Rows[i][2].ToString()))
                {
                    ScoreIsNull = true;
                    break;
                }
            }
            if (ScoreIsNull)
            {
                MessageBox.Show("请录入全部成绩");
            }
            else
            {
                try
                {
                    mycommand = mycon.CreateCommand();
                    if (mycon.State == ConnectionState.Closed)
                    {
                        mycon.Open();
                    }
                    for (int i = 0; i < myds.Tables["StudentList"].Rows.Count; i++)
                    {
                        string StudentID = myds.Tables["StudentList"].Rows[i][0].ToString();
                        string Score = myds.Tables["StudentList"].Rows[i][2].ToString();
                        mycommand.CommandText = "update Score set Score=" + Score
                            + "where CourseID='" + CourseID + "' and StudentID='" + StudentID 
                            + "' and Term='" + Term + "'";
                        mycommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("录入成功");
                }
                catch (Exception e1)
                {

                    MessageBox.Show("数据库问题");
                }
                finally
                {
                    mycon.Close();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

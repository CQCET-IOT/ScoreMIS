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
    public partial class InsertScore : Form
    {
        public InsertScore()
        {
            InitializeComponent();
        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();
        DataSet myds = null;
        private void InsertScore_Load(object sender, EventArgs e)
        {
            //初始化学期
            cmbTerm.Items.Clear();
            cmbTerm.Items.Add("2012-2013第一学期");
            cmbTerm.Items.Add("2012-2013第二学期");
            cmbTerm.Items.Add("2013-2014第一学期");
            cmbTerm.Items.Add("2013-2014第二学期");
            //初始化学号
            txtTeacherID.Text = ShareClass.Name;
        }

        private void cmbTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTerm.SelectedIndex == -1)
            {
                MessageBox.Show("请选择学期");
            }
            else
            {
                try
                {
                    mycommand = mycon.CreateCommand();
                    mycommand.CommandText = "GetCourseByTeacherID '" + ShareClass.ID + "','" + cmbTerm.SelectedItem + "'";
                    myds = conclass.GetDataSet(mycommand.CommandText, "CourseName");
                    //cmbCourseName.DataSource = myds.Tables["CourseName"];
                    dataGridView1.DataSource = myds.Tables["CourseName"];
                    dataGridView1.Columns[0].HeaderText = "";
                    dataGridView1.Columns[1].HeaderText = "班级";
                    dataGridView1.Columns[2].HeaderText = "课程编号";
                    dataGridView1.Columns[3].HeaderText = "课程名称";
                    //cmbCourseName.ValueMember = "CourseID";
                    //cmbCourseName.DisplayMember = "CourseName";
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.ColumnIndex == 0)
            {
                string CourseID= dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string ClassID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Term = cmbTerm.SelectedItem.ToString(); ;
                InsertScore2 insertform = new InsertScore2();
                insertform.ClassID = ClassID;
                insertform.CourseID = CourseID;
                insertform.Term = Term;
                insertform.ShowDialog();
            }  

        }
    }
}

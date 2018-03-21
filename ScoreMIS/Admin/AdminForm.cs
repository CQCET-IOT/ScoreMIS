using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScoreMIS.Admin
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void 添加分院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDepartment adform = new AddDepartment();
            adform.MdiParent = this;
            adform.Show();
        }

        private void 添加班级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClass acform = new AddClass();
            acform.MdiParent = this;
            acform.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 添加学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudent asform = new AddStudent();
            asform.MdiParent = this;
            asform.Show();
        }

        private void 添加教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTeacher atform = new AddTeacher();
            atform.MdiParent = this;
            atform.Show();
        }

        private void 添加课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourse acform = new AddCourse();
            acform.MdiParent = this;
            acform.Show();
        }

        private void 必修课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Required_Course form = new Required_Course();
            form.MdiParent = this;
            form.Show();

        }

        private void 选修课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = System.DateTime.Now.ToShortDateString() + "  " + System.DateTime.Now.ToShortTimeString();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            Picture_Resize();
            toolStripStatusLabel2.Text = "当前时间：";
            toolStripStatusLabel3.Text = System.DateTime.Now.ToShortDateString() + "  " + System.DateTime.Now.ToShortTimeString();
        }
        private void Picture_Resize()
        {
            this.BackgroundImage = pictureBox1.Image;
            this.BackgroundImageLayout = ImageLayout.Stretch;


        }

        private void AdminForm_Resize(object sender, EventArgs e)
        {
            Picture_Resize();
        }
    }
}

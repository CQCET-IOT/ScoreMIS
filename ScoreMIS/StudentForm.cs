using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScoreMIS.App_Code;
namespace ScoreMIS
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            Picture_Resize();
            toolStripStatusLabel2.Text = ShareClass.Name;
            toolStripStatusLabel3.Text = DateTime.Now.ToShortDateString();
            toolStripStatusLabel4.Text = DateTime.Now.ToLongTimeString(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToShortDateString();
            toolStripStatusLabel4.Text = DateTime.Now.ToLongTimeString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ModifyPwd modifyPwdForm = new ModifyPwd();
            modifyPwdForm.MdiParent = this;
            modifyPwdForm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ModifyStudent modifyStuForm = new ModifyStudent();
            modifyStuForm.MdiParent = this;
            modifyStuForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MyScore MyScoreForm = new MyScore();
            MyScoreForm.MdiParent = this;
            MyScoreForm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Picture_Resize()
        {
            this.BackgroundImage = pictureBox1.Image;
            this.BackgroundImageLayout = ImageLayout.Stretch;
           

        }
        private void StudentForm_Resize(object sender, EventArgs e)
        {
            Picture_Resize();

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

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
    public partial class AddClass : Form
    {
        public AddClass()
        {
            InitializeComponent();
        }
        private SqlConnection mycon = new SqlConnection(ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        ConnectionClass conclass = new ConnectionClass();

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClassID.Text))
            {
                MessageBox.Show("请输入班级编号");
            }
            else
            {
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select ClassID from Class";
                SqlDataReader mydr = conclass.GetDataReader(mycommand.CommandText);
                if (mycon.State == ConnectionState.Closed)
                {
                    mycon.Open();
                }
                bool ClassIDExists = false;
                while (mydr.Read())
                {
                    if (mydr["ClassID"].ToString().ToLower() == txtClassID.Text.Trim().ToLower())
                    {
                        ClassIDExists = true;
                       
                        break;
                    }
                }
                if (ClassIDExists)
                {
                    MessageBox.Show("班级编号已经存在，请重新输入");
                    txtClassID.SelectAll();
                    txtClassID.Focus();
                }
                else
                {
                    if (cmbDepart.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择分院");
                    }
                    else
                    {
                        try
                        {
                            mycommand.CommandText = "insert into Class values(@ClassID,@DepartID)";
                            SqlParameter ClassID = new SqlParameter("@ClassID", SqlDbType.NVarChar);
                            SqlParameter DepartID = new SqlParameter("@DepartID", SqlDbType.NVarChar);

                            mycommand.Parameters.Add(ClassID);
                            mycommand.Parameters.Add(DepartID);

                            ClassID.Value = txtClassID.Text.Trim();
                            DepartID.Value = cmbDepart.SelectedValue.ToString().Trim();
                            if (mycon.State == ConnectionState.Closed)
                            {
                                mycon.Open();
                            }
                            mycommand.ExecuteNonQuery();
                            MessageBox.Show("添加班级成功");
                            txtClassID.SelectAll();
                            txtClassID.Focus();
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

        private void AddClass_Load(object sender, EventArgs e)
        {
            try
            {
                cmbDepart.Items.Clear();
                mycommand = mycon.CreateCommand();
                mycommand.CommandText = "select * from Department";
                // ConnectionClass conclass = new ConnectionClass();
                DataSet myds = conclass.GetDataSet(mycommand.CommandText, "Depart");
                cmbDepart.DataSource = myds.Tables["Depart"];
                cmbDepart.ValueMember = "DepartID";
                cmbDepart.DisplayMember = "DepartName";
            }
            catch (Exception e1)
            {
                MessageBox.Show("初始化错误！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

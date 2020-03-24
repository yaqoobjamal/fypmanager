using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FYPMANAGERFINAL
{
    public partial class Teacher : Form
    {
        int teacherId;
        public Teacher()
        {
           
        }
        public void My_Data(int ID)
        {
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select * from Teacher where TeacherID='" + ID + "'", connection);
                sda.Fill(dt);
                label_ID.Text = dt.Rows[0][0].ToString();
                label_Name.Text = dt.Rows[0][1].ToString();
                label_ContactNo.Text = dt.Rows[0][2].ToString();
                label_Email.Text = dt.Rows[0][3].ToString();
                label_Dept.Text = dt.Rows[0][4].ToString();

            }

        }
        public Teacher(DataTable dt1)
        {
            InitializeComponent();
            teacherId = int.Parse(dt1.Rows[0][2].ToString());
            My_Data(teacherId);
        }


        private void Teacher_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllocateProjectPanel.Hide();
            RequestPanel.Hide();
            ProgressPanel.Hide();
            My_Data(teacherId);
            ProfilePanel.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            T_AddProject obj = new T_AddProject(teacherId);
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RequestPanel.Hide();
            ProgressPanel.Hide();
            ProfilePanel.Hide();
            AllocateProjectPanel.Show();

           
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select StudentID from Student where ProjectID is NULL", connection);
                sda.Fill(dt);
                comboBox4.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    comboBox4.Items.Add(row[0].ToString());
                }
            }

            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select ProjectCode from Project where TeacherID=" + teacherId, connection);
                sda.Fill(dt);
                comboBox3.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    comboBox3.Items.Add(row[0].ToString());
                }
            }
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select StudentID,Name,ProjectID from Student", connection);
                sda.Fill(dt);
                ListViewItem lv;
                listView6.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    lv = new ListViewItem(row[0].ToString());
                    lv.SubItems.Add(row[1].ToString());
                    lv.SubItems.Add(row[2].ToString());

                    listView6.Items.Add(lv);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllocateProjectPanel.Hide();
            RequestPanel.Hide();
            ProfilePanel.Hide();
            ProgressPanel.Show();

            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select ProjectCode from Project where TeacherID="+teacherId, connection);
                sda.Fill(dt);
                comboBox1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {                    
                    comboBox1.Items.Add(row[0].ToString());
                }               
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllocateProjectPanel.Hide();
            ProfilePanel.Hide();
            ProgressPanel.Hide();
            RequestPanel.Show();
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("sp_displayrequests  " + teacherId, connection);
                sda.Fill(dt);
                ListViewItem lv;
                listView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    lv = new ListViewItem(row[0].ToString());
                    lv.SubItems.Add(row[1].ToString());
                    lv.SubItems.Add(row[2].ToString());
                    lv.SubItems.Add(row[3].ToString());
                    listView1.Items.Add(lv);
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int StudentID;
            string StudentName, ProjectTitle;

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                StudentID = int.Parse(item.SubItems[0].Text);
                StudentName = item.SubItems[1].Text.ToString();
                ProjectTitle = item.SubItems[2].Text.ToString().Trim();
                using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
                {
                    connection.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_accept_notification '" + ProjectTitle + "'," + StudentID, connection);
                    cmd1.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("You have accepted Mr/Ms " + StudentName + " request.");
                    listView1.Clear();
                }
            }    
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int StudentID;
            if (listView1.Items.Count>0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                StudentID = int.Parse(item.SubItems[0].Text);
                using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
                {
                    connection.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_reject_notification "+StudentID, connection);
                    cmd1.ExecuteNonQuery();
                    connection.Close();
                    listView1.Clear();
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                string s1 = comboBox1.Text.ToString();
                SqlDataAdapter sda = new SqlDataAdapter("sp_viewprogress '"+s1+"'", connection);
                sda.Fill(dt);
                ListViewItem lv;
                listView5.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    lv = new ListViewItem(row[0].ToString());
                    lv.SubItems.Add(row[1].ToString());
                    lv.SubItems.Add(row[2].ToString());
                    lv.SubItems.Add(row[3].ToString());
                    listView5.Items.Add(lv);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                string s3 = comboBox3.Text.ToString();
                SqlDataAdapter sda = new SqlDataAdapter("sp_assignproject " + teacherId + ",'"+s3+"'", connection);
                sda.Fill(dt);

                string s1 = dt.Rows[0][0].ToString();
                int x1 = Convert.ToInt16(s1);
                if(x1==1)
                {
                    MessageBox.Show("Project Already Assigned");
                }
                if(x1==0)
                {
                    using (SqlConnection connection2 = new SqlConnection(Linker.CnnVal("FYPManager123")))
                    {
                        DataTable dt1 = new DataTable();
                        string s_id = comboBox4.Text.ToString();
                        string p_id = comboBox3.Text.ToString();
                        //SqlDataAdapter sdaa = new SqlDataAdapter("sp_giveproject " + s_id + ",'"+p_id+"'", connection);
                        connection.Open();
                        SqlCommand cmd1 = new SqlCommand("sp_giveproject " + s_id + ",'" + p_id + "'", connection);
                        cmd1.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Project Assigned");

                    }
                }
            }

            



        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class student : Form
    {
        int StudentId;
        public void My_Data(int ID)
        {
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select * from Student where StudentID='" + ID + "'", connection);
                sda.Fill(dt);
                S_ID.Text = dt.Rows[0][0].ToString();
                S_Name.Text = dt.Rows[0][1].ToString();
                S_ContactNo.Text = dt.Rows[0][2].ToString();
                S_Address.Text = dt.Rows[0][3].ToString();
                S_DOB.Text = dt.Rows[0][4].ToString();
                S_YOS.Text = dt.Rows[0][5].ToString();
                Y_Major.Text= dt.Rows[0][6].ToString();
                S_CGPA.Text = dt.Rows[0][8].ToString();
            }

        }
        public student()
        {
        }
        public student(DataTable dt1)
        {
            InitializeComponent();
            StudentId = int.Parse(dt1.Rows[0][2].ToString());
            My_Data(StudentId);
        }

        private void student_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myprojectPanel.Hide();
            AllProjectsPanel.Hide();
            ReqSupervisorPanel.Show();
            ProfilePanel.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddGroupMembers obj = new AddGroupMembers(StudentId);
            obj.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProfilePanel.Hide();
            myprojectPanel.Hide();
            AllProjectsPanel.Hide();
            ReqSupervisorPanel.Show();

            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("sp_display_unassigned_projects", connection);
                sda.Fill(dt);
                ListViewItem lv;
                listView2.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    lv = new ListViewItem(row[0].ToString());
                    lv.SubItems.Add(row[1].ToString());
                    lv.SubItems.Add(row[2].ToString());
                    listView2.Items.Add(lv);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProfilePanel.Hide();
            myprojectPanel.Hide();
            ReqSupervisorPanel.Hide();
            AllProjectsPanel.Show();
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("sp_displayprojects", connection);
                sda.Fill(dt);
                ListViewItem lv;
                listView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    lv = new ListViewItem(row[0].ToString());
                    lv.SubItems.Add(row[1].ToString());
                    listView1.Items.Add(lv);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProfilePanel.Hide();
            ReqSupervisorPanel.Hide();
            AllProjectsPanel.Hide();
            myprojectPanel.Show();

            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("sp_groupmembers "+StudentId, connection);
                sda.Fill(dt);

                int x = dt.Rows.Count;

                if (x == 0)
                {
                    lb_gm1.Text = "NULL";
                    lb_gm2.Text = "NULL";
                    lb_gm3.Text = "NULL";
                    lb_gm4.Text = "NULL";
                    lb_supervisor.Text = "NULL";
                    lb_pt.Text = "NULL";
                }
                else if (x == 1)
                {
                    lb_gm1.Text = dt.Rows[0][0].ToString();
                    lb_gm2.Text = "NULL";
                    lb_gm3.Text = "NULL";
                    lb_gm4.Text = "NULL";
                    lb_supervisor.Text = dt.Rows[0][2].ToString();
                    lb_pt.Text = dt.Rows[0][1].ToString();
                }
                else if (x == 2)
                {
                    lb_gm1.Text = dt.Rows[0][0].ToString();
                    lb_gm2.Text = dt.Rows[1][0].ToString();
                    lb_gm3.Text = "NULL";
                    lb_gm4.Text = "NULL";
                    lb_supervisor.Text = dt.Rows[0][2].ToString();
                    lb_pt.Text = dt.Rows[0][1].ToString();
                }
                else if (x == 3)
                {
                    lb_gm1.Text = dt.Rows[0][0].ToString();
                    lb_gm2.Text = dt.Rows[1][0].ToString();
                    lb_gm3.Text = dt.Rows[2][0].ToString();
                    lb_gm4.Text = "NULL";
                    lb_supervisor.Text = dt.Rows[0][2].ToString();
                    lb_pt.Text = dt.Rows[0][1].ToString();
                }
                else if (x == 4)
                {
                    lb_gm1.Text = dt.Rows[0][0].ToString();
                    lb_gm2.Text = dt.Rows[1][0].ToString();
                    lb_gm3.Text = dt.Rows[2][0].ToString();
                    lb_gm4.Text = dt.Rows[3][0].ToString();
                    lb_supervisor.Text = dt.Rows[0][2].ToString();
                    lb_pt.Text = dt.Rows[0][1].ToString();
                }
                    
                
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string d1 = dateTimePicker1.Text.ToString();
            string d2 = richTextBox1.Text.ToString();
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {              
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("sp_updateprogresslog " + StudentId + ",'" + d1 + "','"+d2+"'", connection);
                cmd1.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("ProgressLog Updated");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string Projectcode;
            string ProjectTitle;
            string supervisername;

            if (listView2.SelectedItems.Count > 0)
            {
                ListViewItem item = listView2.SelectedItems[0];
                Projectcode = item.SubItems[0].Text.ToString();
                ProjectTitle = item.SubItems[1].Text.ToString().Trim();
                supervisername = item.SubItems[2].Text.ToString();
                
                using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
                {
                    connection.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_send_notification " + StudentId + ",'" + supervisername+"','"+ ProjectTitle+"'", connection);
                    cmd1.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Notification Sent to " + supervisername);
                    listView2.Clear();
                }
            }
        }
    }
}

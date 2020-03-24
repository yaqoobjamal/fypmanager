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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username;
            string password;
            username = textBox1.Text.Trim();
            password = textBox2.Text.Trim();

            if (checkBox1.Checked)
            {
                using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("select * from TeacherLogin where TeacherUserName= '" + username + "' AND TeacherPassword= '" +password+"'", connection);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        Teacher obj = new Teacher(dt);
                        this.Hide();
                        obj.Show();
                    }
                    else
                        MessageBox.Show("Error login"); 
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("select * from StudentLogin where StudentUserName = '" + username + "' and StudentPassword = '" + password+"'", connection);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        student obj = new student(dt);
                        this.Hide();
                        obj.Show();

                    }
                    else
                        MessageBox.Show("Error login");
                }

            }

        }
    }
}

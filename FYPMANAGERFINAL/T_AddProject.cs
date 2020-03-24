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
    public partial class T_AddProject : Form
    {
        int teacherId;
        public T_AddProject()
        {
        }
        public T_AddProject(int teacherid)
        {
            InitializeComponent();
            teacherId = teacherid;

        }

        private void T_AddProject_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string projectitle, projectcode, projectdescription, projectscope, projectprereq;
            projectitle = mktb1.Text.ToString();
            projectcode = mktb2.Text.ToString();
            projectdescription = mktb3.Text.ToString();
            projectscope = mktb4.Text.ToString();
            projectprereq = mktb5.Text.ToString();
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_new_project "+projectcode+","+projectitle+","+projectdescription+","+projectscope+","+projectprereq+","+teacherId+","+"0", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            this.Hide();

        }
    }
}

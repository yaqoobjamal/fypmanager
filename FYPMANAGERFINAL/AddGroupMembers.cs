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
    public partial class AddGroupMembers : Form
    {
        int Student_sins;
        public AddGroupMembers(int S_id)
        {
            Student_sins = S_id;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int s1 = int.Parse(maskedTextBox1.Text.ToString());
            int s2 = int.Parse(maskedTextBox2.Text.ToString());
            int s3 = int.Parse(maskedTextBox3.Text.ToString());
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd1 = new SqlCommand("sp_add_members " + Student_sins + "," + s1 + "," + s2 + "," + s3, connection);
                    cmd1.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Group Created");
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
                    maskedTextBox3.Text = "";
                    listView1.Items.Clear();
                }catch
                {
                    MessageBox.Show("Error 404");
                }
            }
        }

        private void AddGroupMembers_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Linker.CnnVal("FYPManager123")))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("sp_Free_Students ", connection);
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
    }
}

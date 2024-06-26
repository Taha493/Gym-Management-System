using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Portal
{
    public partial class Trainer_Feedback : Form
    {
        public Trainer_Feedback()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void Trainer_Feedback_Load(object sender, EventArgs e)
        {
            try
            {
                Trainer_Login tl = new Trainer_Login();
                int trainer_id = tl.GetTrainerId();
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                	 SELECT 
                    CF.feedback_text,
                    CF.rating,
                    GM.member_id,
                    GM.first_name + ' ' + GM.last_name AS member_name
                FROM 
                    ClientFeedback CF
                JOIN 
                    gym_member GM ON CF.member_id = GM.member_id
                WHERE 
                    CF.trainer_id = @trainer_id;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            command.Parameters.AddWithValue("@trainer_id", trainer_id);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trainer_Options trainer_Options = new Trainer_Options();
            this.Hide();
            trainer_Options.Show();
        }
    }
}

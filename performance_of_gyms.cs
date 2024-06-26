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
    public partial class performance_of_gyms : Form
    {
        public performance_of_gyms()
        {
            InitializeComponent();
        }

        private void performance_of_gyms_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT G.gym_id, G.gym_name, G.location, G.phone, G.status, 
                       COUNT(MGA.member_id) AS attendance,
                       SUM(IIF(M.gender = 'Male', 1, 0)) AS male_count,
                       SUM(IIF(M.gender = 'Female', 1, 0)) AS female_count,
                       (100.0 * SUM(IIF(M.gender = 'Male', 1, 0))) / NULLIF(COUNT(MGA.member_id), 0) AS male_percentage,
                       (100.0 * SUM(IIF(M.gender = 'Female', 1, 0))) / NULLIF(COUNT(MGA.member_id), 0) AS female_percentage
                FROM Gym G
                LEFT JOIN Member_Gym_Association MGA ON G.gym_id = MGA.gym_id
                LEFT JOIN gym_member M ON MGA.member_id = M.member_id
                GROUP BY G.gym_id, G.gym_name, G.location, G.phone, G.status";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin_options go = new admin_options();
            this.Hide();
            go.Show();
        }
    }
}
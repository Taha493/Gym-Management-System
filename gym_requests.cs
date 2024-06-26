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
    public partial class gym_requests : Form
    {
        public gym_requests()
        {
            InitializeComponent();
        }

        private void gym_requests_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
            SELECT gym_id, gym_ownerID, gym_name, location, phone, status
            FROM Gym
            WHERE status = 'Pending'";

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a gym ID.");
                return;
            }

            // Check if the entered text is a valid integer
            if (!int.TryParse(textBox1.Text, out int gymId))
            {
                MessageBox.Show("Please enter a valid gym ID.");
                return;
            }

            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                UPDATE Gym
                SET status = 'Approved'
                WHERE gym_id = @gymId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gymId", gymId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                           // MessageBox.Show("Gym status updated to 'Approved' successfully.");
                            gym_requests_Load(null,null);
                        }
                        else
                        {
                            MessageBox.Show("No gym found with the entered ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a gym ID.");
                return;
            }

            // Check if the entered text is a valid integer
            if (!int.TryParse(textBox1.Text, out int gymId))
            {
                MessageBox.Show("Please enter a valid gym ID.");
                return;
            }

            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                UPDATE Gym
                SET status = 'Declined'
                WHERE gym_id = @gymId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gymId", gymId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                          //  MessageBox.Show("Gym status updated to 'Declined' successfully.");
                            gym_requests_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No gym found with the entered ID.");
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

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
    public partial class GymOwner_AddTrainer : Form
    {
        public GymOwner_AddTrainer()
        {
            InitializeComponent();
        }

        private void GymOwner_AddTrainer_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
                GymOwner_Login gol = new GymOwner_Login();
                int gym_owner_id = gol.GetGymOnwerId();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT T.trainer_id, T.first_name + ' ' + T.last_name AS full_name,
                   T.gender,
                   T.qualifications,
                   T.specialty_areas,
                   T.experience,
                   TGA.status
            FROM Trainer T
            INNER JOIN Trainer_Gym_Association TGA ON T.trainer_id = TGA.trainer_id
            WHERE TGA.gym_id IN (SELECT gym_id FROM Gym WHERE gym_ownerID = @gym_owner_id)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gym_owner_id", gym_owner_id);

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

        private void button1_Click(object sender, EventArgs e)
        {
            GymOwner_Options gymOwnerOptions = new GymOwner_Options();
            this.Hide();
            gymOwnerOptions.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int trainer_id) || trainer_id <= 0)
            {
                MessageBox.Show("Please enter a valid appointment ID.");
                return;
            }

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE Trainer_Gym_Association SET Status = 'Approved' WHERE trainer_id = @trainer_id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@trainer_id", trainer_id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status updated successfully to 'Approved' for Trainer ID: " + trainer_id);
                            GymOwner_AddTrainer_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No trainer found with the provided ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int trainer_id) || trainer_id <= 0)
            {
                MessageBox.Show("Please enter a valid appointment ID.");
                return;
            }

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE Trainer_Gym_Association SET Status = 'Cancelled' WHERE trainer_id = @trainer_id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@trainer_id", trainer_id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status updated successfully to 'Cancelled' for Trainer ID: " + trainer_id);
                            GymOwner_AddTrainer_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No trainer found with the provided ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GymOwner_Options go = new GymOwner_Options();
            this.Hide();
            go.Show();
        }
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gym_Portal
{
    public partial class GymOwner_AccountManagement : Form
    {
        public GymOwner_AccountManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GymOwner_Options gymOwnerOptions = new GymOwner_Options();
            this.Hide();
            gymOwnerOptions.Show();
        }

        private void GymOwner_AccountManagement_Load(object sender, EventArgs e)
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
                SELECT T.member_id, T.first_name + ' ' + T.last_name AS full_name,
                   T.gender,
                   T.dob,
                   T.phone_number,
                   T.email_address
            FROM gym_member T
            INNER JOIN Member_Gym_Association TGA ON T.member_id = TGA.member_id
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
                   T.DOB,
                   T.phone_number,
                   T.email
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
                            dataGridView2.DataSource = table;
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
            if (!int.TryParse(textBox15.Text, out int member_id) || member_id <= 0)
            {
                MessageBox.Show("Please enter a valid member ID.");
                return;
            }

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "DELETE FROM Member_Gym_Association WHERE member_id = @member_id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@member_id", member_id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully deleted member with member ID: " + member_id);
                            GymOwner_AccountManagement_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No member found with the provided ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int trainer_id) || trainer_id <= 0)
            {
                MessageBox.Show("Please enter a valid trainer ID.");
                return;
            }

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "DELETE FROM Trainer_Gym_Association WHERE trainer_id = @trainer_id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@trainer_id", trainer_id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully deleted trainer with member ID: " + trainer_id);
                            GymOwner_AccountManagement_Load(null, null);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GymOwner_Options go = new GymOwner_Options();
            this.Hide();
            go.Show();
        }
    }
}

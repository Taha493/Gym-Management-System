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
    public partial class GymOwner_GymRegister : Form
    {
        public GymOwner_GymRegister()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GymOwner_Options gymOwnerOptions = new GymOwner_Options();
            this.Hide();
            gymOwnerOptions.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gym_name = textBox8.Text;
            string location = textBox7.Text;
            string contact = textBox6.Text;

            GymOwner_Login gl = new GymOwner_Login();
            int gym_ownerID = gl.GetGymOnwerId();

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True"; // Replace this with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to insert data
                string query = "INSERT INTO Gym (gym_ownerID, gym_name, location, phone) VALUES (@OwnerID, @Name, @Location, @Phone)";

                // Open the connection
                connection.Open();

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameter values
                    command.Parameters.AddWithValue("@OwnerID", gym_ownerID);
                    command.Parameters.AddWithValue("@Name", gym_name);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@Phone", contact);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Display the result
                    if (rowsAffected > 0)
                    {
                        // Display the result
                        MessageBox.Show("Request sent to admin", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GymOwner_Options go = new GymOwner_Options();
            this.Hide();
            go.Show();
        }
    }
}

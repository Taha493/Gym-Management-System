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
    public partial class GymOwner_Signup : Form
    {
        public GymOwner_Signup()
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


            // Retrieve data from input controls
            string ownerName = textBox8.Text;
            string username = textBox6.Text;
            string password = textBox5.Text;
            string email = textBox7.Text;
            string contactNumber = textBox1.Text;

            if (string.IsNullOrWhiteSpace(ownerName) || string.IsNullOrWhiteSpace(username) ||
    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) ||
    string.IsNullOrWhiteSpace(contactNumber))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Insert data into GymOwners table
                string insertOwnerQuery = @"INSERT INTO GymOwners (owner_name, username, password, email, contact_number)
                                    VALUES (@OwnerName, @Username, @Password, @Email, @ContactNumber);";

                using (SqlCommand insertOwnerCommand = new SqlCommand(insertOwnerQuery, connection))
                {
                    insertOwnerCommand.Parameters.AddWithValue("@OwnerName", ownerName);
                    insertOwnerCommand.Parameters.AddWithValue("@Username", username);
                    insertOwnerCommand.Parameters.AddWithValue("@Password", password);
                    insertOwnerCommand.Parameters.AddWithValue("@Email", email);
                    insertOwnerCommand.Parameters.AddWithValue("@ContactNumber", contactNumber);

                    insertOwnerCommand.ExecuteNonQuery();
                }
            }
            GymOwner_Login g1 = new GymOwner_Login();
            this.Hide();
            g1.Show();
         
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_Login gl = new GymOwner_Login();
            this.Hide();
            gl.Show();
        }
    }
}

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
    public partial class GymOwner_Login : Form
    {
        public static int gymOwnerId = -1;
        public GymOwner_Login()
        {
            InitializeComponent();
        }

        private void GymOwner_Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text;
            string password = textBox3.Text;

            // Check if the username and password are not empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the credentials are valid
            if (CheckCredentials(username, password))
            {
                SetGymOwnerId(username, password);
                //MessageBox.Show("You've successfully logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GymOwner_Options gymOwnerInterface = new GymOwner_Options();
                this.Hide();
                gymOwnerInterface.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckCredentials(string username, string password)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            string query = "SELECT COUNT(*) FROM GymOwners WHERE username = @username AND password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void SetGymOwnerId(string username, string password)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT onwer_id FROM GymOwners WHERE username = @Username AND password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        gymOwnerId = Convert.ToInt32(result);
                    }
                }
            }
        }

        public int GetGymOnwerId()
        {
            return gymOwnerId;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_Signup gymOwner_Signup = new GymOwner_Signup();
            this.Hide();
            gymOwner_Signup.Show();
        }
    }
}

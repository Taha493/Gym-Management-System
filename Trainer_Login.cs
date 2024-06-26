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
    public partial class Trainer_Login : Form
    {
        public static int Trainerid = -1;
        public Trainer_Login()
        {
            InitializeComponent();
        }

        private void Trainer_Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register_Trainer trainer_signup = new Register_Trainer();
            this.Hide();
            trainer_signup.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox7.Text;
            string password = textBox6.Text;

            // Check if the username and password are not empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the credentials are valid
            if (CheckCredentials(username, password))
            {
                SetTrainerId(username, password);
                //MessageBox.Show("You've successfully logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Trainer_Options trainerInterface = new Trainer_Options();
                this.Hide();
                trainerInterface.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckCredentials(string username, string password)
        {
            SqlConnection connection = new SqlConnection("Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True");
            string query = "SELECT COUNT(*) FROM Trainer WHERE username = @username AND password = @password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }

        public void SetTrainerId(string username, string password)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT trainer_id FROM Trainer WHERE username = @Username AND password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        Trainerid = Convert.ToInt32(result);
                    }
                }
            }
        }

        public int GetTrainerId()
        {
            return Trainerid;
        }
    }
}

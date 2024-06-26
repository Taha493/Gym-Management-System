using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Gym_Portal
{
    public partial class Member_Login : Form
    {
        public static int memberId = -1;
        public Member_Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Member_Signup signUpForm = new Member_Signup();
            this.Hide();
            signUpForm.Show();


        }

        private void label11_Click(object sender, EventArgs e)
        {

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
                SetMemberId(username, password);
                //MessageBox.Show("You've successfully logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Member_Options memberInterface = new Member_Options();
                this.Hide();
                memberInterface.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckCredentials(string username, string password)
        {
            SqlConnection connection = new SqlConnection("Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True");
            string query = "SELECT COUNT(*) FROM gym_member WHERE username = @Username AND user_password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }

        public void SetMemberId(string username, string password)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT member_id FROM gym_member WHERE username = @Username AND user_password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        memberId = Convert.ToInt32(result);
                    }
                }
            }
        }

        public int GetMemberId()
        {
            return memberId;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }   
}

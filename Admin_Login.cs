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
    public partial class Admin_Login : Form
    {
        public Admin_Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text;
            string password = textBox5.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Check if the credentials are valid
            if (CheckCredentials(username, password))
            {
                //MessageBox.Show("You've successfully logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                admin_options gymOwnerInterface = new admin_options();
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
            string query = "SELECT COUNT(*) FROM Admin WHERE username = @username AND password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection)   )
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}

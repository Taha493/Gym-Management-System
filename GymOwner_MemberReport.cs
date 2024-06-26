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
    public partial class GymOwner_MemberReport : Form
    {

        Dictionary<string, int> MemberIdMap = new Dictionary<string, int>(); // Dictionary to map trainer names to IDs
        int selectedMemberId = -1; // Variable to store the selected trainer ID
        public GymOwner_MemberReport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GymOwner_Options gymOwnerOptions = new GymOwner_Options();
            this.Hide();
            gymOwnerOptions.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMember = comboBox1.SelectedItem.ToString();

            // Get the trainer's ID from the dictionary
            if (MemberIdMap.ContainsKey(selectedMember))
            {
                selectedMemberId = MemberIdMap[selectedMember];
            }

            try
            {

                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT first_name, last_name, gender, dob, phone_number, email_address, user_address FROM gym_member WHERE member_id = @member_id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@member_id", selectedMemberId);

                    SqlDataAdapter member_plans = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    member_plans.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log or display error message.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void GymOwner_MemberReport_Load(object sender, EventArgs e)
        {
            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve data
                string query = "SELECT member_id, first_name, last_name FROM gym_member";

                // Command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the connection
                    connection.Open();

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear the combo box
                        comboBox1.Items.Clear();

                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int member_id = Convert.ToInt32(reader["member_id"]);
                            string fullName = $"{reader["first_name"]} {reader["last_name"]}";
                            string member_info = $"{fullName}";
                            comboBox1.Items.Add(member_info); // Add trainer info to combo box
                            MemberIdMap.Add(member_info, member_id); // Add trainer info and ID to dictionary
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GymOwner_Options go = new GymOwner_Options();
            this.Hide();
            go.Show();
        }
    }
}
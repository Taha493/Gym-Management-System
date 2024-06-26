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
    public partial class GymOwner_TrainerReport : Form
    {
        Dictionary<string, int> TrainerIdMap = new Dictionary<string, int>(); // Dictionary to map trainer names to IDs
        int selectedTrainerId = -1; // Variable to store the selected trainer ID
        public GymOwner_TrainerReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GymOwner_Options gymOwnerOptions = new GymOwner_Options();
            this.Hide();
            gymOwnerOptions.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMember = comboBox1.SelectedItem.ToString();

            // Get the trainer's ID from the dictionary
            if (TrainerIdMap.ContainsKey(selectedMember))
            {
                selectedTrainerId = TrainerIdMap[selectedMember];
            }

            try
            {

                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT first_name, last_name, gender, DOB, phone_number, email, address, qualifications, specialty_areas, experience FROM Trainer WHERE trainer_id = @trainer_id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@trainer_id", selectedTrainerId);

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

        private void GymOwner_TrainerReport_Load(object sender, EventArgs e)
        {
            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve data
                string query = "SELECT trainer_id, first_name, last_name FROM Trainer";

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
                            int member_id = Convert.ToInt32(reader["trainer_id"]);
                            string fullName = $"{reader["first_name"]} {reader["last_name"]}";
                            string trainer_info = $"{fullName}";
                            comboBox1.Items.Add(trainer_info); // Add trainer info to combo box
                            TrainerIdMap.Add(trainer_info, member_id); // Add trainer info and ID to dictionary
                        }
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GymOwner_Options go = new GymOwner_Options();
            this.Hide();
            go.Show();
        }
    }
}

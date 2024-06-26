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
    public partial class Personal_Session : Form
    {
        Dictionary<string, int> trainerIdMap = new Dictionary<string, int>(); // Dictionary to map trainer names to IDs
        int selectedTrainerId = -1; // Variable to store the selected trainer ID
        public Personal_Session()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Member_Options memberInterface = new Member_Options();
            this.Hide();
            memberInterface.Show();
        }

        private void Personal_Session_Load(object sender, EventArgs e)
        {
            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve data
                string query = "SELECT trainer_id, first_name, last_name, specialty_areas, experience FROM Trainer";

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
                            int trainerId = Convert.ToInt32(reader["trainer_id"]);
                            string fullName = $"{reader["first_name"]} {reader["last_name"]}";
                            string specialtyArea = reader["specialty_areas"].ToString();
                            string experience = reader["experience"].ToString();
                            string trainerInfo = $"{fullName} - {specialtyArea} ({experience})";
                            comboBox1.Items.Add(trainerInfo); // Add trainer info to combo box
                            trainerIdMap.Add(trainerInfo, trainerId); // Add trainer info and ID to dictionary
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the member ID
            Member_Login ml = new Member_Login();
            int member_id = ml.GetMemberId();

            // Get the selected date and time from the DateTimePicker
            DateTime selectedDateTime = dateTimePicker1.Value;

            // Check if the selected trainer ID is 1
            if (selectedTrainerId == -1)
            {
                MessageBox.Show("Error: Please select a trainer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to insert data into Personal_Sessions table
                string query = "INSERT INTO Personal_Sessions (MemberID, TrainerID, Date_and_Time) VALUES (@MemberID, @TrainerID, @DateTime)";

                // Command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@MemberID", member_id);
                    command.Parameters.AddWithValue("@TrainerID", selectedTrainerId);
                    command.Parameters.AddWithValue("@DateTime", selectedDateTime);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected trainer's name from the combo box
            string selectedTrainer = comboBox1.SelectedItem.ToString();

            // Get the trainer's ID from the dictionary
            if (trainerIdMap.ContainsKey(selectedTrainer))
            {
                selectedTrainerId = trainerIdMap[selectedTrainer];
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

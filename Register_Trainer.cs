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
    public partial class Register_Trainer : Form
    {
        Dictionary<string, int> gymIdMap = new Dictionary<string, int>();
        public Register_Trainer()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trainer_Login trainer_login = new Trainer_Login();
            this.Hide();
            trainer_login.Show();
        }

        private void Register_Trainer_Load(object sender, EventArgs e)
        {
            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve gym names and IDs
                string query = "SELECT gym_id, gym_name FROM Gym WHERE status = 'Approved'";

                // Command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the connection
                    connection.Open();

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear the combo box
                        comboBox2.Items.Clear();

                        // Dictionary to store gym names and IDs

                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int gymId = Convert.ToInt32(reader["gym_id"]);
                            string gymName = reader["gym_name"].ToString();
                            comboBox2.Items.Add(gymName); // Add gym name to combo box
                            gymIdMap.Add(gymName, gymId); // Add gym name and ID to dictionary
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox7.Text) ||
        string.IsNullOrWhiteSpace(textBox6.Text) || comboBox1.SelectedItem == null || string.IsNullOrWhiteSpace(textBox3.Text) ||
        string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox10.Text) ||
        string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox9.Text) || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
            // Retrieve data from input controls
            string firstName = textBox2.Text;
            string lastName = textBox1.Text;
            string username = textBox7.Text;
            string password = textBox6.Text;
            string gender = comboBox1.SelectedItem.ToString();
            DateTime dob = dateTimePicker1.Value;
            string phoneNumber = textBox3.Text;
            string email = textBox4.Text;
            string address = textBox5.Text;
            string qualifications = textBox10.Text;
            string specialtyAreas = textBox8.Text;
            string experience = textBox9.Text;

            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Insert data into Trainer table
                string insertTrainerQuery = @"INSERT INTO Trainer (first_name, last_name, username, password, gender, DOB, phone_number, email, address, qualifications, specialty_areas, experience)
                                      VALUES (@FirstName, @LastName, @Username, @Password, @Gender, @DOB, @PhoneNumber, @Email, @Address, @Qualifications, @SpecialtyAreas, @Experience);
                                      SELECT SCOPE_IDENTITY();"; // Retrieve the ID of the newly inserted trainer

                using (SqlCommand insertTrainerCommand = new SqlCommand(insertTrainerQuery, connection))
                {
                    insertTrainerCommand.Parameters.AddWithValue("@FirstName", firstName);
                    insertTrainerCommand.Parameters.AddWithValue("@LastName", lastName);
                    insertTrainerCommand.Parameters.AddWithValue("@Username", username);
                    insertTrainerCommand.Parameters.AddWithValue("@Password", password);
                    insertTrainerCommand.Parameters.AddWithValue("@Gender", gender);
                    insertTrainerCommand.Parameters.AddWithValue("@DOB", dob);
                    insertTrainerCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    insertTrainerCommand.Parameters.AddWithValue("@Email", email);
                    insertTrainerCommand.Parameters.AddWithValue("@Address", address);
                    insertTrainerCommand.Parameters.AddWithValue("@Qualifications", qualifications);
                    insertTrainerCommand.Parameters.AddWithValue("@SpecialtyAreas", specialtyAreas);
                    insertTrainerCommand.Parameters.AddWithValue("@Experience", experience);

                    int trainerId = Convert.ToInt32(insertTrainerCommand.ExecuteScalar()); // Execute the query and get the newly inserted trainer ID

                    // Associate the trainer with the selected gym
                    if (comboBox2.SelectedItem != null)
                    {
                        string selectedGymName = comboBox2.SelectedItem.ToString();
                        string selectGymIdQuery = "SELECT gym_id FROM Gym WHERE gym_name = @GymName;";
                        using (SqlCommand selectGymIdCommand = new SqlCommand(selectGymIdQuery, connection))
                        {
                            selectGymIdCommand.Parameters.AddWithValue("@GymName", selectedGymName);
                            int gymId = Convert.ToInt32(selectGymIdCommand.ExecuteScalar());

                            // Insert association into Trainer_Gym_Association table
                            string insertAssociationQuery = "INSERT INTO Trainer_Gym_Association (trainer_id, gym_id) VALUES (@TrainerId, @GymId);";
                            using (SqlCommand insertAssociationCommand = new SqlCommand(insertAssociationQuery, connection))
                            {
                                insertAssociationCommand.Parameters.AddWithValue("@TrainerId", trainerId);
                                insertAssociationCommand.Parameters.AddWithValue("@GymId", gymId);
                                insertAssociationCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            Trainer_Login t1 = new Trainer_Login();
            this.Hide();
            t1.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

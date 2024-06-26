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
    public partial class Member_Signup : Form
    {
        Dictionary<string, int> gymIdMap = new Dictionary<string, int>();
        public Member_Signup()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Member_Login LoginForm = new Member_Login();
            LoginForm.Show();

            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void Member_Signup_Load(object sender, EventArgs e)
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

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Member_Login memberLogin = new Member_Login();
            this.Hide();
            memberLogin.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();

            // Get the selected gym name
            string selectedGymName = comboBox2.SelectedItem.ToString();

            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve trainers in the selected gym with status 'Approved'
                string query = @"SELECT T.trainer_id, T.first_name, T.last_name 
                                 FROM Trainer T
                                 INNER JOIN Trainer_Gym_Association TA ON T.trainer_id = TA.trainer_id
                                 INNER JOIN Gym G ON TA.gym_id = G.gym_id
                                 WHERE G.gym_name = @GymName AND G.status = 'Approved';";

                // Command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for gym name
                    command.Parameters.AddWithValue("@GymName", selectedGymName);

                    // Open the connection
                    connection.Open();

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Populate the trainers combo box
                        while (reader.Read())
                        {
                            int trainerId = Convert.ToInt32(reader["trainer_id"]);
                            string trainerName = reader["first_name"].ToString() + " " + reader["last_name"].ToString();
                            comboBox3.Items.Add(new TrainerItem(trainerId, trainerName)); // Add trainer to combo box
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if all required fields are filled
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox7.Text) ||
                 string.IsNullOrWhiteSpace(textBox6.Text) || comboBox1.SelectedItem == null || string.IsNullOrWhiteSpace(textBox3.Text) ||
                 string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || comboBox2.SelectedItem == null ||
                 comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Extract user input
            string firstName = textBox2.Text;
            string lastName = textBox1.Text;
            string username = textBox7.Text;
            string password = textBox6.Text;
            string gender = comboBox1.SelectedItem.ToString();
            DateTime dob = dateTimePicker1.Value;
            string phoneNumber = textBox3.Text;
            string emailAddress = textBox4.Text;
            string userAddress = textBox5.Text;

            // Get the selected trainer
            TrainerItem selectedTrainer = (TrainerItem)comboBox3.SelectedItem;
            int selectedTrainerId = selectedTrainer.TrainerId;

            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // Insert into gym_member table
                string insertQuery = @"INSERT INTO gym_member (first_name, last_name, username, user_password, gender, dob, phone_number, email_address, user_address) 
                                       VALUES (@FirstName, @LastName, @Username, @Password, @Gender, @DOB, @PhoneNumber, @EmailAddress, @UserAddress);
                                       SELECT SCOPE_IDENTITY();"; // To get the inserted member_id

                // Command to execute the insert query
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@DOB", dob);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                    command.Parameters.AddWithValue("@UserAddress", userAddress);

                    // Open the connection
                    connection.Open();

                    // Execute the insert query and get the inserted member_id
                    int memberId = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into Member_Gym_Association table
                    string gymName = comboBox2.SelectedItem.ToString();
                    int gymId = 0;

                    // Query to retrieve gym ID based on gym name
                    string selectGymIdQuery = "SELECT gym_id FROM Gym WHERE gym_name = @GymName";

                    // Command to execute the query
                    using (SqlCommand selectGymIdCommand = new SqlCommand(selectGymIdQuery, connection))
                    {
                        // Add parameter for gym name
                        selectGymIdCommand.Parameters.AddWithValue("@GymName", gymName);

                        // Execute the query to get the gym ID
                        var gymIdResult = selectGymIdCommand.ExecuteScalar();
                        if (gymIdResult != null)
                        {
                            gymId = Convert.ToInt32(gymIdResult);
                        }
                        else
                        {
                            // Handle the case where gym ID is not found (e.g., throw an exception or display an error message)
                            MessageBox.Show("Gym not found.");
                            return;
                        }
                    }

                    // Insert into Member_Gym_Association table
                    string insertAssociationQuery = "INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (@MemberId, @GymId);";

                    // Command to execute the association query
                    using (SqlCommand associationCommand = new SqlCommand(insertAssociationQuery, connection))
                    {
                        // Add parameters
                        associationCommand.Parameters.AddWithValue("@MemberId", memberId);
                        associationCommand.Parameters.AddWithValue("@GymId", gymId);

                        // Execute the association query
                        associationCommand.ExecuteNonQuery();
                    }

                    // Insert into Member_Trainer_Association table
                    string insertTrainerAssociationQuery = "INSERT INTO Member_Trainer_Association (member_id, trainer_id) VALUES (@MemberId, @TrainerId);";

                    // Command to execute the trainer association query
                    using (SqlCommand trainerAssociationCommand = new SqlCommand(insertTrainerAssociationQuery, connection))
                    {
                        // Add parameters
                        trainerAssociationCommand.Parameters.AddWithValue("@MemberId", memberId);
                        trainerAssociationCommand.Parameters.AddWithValue("@TrainerId", selectedTrainerId);

                        // Execute the trainer association query
                        trainerAssociationCommand.ExecuteNonQuery();
                    }

                    Member_Login m1 = new Member_Login();
                    this.Hide();
                    m1.Show();
                }
            }
        }

        public class TrainerItem
        {
            public int TrainerId { get; set; }
            public string TrainerName { get; set; }

            public TrainerItem(int trainerId, string trainerName)
            {
                TrainerId = trainerId;
                TrainerName = trainerName;
            }

            public override string ToString()
            {
                return TrainerName;
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

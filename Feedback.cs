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
using static System.Net.Mime.MediaTypeNames;

namespace Gym_Portal
{
    public partial class Feedback : Form
    {
        int AssignedTrainer = -1;
        public Feedback()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Member_Options memberInterface = new Member_Options();
            this.Hide();
            memberInterface.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True"; // Replace with your actual connection string


            // Provide the member_id for which you want to find the trainer_id
            Member_Login ml = new Member_Login();
            int memberId = ml.GetMemberId();
            string feedback = textBox1.Text;

            if (string.IsNullOrWhiteSpace(feedback) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Feedback or rating cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rating = int.Parse(comboBox1.Text);


            // SQL query to find the trainer_id for the given member_id
            string query = "SELECT trainer_id FROM Member_Trainer_Association WHERE member_id = @MemberId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for member_id
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    try
                    {
                        connection.Open();

                        // Execute the query and store the result in a variable
                        int trainerId = Convert.ToInt32(command.ExecuteScalar());
                        AssignedTrainer = trainerId;

                        if (AssignedTrainer == -1)
                        {
                            MessageBox.Show("Trainer must be assigned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (trainerId != 0)
                        {
                            Console.WriteLine($"Trainer ID for Member ID {memberId}: {trainerId}");
                        }
                        else
                        {
                            Console.WriteLine($"No trainer found for Member ID {memberId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO ClientFeedback (trainer_id, member_id, feedback_text, rating) VALUES (@TrainerId, @MemberId, @FeedbackText, @Rating)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@TrainerId", AssignedTrainer);
                command.Parameters.AddWithValue("@MemberId", memberId);
                command.Parameters.AddWithValue("@FeedbackText", feedback);
                command.Parameters.AddWithValue("@Rating", rating);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows Affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}

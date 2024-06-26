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
    public partial class Trainer_Appointment : Form
    {
        public Trainer_Appointment()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Trainer_Appointment_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
                Trainer_Login tl = new Trainer_Login();
                int trainer_id = tl.GetTrainerId();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Console.WriteLine(trainer_id);
                    string query = @"
                	SELECT * 
                    FROM Personal_Sessions
                    WHERE TrainerID = @trainer_id;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            command.Parameters.AddWithValue("@trainer_id", trainer_id);

                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (!int.TryParse(textBox1.Text, out int appointment_id) || appointment_id <= 0)
                {
                    MessageBox.Show("Please enter a valid appointment ID.");
                    return;
                }

                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("ID: ", appointment_id);
                        string updateQuery = "UPDATE Personal_Sessions SET Status = 'Cancelled' WHERE SessionID = @AppointmentID";
                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@AppointmentID", appointment_id);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Status updated successfully to 'Cancelled' for Appointment ID: " + appointment_id);
                               Trainer_Appointment_Load(null, null);
                        }
                            else
                            {
                                MessageBox.Show("No appointment found with the provided ID.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating status: " + ex.Message);
                    }
                }
            }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int appointment_id) || appointment_id <= 0)
            {
                MessageBox.Show("Please enter a valid appointment ID.");
                return;
            }

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("ID: ", appointment_id);
                    string updateQuery = "UPDATE Personal_Sessions SET Status = 'Approved' WHERE SessionID = @AppointmentID";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", appointment_id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status updated successfully to 'Approve' for Appointment ID: " + appointment_id);
                            Trainer_Appointment_Load(null,null);
                        }
                        else
                        {
                            MessageBox.Show("No appointment found with the provided ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int appointment_id) || appointment_id <= 0)
            {
                MessageBox.Show("Please enter a valid appointment ID.");
                return;
            }

            DateTime newDateTime = dateTimePicker1.Value;

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("ID: ", appointment_id);
                    string updateQuery = "UPDATE Personal_Sessions SET Status = 'Rescheduled', Date_and_Time = @NewDateTime WHERE SessionID = @AppointmentID";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewDateTime", newDateTime);
                        command.Parameters.AddWithValue("@AppointmentID", appointment_id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status and Date updated successfully for Appointment ID: " + appointment_id);
                            Trainer_Appointment_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No appointment found with the provided ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status and date: " + ex.Message);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Trainer_Options trainer_Options = new Trainer_Options();
            this.Hide();
            trainer_Options.Show();
        }
    }
}

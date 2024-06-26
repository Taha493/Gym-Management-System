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
    public partial class revoke_membership : Form
    {
        public revoke_membership()
        {
            InitializeComponent();
        }

        private void revoke_membership_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT G.gym_id, G.gym_name, G.location, G.phone, G.status, 
                       COUNT(MGA.member_id) AS attendance
                FROM Gym G
                LEFT JOIN Member_Gym_Association MGA ON G.gym_id = MGA.gym_id
                LEFT JOIN gym_member M ON MGA.member_id = M.member_id
                WHERE G.status = 'Approved'
                GROUP BY G.gym_id, G.gym_name, G.location, G.phone, G.status";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a gym ID.");
                return;
            }

            // Check if the entered text is a valid integer
            if (!int.TryParse(textBox1.Text, out int gymId))
            {
                MessageBox.Show("Please enter a valid gym ID.");
                return;
            }

            try
            {
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Begin a transaction
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Update Gym status to 'Revoked'
                        string updateQuery = @"UPDATE Gym SET status = 'Revoked' WHERE gym_id = @gymId";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@gymId", gymId);
                            updateCommand.ExecuteNonQuery();
                        }

                        // Delete rows from Gym Member Association table
                        string deleteMemberAssociationQuery = @"DELETE FROM Member_Gym_Association WHERE gym_id = @gymId";
                        using (SqlCommand deleteMemberAssociationCommand = new SqlCommand(deleteMemberAssociationQuery, connection, transaction))
                        {
                            deleteMemberAssociationCommand.Parameters.AddWithValue("@gymId", gymId);
                            deleteMemberAssociationCommand.ExecuteNonQuery();
                        }

                        // Delete rows from Gym Trainer Association table
                        string deleteTrainerAssociationQuery = @"DELETE FROM Trainer_Gym_Association WHERE gym_id = @gymId";
                        using (SqlCommand deleteTrainerAssociationCommand = new SqlCommand(deleteTrainerAssociationQuery, connection, transaction))
                        {
                            deleteTrainerAssociationCommand.Parameters.AddWithValue("@gymId", gymId);
                            deleteTrainerAssociationCommand.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                        revoke_membership_Load(null, null);
                        //MessageBox.Show("Gym status revoked and associated associations deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if an error occurs
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin_options go = new admin_options();
            this.Hide();
            go.Show();
        }
    }
}

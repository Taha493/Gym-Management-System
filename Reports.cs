using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Portal
{
    public partial class Reports : Form
    {

        Dictionary<string, int> trainerIdMap = new Dictionary<string, int>(); // Dictionary to map trainer names to IDs
        int selectedTrainerId = -1; // Variable to store the selected trainer ID
        Dictionary<string, int> gymIdMap = new Dictionary<string, int>();
        int selectedGymId = -1;
        Dictionary<string, int> DietIdMap = new Dictionary<string, int>();
        int selectedDietId = -1;
        public Reports()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // 1
            ShowTrainers(comboBox1);
            ShowGyms(comboBox2);

            //2
            ShowGyms(comboBox3);
            ShowDietPlans(comboBox4);

            //3
            ShowTrainers(comboBox6);
            ShowDietPlans(comboBox5);

            //4
            ShowGyms(comboBox8);

            ShowReport7();
            ShowReport8();
            ShowReport9();
            ShowReport10();
            ShowReport11();
            ShowReport12();
            ShowReport13();
            ShowReport14();
            ShowReport15();
            ShowReport16();
            ShowReport17();
            ShowReport18();
            ShowReport19();
            ShowReport20();

        }

        public void ShowTrainers(ComboBox c)
        {
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
                        trainerIdMap.Clear();

                        c.Items.Clear();

                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int trainerId = Convert.ToInt32(reader["trainer_id"]);
                            string fullName = $"{reader["first_name"]} {reader["last_name"]}";
                            string trainerInfo = $"{fullName}";
                            if (!gymIdMap.ContainsKey(fullName))
                            {
                                c.Items.Add(trainerInfo); // Add trainer info to combo box
                                trainerIdMap.Add(trainerInfo, trainerId); // Add trainer info and ID to dictionary
                            }

                        }
                    }
                }
            }
        }

        public void ShowGyms(ComboBox c)
        {
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
                        c.Items.Clear();

                        // Clear the dictionary
                        gymIdMap.Clear();

                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int gymId = Convert.ToInt32(reader["gym_id"]);
                            selectedGymId = gymId;
                            string gymName = reader["gym_name"].ToString();

                            // Check if the gym name already exists in the dictionary
                            if (!gymIdMap.ContainsKey(gymName))
                            {
                                c.Items.Add(gymName); // Add gym name to combo box
                                gymIdMap.Add(gymName, gymId); // Add gym name and ID to dictionary
                            }
                        }
                    }
                }
            }
        }


        public void ShowDietPlans(ComboBox c)
        {
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve data
                string query = "SELECT PlanID, MemberID FROM Diet_Plans";

                // Command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the connection
                    connection.Open();

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear the combo box
                        DietIdMap.Clear();

                        c.Items.Clear();

                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int planID = Convert.ToInt32(reader["PlanID"]);
                            selectedDietId = planID;
                            string fullId = $"{reader["PlanID"]} {reader["MemberID"]}";
                            if (!gymIdMap.ContainsKey(fullId))
                            {
                                c.Items.Add(planID); // Add trainer info to combo box
                                trainerIdMap.Add(fullId, planID); // Add trainer info and ID to dictionary
                            }

                        }
                    }
                }
            }
        }
        public void showReport1()
        {
            if (selectedGymId == -1 || selectedTrainerId == -1)
            {
                MessageBox.Show("Please select a gym and a trainer.");
                return;
            }

            string query = @"SELECT gm.first_name, gm.last_name, gm.gender, gm.dob, gm.phone_number
                     FROM gym_member gm
                     JOIN Member_Gym_Association mga ON gm.member_id = mga.member_id
                     JOIN Member_Trainer_Association mta ON gm.member_id = mta.member_id
                     JOIN Trainer t ON mta.trainer_id = t.trainer_id
                     JOIN Gym g ON mga.gym_id = g.gym_id
                     WHERE g.gym_id = @GymId
                     AND t.trainer_id = @TrainerId;";

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GymId", selectedGymId);
                    command.Parameters.AddWithValue("@TrainerId", selectedTrainerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        public void showReport2()
        {
            if (selectedGymId == -1)
            {
                MessageBox.Show("Please select a gym.");
                return;
            }

            string query = @"SELECT  gm.first_name, gm.last_name, gm.gender, gm.dob, gm.phone_number
                     FROM gym_member gm
                     JOIN Member_Gym_Association mga ON gm.member_id = mga.member_id
                     JOIN Diet_Plans dp ON gm.member_id = dp.MemberID
                     WHERE mga.gym_id = @GymId
                     AND dp.PlanID = (SELECT PlanID FROM Diet_Plans WHERE MemberID = gm.member_id AND PlanID = @PlanID);";

            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GymId", selectedGymId);
                    command.Parameters.AddWithValue("@PlanID", selectedDietId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;
                }
            }
        }

        public void ShowReport3()
        {
            string query = @"SELECT gm.first_name, gm.last_name, gm.gender, gm.dob, gm.phone_number
                        FROM gym_member gm
                        JOIN Member_Trainer_Association mta ON gm.member_id = mta.member_id
                        JOIN Diet_Plans dp ON gm.member_id = dp.MemberID
                        WHERE mta.trainer_id = @TrainerId
                        AND dp.PlanID = @PlanID";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TrainerId", selectedTrainerId);
                    command.Parameters.AddWithValue("@PlanID", selectedDietId);

                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView3.DataSource = dataTable;
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTrainer = comboBox1.SelectedItem.ToString();
            // Get the trainer's ID from the dictionary
            if (trainerIdMap.ContainsKey(selectedTrainer))
            {
                selectedTrainerId = trainerIdMap[selectedTrainer];
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGym = comboBox2.SelectedItem.ToString();
            // Get the trainer's ID from the dictionary

            if (trainerIdMap.ContainsKey(selectedGym))
            {
                selectedGymId = gymIdMap[selectedGym];
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGym = comboBox3.SelectedItem.ToString();

            if (trainerIdMap.ContainsKey(selectedGym))
            {
                selectedGymId = gymIdMap[selectedGym];
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPlan = comboBox4.SelectedItem.ToString();
            // Get the trainer's ID from the dictionary

            if (DietIdMap.ContainsKey(selectedPlan))
            {
                selectedDietId = DietIdMap[selectedPlan];
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPlan = comboBox6.SelectedItem.ToString();
            // Get the trainer's ID from the dictionary

            if (DietIdMap.ContainsKey(selectedPlan))
            {
                selectedDietId = DietIdMap[selectedPlan];
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTrainer = comboBox5.SelectedItem.ToString();

            if (trainerIdMap.ContainsKey(selectedTrainer))
            {
                selectedTrainerId = trainerIdMap[selectedTrainer];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            showReport1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showReport2();
        }



        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowReport3();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGym = comboBox8.SelectedItem.ToString();

            if (trainerIdMap.ContainsKey(selectedGym))
            {
                selectedGymId = gymIdMap[selectedGym];
            }
        }

        public void ShowReport4()
        {
            string query = @"SELECT COUNT(DISTINCT mmt.member_id) AS member_count
                            FROM MemberMuscleTargets mmt
                            JOIN Exercises e ON mmt.muscle_id = e.muscle_target_id
                            JOIN PlanDetails pd ON e.exercise_id = pd.exercise_id
                            JOIN WorkoutPlans wp ON pd.plan_id = wp.plan_id
                            JOIN gym_member gm ON wp.member_id = gm.member_id
                            JOIN Member_Gym_Association mga ON gm.member_id = mga.member_id
                            JOIN Gym g ON mga.gym_id = g.gym_id
                            WHERE g.gym_name = @GymName
                            AND e.exercise_name IN (@MachineName)";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MachineName", comboBox7.Text);
                    command.Parameters.AddWithValue("@GymName", comboBox8.Text);

                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView4.DataSource = dataTable;
                }
            }
        }


        public void ShowReport5()
        {
            string query = @"SELECT DISTINCT dp.* " +
                           "FROM Diet_Plans dp " +
                           "JOIN Meals m ON dp.PlanID = m.PlanID " +
                           "JOIN Meal_Nutrition mn ON m.MealID = mn.MealID " +
                           "WHERE m.Meal_Name = @MealName " +
                           "AND (mn.Protein + mn.Carbs + mn.Fiber + mn.Fat) < 500;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MealName", comboBox9.Text);

                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView5.DataSource = dataTable;
                }
            }
        }


        public void ShowReport6()
        {
            string query = @"SELECT DISTINCT dp.* 
                                FROM Diet_Plans dp 
                                JOIN Meals m ON dp.PlanID = m.PlanID 
                                JOIN Meal_Nutrition mn ON m.MealID = mn.MealID 
                                WHERE m.Meal_Name = @MealName 
                                AND mn.Carbs < 300;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MealName", comboBox10.Text);

                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView6.DataSource = dataTable;
                }
            }
        }


        public void ShowReport7()
        {
            string query = @"SELECT DISTINCT wp.*
                            FROM WorkoutPlans wp
                            JOIN PlanDetails pd ON wp.plan_id = pd.plan_id
                            JOIN Exercises e ON pd.exercise_id = e.exercise_id
                            WHERE e.exercise_name IN ('Push Ups', 'Pull Ups', 'Squats');";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView7.DataSource = dataTable;
                }
            }
        }

        public void ShowReport8()
        {
            string query = @"SELECT *
                            FROM Diet_Plans
                            WHERE Allergens IS NULL OR Allergens NOT LIKE '%peanuts%'";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView8.DataSource = dataTable;
                }
            }
        }

        public void ShowReport9()
        {
            string query = @"SELECT COUNT(*) AS total_pending_gyms FROM Gym WHERE status = 'Pending';";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView9.DataSource = dataTable;
                }
            }
        }

        public void ShowReport10() {

            string query = @"SELECT g.gym_name, COUNT(DISTINCT gm.member_id) AS total_members
                            FROM Gym g
                            JOIN Member_Gym_Association mga ON g.gym_id = mga.gym_id
                            JOIN gym_member gm ON mga.member_id = gm.member_id
                            GROUP BY g.gym_name;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView10.DataSource = dataTable;
                }
            }

        }

        public void ShowReport11()
        {

            string query = @"SELECT COUNT(*) AS total_trainers FROM Trainer;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView11.DataSource = dataTable;
                }
            }

        }


        public void ShowReport12()
        {

            string query = @"SELECT COUNT(*) AS total_approved_gyms FROM Gym WHERE status = 'Approved';";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView12.DataSource = dataTable;
                }
            }

        }



        public void ShowReport13()
        {

            string query = @"SELECT COUNT(DISTINCT muscle_id) AS total_unique_muscle_groups FROM MusclesTargeted;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView13.DataSource = dataTable;
                }
            }

        }



        public void ShowReport14()
        {

            string query = @"SELECT COUNT(*) AS total_pending_sessions FROM Personal_Sessions WHERE Status = 'Pending';";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView14.DataSource = dataTable;
                }
            }

        }




        public void ShowReport15()
        {

            string query = @"SELECT COUNT(*) AS total_diet_plans_with_allergens 
                            FROM Diet_Plans 
                            WHERE Allergens IS NOT NULL AND Allergens <> '';";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView15.DataSource = dataTable;
                }
            }

        }




        public void ShowReport16()
        {

            string query = @"SELECT COUNT(DISTINCT owner_name) AS total_gym_owners FROM GymOwners;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView16.DataSource = dataTable;
                }
            }

        }
        public void ShowReport17()
        {

            string query = @"SELECT COUNT(*) AS total_exercises FROM Exercises;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView17.DataSource = dataTable;
                }
            }

        }


        public void ShowReport18()
        {

            string query = @"SELECT COUNT(DISTINCT user_address) AS total_unique_addresses FROM gym_member;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView18.DataSource = dataTable;
                }
            }

        }



        public void ShowReport19()
        {

            string query = @"SELECT COUNT(DISTINCT specialty_areas) AS total_specialties FROM Trainer;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView19.DataSource = dataTable;
                }
            }

        }

        public void ShowReport20()
        {

            string query = @"SELECT COUNT(DISTINCT location) AS total_unique_locations FROM Gym;";

            // Create and open connection
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command with parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute command and read data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView20.DataSource = dataTable;
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowReport4();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowReport5();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowReport6();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            ShowReport6();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            admin_options ao = new admin_options();
            this.Hide();
            ao.Show();
        }
    }
}

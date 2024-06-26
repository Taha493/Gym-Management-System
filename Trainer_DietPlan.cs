using System;
using System.Collections;
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
    //int selectedTrainerId = -1;
    public partial class Trainer_DietPlan : Form
    {
        Dictionary<string, int> memberIdMap = new Dictionary<string, int>(); // Dictionary to map trainer names to IDs
        public Trainer_DietPlan()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Trainer_DietPlan_Load(object sender, EventArgs e)
        {
            Trainer_Login tl = new Trainer_Login();
            int trainer_id = tl.GetTrainerId();
            // Connect to the database
            string ConnectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SQL query to retrieve data
                string query = "SELECT  m.member_id, m.first_name + ' ' + m.last_name AS full_name FROM gym_member m JOIN Member_Trainer_Association mt ON m.member_id = mt.member_id WHERE mt.trainer_id = @trainer_id";

                // Command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the connection
                    connection.Open();
                    command.Parameters.AddWithValue("@trainer_id", trainer_id);

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear the combo box
                        comboBox1.Items.Clear();
                        //comboBox3.Items.Clear();
                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int memberId = Convert.ToInt32(reader["member_id"]);
                            string fullName = $"{reader["full_name"]}";
                            string memberInfo = $"{fullName}";
                           // comboBox3.Items.Add(memberInfo);
                            comboBox1.Items.Add(memberInfo); // Add member info to combo box
                            memberIdMap.Add(memberInfo, memberId); // Add member info and ID to dictionary
                        }

                    }
                }
            }
        }

        private int InsertDietPlan(string connectionString, int memberID, int trainerID, DateTime startDate, DateTime EndDate)
        {
            string query = @"INSERT INTO Diet_Plans (MemberID,TrainerID, Start_Date, End_Date)
                 OUTPUT INSERTED.PlanID
                 VALUES (@MemberID,@TrainerID, @StartDate, @EndDate)";

            int planID = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MemberID", memberID);
                command.Parameters.AddWithValue("@TrainerID", trainerID);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", EndDate);
                //command.Parameters.AddWithValue("@Allergens", Allergens);

                planID = (int)command.ExecuteScalar();
                Console.WriteLine($"PlanID inserted: {planID}");
            }

            return planID;
        }

        private int InsertMeal(string connectionString, int planId, string mealName, string description)
        {
            string query = @"INSERT INTO Meals (PlanID, Meal_Name, Description)
                     OUTPUT INSERTED.MealID
                     VALUES (@PlanID, @Meal_Name, @Description)";

            int mealID = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlanID", planId);
                command.Parameters.AddWithValue("@Meal_Name", mealName);
                command.Parameters.AddWithValue("@Description", description);

                mealID = (int)command.ExecuteScalar();
            }

            return mealID;
        }


        private void InsertMealNutrition(string connectionString, int mealID, decimal protein, decimal carbs, decimal fiber, decimal fat)
        {
            string query = @"INSERT INTO Meal_Nutrition (MealID, Protein, Carbs, Fiber, Fat)
                     VALUES (@MealID, @Protein, @Carbs, @Fiber, @Fat)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MealID", mealID);
                command.Parameters.AddWithValue("@Protein", protein);
                command.Parameters.AddWithValue("@Carbs", carbs);
                command.Parameters.AddWithValue("@Fiber", fiber);
                command.Parameters.AddWithValue("@Fat", fat);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Meal nutrition inserted successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to insert meal nutrition.");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int planId = 0;
            string connectionString = "";
            string selectedMemberInfo = comboBox1.SelectedItem.ToString();
            if (memberIdMap.ContainsKey(selectedMemberInfo))
            {
                int memberId = memberIdMap[selectedMemberInfo];
                Trainer_Login tl = new Trainer_Login();
                int trainer_id = tl.GetTrainerId();

                DateTime startDate = DateTime.Today;
                DateTime EndDate = startDate.AddDays(7);

                // Insert into Diet_Plans table
                connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
                planId = InsertDietPlan(connectionString, memberId, trainer_id, startDate, EndDate);
            }
            string mealName1 = label1.Text;
            string mealName2 = label2.Text;
            string mealName3 = label3.Text;

            // Get description from text boxes
            string description1 = textBox13.Text;
            string description2 = textBox8.Text;
            string description3 = textBox14.Text;

            // Insert into Meals table
            int mealID1 = InsertMeal(connectionString, planId, mealName1, description1);
            int mealID2 = InsertMeal(connectionString, planId, mealName2, description2);
            int mealID3 = InsertMeal(connectionString, planId, mealName3, description3);

            int protein1 = Convert.ToInt32(textBox1.Text);
            int carbs1 = Convert.ToInt32(textBox2.Text);
            int fiber1 = Convert.ToInt32(textBox3.Text);
            int fat1 = Convert.ToInt32(textBox4.Text);

            int protein2 = Convert.ToInt32(textBox9.Text);
            int carbs2 = Convert.ToInt32(textBox7.Text);
            int fiber2 = Convert.ToInt32(textBox6.Text);
            int fat2 = Convert.ToInt32(textBox5.Text);

            int protein3 = Convert.ToInt32(textBox15.Text);
            int carbs3 = Convert.ToInt32(textBox12.Text);
            int fiber3 = Convert.ToInt32(textBox11.Text);
            int fat3 = Convert.ToInt32(textBox10.Text);

            InsertMealNutrition(connectionString, mealID1, protein1, carbs1, fiber1, fat1);
            InsertMealNutrition(connectionString, mealID2, protein2, carbs2, fiber2, fat2);
            InsertMealNutrition(connectionString, mealID3, protein3, carbs3, fiber3, fat3);

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the trainer ID
                Trainer_Login tl = new Trainer_Login();
                int trainer_id = tl.GetTrainerId();

                // Your connection string
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to retrieve plans of a specific trainer
                    string query = @"
                SELECT 
                    dp.PlanID, 
                    dp.MemberID, 
                    m.MealID, 
                    m.Meal_Name, 
                    m.Description, 
                    mn.Protein, 
                    mn.Carbs, 
                    mn.Fiber, 
                    mn.Fat
                FROM 
                    Diet_Plans dp
                JOIN 
                    Meals m ON dp.PlanID = m.PlanID
                JOIN 
                    Meal_Nutrition mn ON m.MealID = mn.MealID
                WHERE 
                    dp.TrainerID = @trainer_id";

                    // Create command with the query
                    SqlCommand command = new SqlCommand(query, connection);

                    // Add parameters
                    command.Parameters.AddWithValue("@trainer_id", trainer_id);

                    // Create data adapter
                    SqlDataAdapter trainer_plans = new SqlDataAdapter(command);

                    // Fill data into DataTable
                    DataTable table = new DataTable();
                    trainer_plans.Fill(table);

                    // Bind data to DataGridView
                    dataGridView1.DataSource = table;

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log or display error message.
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }


    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the trainer ID
                Trainer_Login tl = new Trainer_Login();
                int trainer_id = tl.GetTrainerId();

                // Your connection string
                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to retrieve plans of a specific trainer
                    string query = @"
            SELECT 
            dp.PlanID, 
            dp.MemberID, 
            dp.TrainerID,
            m.MealID, 
            m.Meal_Name, 
            m.Description, 
            mn.Protein, 
            mn.Carbs, 
            mn.Fiber, 
            mn.Fat
        FROM 
            Diet_Plans dp
        JOIN 
            Meals m ON dp.PlanID = m.PlanID
        JOIN 
            Meal_Nutrition mn ON m.MealID = mn.MealID
        WHERE 
            dp.TrainerID = @trainer_id OR dp.TrainerID IS NULL;";

                    // Create command with the query
                    SqlCommand command = new SqlCommand(query, connection);

                    // Add parameters
                    command.Parameters.AddWithValue("@trainer_id", trainer_id);

                    // Create data adapter
                    SqlDataAdapter trainer_plans = new SqlDataAdapter(command);

                    // Fill data into DataTable
                    DataTable table = new DataTable();
                    trainer_plans.Fill(table);

                    // Bind data to DataGridView
                   // dataGridView2.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log or display error message.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Trainer_Options trainer_Options = new Trainer_Options();
            this.Hide();
            trainer_Options.Show();
        }
    }
}
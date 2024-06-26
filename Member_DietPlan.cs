using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Gym_Portal
{
    public partial class Member_DietPlan : Form
    {
        public Member_DietPlan()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Member_Options memberInterface = new Member_Options();
            this.Hide();
            memberInterface.Show();
        }

        private int InsertDietPlan(string connectionString, int memberID, DateTime startDate, DateTime EndDate, string Allergens)
        {
                string query = @"INSERT INTO Diet_Plans (MemberID, Start_Date, End_Date, Allergens)
                 OUTPUT INSERTED.PlanID
                 VALUES (@MemberID, @StartDate, @EndDate, @Allergens)";

                 int planID = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MemberID", memberID);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", EndDate);
                command.Parameters.AddWithValue("@Allergens", Allergens);

                planID = (int)command.ExecuteScalar();
                Console.WriteLine($"PlanID inserted: {planID}");
            }

            return planID;
        }


        private int InsertMeal(string connectionString, int PlanId, string meal_name,string description)
        {
            string query = @"INSERT INTO Meals (PlanID, Meal_Name, Description)
                 OUTPUT INSERTED.MealID
                 VALUES (@PlanId, @meal_name, @description)";

            int MealID = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlanId", PlanId);
                command.Parameters.AddWithValue("@meal_name", meal_name);
                command.Parameters.AddWithValue("@description", description);

                MealID = (int)command.ExecuteScalar();
            }

            return MealID;
        }


        private void InsertNutritions(string connectionString, int MealId, int proteins, int carbs, int fiber, int fat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string planDetailsQuery = "INSERT INTO Meal_Nutrition (MealID, Protein, Carbs, Fiber, Fat) VALUES (@MealId, @proteins, @carbs, @fiber, @fat)";
                using (SqlCommand command = new SqlCommand(planDetailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@MealId", MealId);
                    command.Parameters.AddWithValue("@proteins", proteins);
                    command.Parameters.AddWithValue("@carbs", carbs);
                    command.Parameters.AddWithValue("@fiber", fiber);
                    command.Parameters.AddWithValue("@fat", fat);


                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            // Breakfast
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string breakfast = comboBox1.Text;
            string Allergens = textBox1.Text;
            Member_Login ml = new Member_Login();
            int member_Id = ml.GetMemberId();
            DateTime startDate = DateTime.Today;
            DateTime EndDate = startDate.AddDays(7);

            int planID = InsertDietPlan(connectionString, member_Id, startDate, EndDate, Allergens);
            if (breakfast == "Oatmeal with Berries and Nuts: Protein: 12g, Fat: 18g, Carbs: 42g, Fiber: 10g.")
            {
                int mealId = InsertMeal(connectionString, planID, "breakfast", breakfast);
                InsertNutritions(connectionString, mealId, 12, 42, 10, 18);
                Console.WriteLine("1st option");

            }
            else if(breakfast == "Greek Yogurt Parfait: Protein: 23g, Fat: 2g, Carbs: 28g, Fiber: 2g.")
            {
                int mealId = InsertMeal(connectionString, planID, "breakfast", breakfast);
                InsertNutritions(connectionString, mealId, 12,28, 2, 2);
                Console.WriteLine("2nd option");
            }

            else if(breakfast == "Avocado Toast with Eggs: Protein: 21g, Fat: 22g, Carbs: 38g, Fiber: 12g.")
            {
                int mealId = InsertMeal(connectionString, planID, "breakfast", breakfast);
                InsertNutritions(connectionString, mealId, 21, 38, 12, 22);
                Console.WriteLine("3rd option");
            }

            else if(breakfast == "Smoked Salmon Bagel: Protein: 25g, Fat: 15g, Carbs: 35g, Fiber: 5g.")
            {
                int mealId = InsertMeal(connectionString, planID, "breakfast", breakfast);
                InsertNutritions(connectionString, mealId, 25, 35, 5, 15);
                Console.WriteLine("4th option");

            }

            else if(breakfast == "Omelette with Whole Grain Toast: Protein: 18g, Fat: 14g, Carbs: 30g, Fiber: 6g.")
            {
                int mealId = InsertMeal(connectionString, planID, "breakfast", breakfast);
                InsertNutritions(connectionString, mealId, 18, 30, 6, 14);
                Console.WriteLine("5th option");

            }

            // Lunch
            string lunch = comboBox2.Text;
            if (lunch == "Grilled Chicken Salad: Protein: 25g, Fat: 20g, Carbs: 20g, Fiber: 10g.")
            {
                int mealId = InsertMeal(connectionString, planID, "lunch", lunch);
                InsertNutritions(connectionString, mealId, 25, 20, 10, 20);
                Console.WriteLine("1st option");
            }
            else if (lunch == "Quinoa Salad with Chickpeas and Feta: Protein: 19g, Fat: 12g, Carbs: 62g, Fiber: 11g.")
            {
                int mealId = InsertMeal(connectionString, planID, "lunch", lunch);
                InsertNutritions(connectionString, mealId, 19, 62, 11, 12);
                Console.WriteLine("2nd option");
            }

            else if (lunch == "Turkey and Avocado Wrap: Protein: 27g, Fat: 11g, Carbs: 24g, Fiber: 5g.")
            {
                int mealId = InsertMeal(connectionString, planID, "lunch", lunch);
                InsertNutritions(connectionString, mealId, 27, 24, 5, 27);
                Console.WriteLine("3rd option");
            }

            else if (lunch == "Tofu Scramble with Mixed Vegetables: Protein: 20g, Fat: 10g, Carbs: 30g, Fiber: 8g.")
            {
                int mealId = InsertMeal(connectionString, planID, "lunch", lunch);
                InsertNutritions(connectionString, mealId, 20, 30, 8, 10);
                Console.WriteLine("4th option");

            }

            else if (lunch == "Shrimp and Mango Salad: Protein: 22g, Fat: 8g, Carbs: 35g, Fiber: 6g.")
            {
                int mealId = InsertMeal(connectionString, planID, "lunch", lunch);
                InsertNutritions(connectionString, mealId, 22, 35, 6, 8);
                Console.WriteLine("5th option");
            }

            // Dinner 
            string dinner = comboBox3.Text;
            if (dinner == "Baked Salmon with Sweet Potato and Broccoli: Protein: 32g, Fat: 15g, Carbs: 35g, Fiber: 9g.")
            {
                int mealId = InsertMeal(connectionString, planID, "dinner", dinner);
                InsertNutritions(connectionString, mealId, 32, 35, 9, 15);
                Console.WriteLine("1st option");
            }
            else if (dinner == "Stir - Fried Tofu with Vegetables and Brown Rice: Protein: 27g, Fat: 9g, Carbs: 92g, Fiber: 12g.")
            {
                int mealId = InsertMeal(connectionString, planID, "dinner", dinner);
                InsertNutritions(connectionString, mealId, 27, 92, 12, 9);
                Console.WriteLine("2nd option");
            }

            else if (dinner == "Grilled Chicken with Quinoa and Steamed Asparagus: Protein: 36g, Fat: 7g, Carbs: 44g, Fiber: 8g.")
            {
                int mealId = InsertMeal(connectionString, planID, "dinner", dinner);
                InsertNutritions(connectionString, mealId, 36, 44, 8, 7);
                Console.WriteLine("3rd option");
            }

            else if (dinner == "Beef Stir-Fry with Bell Peppers and Snow Peas: Protein: 30g, Fat: 18g, Carbs: 40g, Fiber: 7g.")
            {
                int mealId = InsertMeal(connectionString, planID, "dinner", dinner);
                InsertNutritions(connectionString, mealId, 30, 40, 7, 18);
                Console.WriteLine("4th option");

            }

            else if (dinner == "Lentil Soup with Whole Grain Bread: Protein: 15g, Fat: 4g, Carbs: 50g, Fiber: 14g.")
            {
                int mealId = InsertMeal(connectionString, planID, "dinner", dinner);
                InsertNutritions(connectionString, mealId, 15, 50, 14, 4);
                Console.WriteLine("5th option");

            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void Member_DietPlan_Load(object sender, EventArgs e)
        {
            try
            {
                Member_Login ml = new Member_Login();
                int member_id = ml.GetMemberId();

                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

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
 WHERE dp.MemberID = @member_id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@member_id", member_id);

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



            try
            {
                Member_Login ml = new Member_Login();
                int member_id = ml.GetMemberId();

                string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

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
    Meal_Nutrition mn ON m.MealID = mn.MealID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@member_id", member_id);

                    SqlDataAdapter member_plans = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    member_plans.Fill(table);

                    dataGridView2.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log or display error message.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        } 
    }
}

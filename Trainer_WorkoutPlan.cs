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

    public partial class Trainer_WorkoutPlan : Form
    {
        int memberId = 0;
        public string member_fitness_goal = "";
        Dictionary<string, int> memberIdMap = new Dictionary<string, int>(); // Dictionary to map trainer names to IDs
        public Trainer_WorkoutPlan()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Trainer_WorkoutPlan_Load(object sender, EventArgs e)
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
                        comboBox3.Items.Clear();
                        // Read data from the SqlDataReader and populate the combo box
                        while (reader.Read())
                        {
                            int memberId = Convert.ToInt32(reader["member_id"]);
                            string fullName = $"{reader["full_name"]}";
                            string memberInfo = $"{fullName}";
                            comboBox3.Items.Add(memberInfo);
                            memberIdMap.Add(memberInfo, memberId); // Add member info and ID to dictionary
                        }

                    }
                }
            }
          
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMemberName = comboBox3.Text;
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            // SQL query to retrieve the member ID based on the selected member name
            string query = "SELECT member_id FROM gym_member WHERE CONCAT(first_name, ' ', last_name) = @MemberName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MemberName", selectedMemberName);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        memberId = Convert.ToInt32(reader["member_id"]);
                        // Now you can use memberId as needed
                        // For example, you might want to store it in a global variable
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine(ex.Message);
                }
                Console.Write(memberId);
            }
        }

        private int GetPlanIDbyGoalID(int goalId)
        {
            int planID = -1;
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            string query = "SELECT plan_id FROM WorkoutPlans WHERE goal_id = @goalId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@goalId", goalId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        planID = Convert.ToInt32(result);
                    }
                }
            }

            return planID;
        }

        static int InsertMuscleIfNotExists(SqlConnection connection, string muscleName)
        {
            string selectQuery = "SELECT muscle_id FROM MusclesTargeted WHERE muscle_name = @MuscleName;";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@MuscleName", muscleName);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }

            // If muscle doesn't exist, insert it
            return InsertMuscle(connection, muscleName);
        }

        static int InsertMuscle(SqlConnection connection, string muscleName)
        {
            string insertQuery = "INSERT INTO MusclesTargeted (muscle_name) VALUES (@MuscleName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@MuscleName", muscleName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        static void InsertMemberMuscleTarget(SqlConnection connection, int memberId, int muscleId, string targetDay)
        {
            // Insert into MemberMuscleTargets without checking if it already exists
            string insertQuery = "INSERT INTO MemberMuscleTargets (member_id, muscle_id, target_day) VALUES (@MemberId, @MuscleId, @TargetDay);";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@MemberId", memberId);
                insertCommand.Parameters.AddWithValue("@MuscleId", muscleId);
                insertCommand.Parameters.AddWithValue("@TargetDay", targetDay);
                insertCommand.ExecuteNonQuery();
            }
        }

        private int GetGoalId(string connectionString, string goalName)
        {
            int goalId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT goal_id FROM FitnessGoals WHERE goal_name = @goalName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@goalName", goalName);
                    try
                    {
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            goalId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
            }
            return goalId;
        }

        private int GetMuscleId(string muscleName)
        {
            int muscleId = -1;
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            string query = "SELECT muscle_id FROM MusclesTargeted WHERE muscle_name = @muscleName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@muscleName", muscleName);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        muscleId = Convert.ToInt32(result);
                    }
                }
            }

            return muscleId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string fitness_goal = checkedListBox1.Items[i].ToString();
                        member_fitness_goal = fitness_goal;
                        Console.WriteLine(fitness_goal);

                        // SQL query to insert the string into the table
                        string query = "INSERT INTO FitnessGoals (goal_name) VALUES (@fitness_goal)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@fitness_goal", fitness_goal);

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

                // Get muscle names from textboxes
                string MondayMuscle = comboBox10.Text;
                string TuesdayMuscle = comboBox4.Text;
                string WednesdayMuscle = comboBox5.Text;
                string ThursdayMuscle = comboBox6.Text;
                string FridayMuscle = comboBox7.Text;
                string SaturdayMuscle = comboBox8.Text;
                string SundayMuscle = comboBox9.Text;

                // Insert muscle names into MusclesTargeted table
                int MondayMuscleId = InsertMuscleIfNotExists(connection, MondayMuscle);
                int TuesdayMuscleId = InsertMuscleIfNotExists(connection, TuesdayMuscle);
                int WednesdayMuscleId = InsertMuscleIfNotExists(connection, WednesdayMuscle);
                int ThursdayMuscleId = InsertMuscleIfNotExists(connection, ThursdayMuscle);
                int FridayMuscleId = InsertMuscleIfNotExists(connection, FridayMuscle);
                int SaturdayMuscleId = InsertMuscleIfNotExists(connection, SaturdayMuscle);
                int SundayMuscleId = InsertMuscleIfNotExists(connection, SundayMuscle);

                // Get the number of members in the map
                int memberCount = memberIdMap.Count;

                Trainer_Login t1 = new Trainer_Login();
                int trainerId = t1.GetTrainerId();
                // Get the keys (member names) from the dictionary
                InsertMemberMuscleTarget(connection, memberId, MondayMuscleId, "Monday");
                InsertMemberMuscleTarget(connection, memberId, TuesdayMuscleId, "Tuesday");
                InsertMemberMuscleTarget(connection, memberId, WednesdayMuscleId, "Wednesday");
                InsertMemberMuscleTarget(connection, memberId, ThursdayMuscleId, "Thursday");
                InsertMemberMuscleTarget(connection, memberId, FridayMuscleId, "Friday");
                InsertMemberMuscleTarget(connection, memberId, SaturdayMuscleId, "Saturday");
                InsertMemberMuscleTarget(connection, memberId, SundayMuscleId, "Sunday");


                // EXERCISE NAME AND MUSCLE TARGET ID IN EXERCISES TABLE
                if (checkedListBox2.GetItemChecked(0))
                {
                    string exercise_name = checkedListBox2.Items[0].ToString();
                    string muscle_name = "arms";
                    int muscle_id = GetMuscleId(muscle_name);
                    string query = "INSERT INTO Exercises (exercise_name, muscle_target_id) VALUES (@exerciseName, @muscle_id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseName", exercise_name);
                        command.Parameters.AddWithValue("@muscle_id", muscle_id);

                        try
                        {
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


            if (checkedListBox2.GetItemChecked(1))
            {
                string exercise_name = checkedListBox2.Items[1].ToString();
                string muscle_name = "biceps";
                int muscle_id = GetMuscleId(muscle_name);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Exercises (exercise_name, muscle_target_id) VALUES (@exerciseName, @muscle_id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseName", exercise_name);
                        command.Parameters.AddWithValue("@muscle_id", muscle_id);

                        try
                        {
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

            if (checkedListBox2.GetItemChecked(2))
            {
                string exercise_name = checkedListBox2.Items[2].ToString();
                string muscle_name = "legs";
                int muscle_id = GetMuscleId(muscle_name);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Exercises (exercise_name, muscle_target_id) VALUES (@exerciseName, @muscle_id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseName", exercise_name);
                        command.Parameters.AddWithValue("@muscle_id", muscle_id);

                        try
                        {
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

            if (checkedListBox2.GetItemChecked(3))
            {
                string exercise_name = checkedListBox2.Items[3].ToString();
                string muscle_name = "legs";
                int muscle_id = GetMuscleId(muscle_name);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Exercises (exercise_name, muscle_target_id) VALUES (@exerciseName, @muscle_id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseName", exercise_name);
                        command.Parameters.AddWithValue("@muscle_id", muscle_id);

                        try
                        {
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

            if (checkedListBox2.GetItemChecked(4))
            {
                string exercise_name = checkedListBox2.Items[4].ToString();
                string muscle_name = "back";
                int muscle_id = GetMuscleId(muscle_name);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Exercises (exercise_name, muscle_target_id) VALUES (@exerciseName, @muscle_id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseName", exercise_name);
                        command.Parameters.AddWithValue("@muscle_id", muscle_id);

                        try
                        {
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

            if (checkedListBox2.GetItemChecked(5))
            {
                string exercise_name = checkedListBox2.Items[5].ToString();
                string muscle_name = "chest";
                int muscle_id = GetMuscleId(muscle_name);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Exercises (exercise_name, muscle_target_id) VALUES (@exerciseName, @muscle_id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseName", exercise_name);
                        command.Parameters.AddWithValue("@muscle_id", muscle_id);

                        try
                        {
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


            Trainer_Login tl = new Trainer_Login();
            int TrainerId = tl.GetTrainerId();
            int goalId = GetGoalId(connectionString, member_fitness_goal);
            DateTime startDate = DateTime.Today;
            DateTime EndDate = startDate.AddMonths(1);
            string query2 = "INSERT INTO WorkoutPlans (trainer_id, member_id, goal_id, start_date, end_date) VALUES (@trainer_id, @MemberId, @goalId,@startDate, @EndDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@trainer_id", TrainerId);
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@goalId", goalId);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);

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

            // Check if the checkboxes are checked and insert data accordingly
            if (checkedListBox2.GetItemChecked(0))
            {
                InsertPlanDetails(connectionString, textBox28.Text, textBox29.Text, textBox30.Text, GetExerciseId(connectionString, checkedListBox2.Items[0].ToString()), GetPlanIDbyGoalID(GetGoalId(connectionString, member_fitness_goal)));
            }
            if (checkedListBox2.GetItemChecked(1))
            {
                InsertPlanDetails(connectionString, textBox1.Text, textBox2.Text, textBox3.Text, GetExerciseId(connectionString, checkedListBox2.Items[1].ToString()), GetPlanIDbyGoalID(GetGoalId(connectionString, member_fitness_goal)));
            }
            if (checkedListBox2.GetItemChecked(2))
            {
                InsertPlanDetails(connectionString, textBox4.Text, textBox5.Text, textBox6.Text, GetExerciseId(connectionString, checkedListBox2.Items[2].ToString()), GetPlanIDbyGoalID(GetGoalId(connectionString, member_fitness_goal)));
            }
            if (checkedListBox2.GetItemChecked(3))
            {
                InsertPlanDetails(connectionString, textBox7.Text, textBox8.Text, textBox9.Text, GetExerciseId(connectionString, checkedListBox2.Items[3].ToString()), GetPlanIDbyGoalID(GetGoalId(connectionString, member_fitness_goal)));
            }
            if (checkedListBox2.GetItemChecked(4))
            {
                InsertPlanDetails(connectionString, textBox10.Text, textBox11.Text, textBox12.Text, GetExerciseId(connectionString, checkedListBox2.Items[4].ToString()), GetPlanIDbyGoalID(GetGoalId(connectionString, member_fitness_goal)));
            }
            if (checkedListBox2.GetItemChecked(5))
            {
                InsertPlanDetails(connectionString, textBox13.Text, textBox14.Text, textBox15.Text, GetExerciseId(connectionString, checkedListBox2.Items[5].ToString()), GetPlanIDbyGoalID(GetGoalId(connectionString, member_fitness_goal)));
            }
        }


        private void InsertPlanDetails(string connectionString, string sets, string reps, string rest, int exerciseId, int planId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string planDetailsQuery = "INSERT INTO PlanDetails (plan_id, exercise_id, sets, reps, rest_intervals) VALUES (@planId, @exercise_id, @sets, @reps, @rest_intervals)";
                using (SqlCommand command = new SqlCommand(planDetailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@planId", planId);
                    command.Parameters.AddWithValue("@exercise_id", exerciseId);
                    command.Parameters.AddWithValue("@sets", sets);
                    command.Parameters.AddWithValue("@reps", reps);
                    command.Parameters.AddWithValue("@rest_intervals", rest);




                    try
                    {
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

        private int GetExerciseId(string connectionString, string exerciseName)
        {
            int exerciseId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT exercise_id FROM Exercises WHERE exercise_name = @exercise_name";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@exercise_name", exerciseName);
                    try
                    {
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            exerciseId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
            }
            return exerciseId;
        }



        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox listBox = (CheckedListBox)sender;
            if (listBox.GetItemChecked(0))
            {
                textBox28.ReadOnly = false;
                textBox29.ReadOnly = false;
                textBox30.ReadOnly = false;
            }
            else
            {
                textBox28.ReadOnly = true;
                textBox29.ReadOnly = true;
                textBox30.ReadOnly = true;
            }

            if (listBox.GetItemChecked(1))
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }

            if (listBox.GetItemChecked(2))
            {
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
            }
            else
            {
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
            }

            if (listBox.GetItemChecked(3))
            {
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox9.ReadOnly = false;
            }
            else
            {
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
            }

            if (listBox.GetItemChecked(4))
            {
                textBox10.ReadOnly = false;
                textBox11.ReadOnly = false;
                textBox12.ReadOnly = false;
            }
            else
            {
                textBox10.ReadOnly = true;
                textBox11.ReadOnly = true;
                textBox12.ReadOnly = true;
            }

            if (listBox.GetItemChecked(5))
            {
                textBox13.ReadOnly = false;
                textBox14.ReadOnly = false;
                textBox15.ReadOnly = false;
            }
            else
            {
                textBox13.ReadOnly = true;
                textBox14.ReadOnly = true;
                textBox15.ReadOnly = true;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox listBox = (CheckedListBox)sender;

            // If more than one item is checked, uncheck all except the currently selected one
            if (listBox.CheckedItems.Count > 1)
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    if (listBox.GetItemChecked(i) && listBox.SelectedIndex != i)
                    {
                        listBox.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Trainer_Options trainer_Options = new Trainer_Options();
            this.Hide();
            trainer_Options.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                try
                {
                    string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True"; // Replace with your actual connection string

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = @"
                        SELECT 
                            wp.plan_id,
                            mt.muscle_name,
                            e.exercise_name,
                            pd.sets,
                            pd.reps,
                            pd.rest_intervals,
                            mmt.target_day
                        FROM 
                            WorkoutPlans wp
                        INNER JOIN 
                            PlanDetails pd ON wp.plan_id = pd.plan_id
                        INNER JOIN 
                            Exercises e ON pd.exercise_id = e.exercise_id
                        INNER JOIN 
                            MemberMuscleTargets mmt ON wp.member_id = mmt.member_id
                        INNER JOIN 
                            MusclesTargeted mt ON mmt.muscle_id = mt.muscle_id
                        WHERE 
                            wp.trainer_id = @trainerId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            Trainer_Login t1 = new Trainer_Login();

                            // Assuming you have a method to get the trainer ID
                            int trainerId = t1.GetTrainerId(); // Replace with your actual method to get the trainer ID
                            command.Parameters.AddWithValue("@trainerId", trainerId);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

        } 
    
}

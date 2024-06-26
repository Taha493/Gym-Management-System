using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gym_Portal
{
    public partial class Member_WorkoutPlan : Form
    {
        public string member_fitness_goal = "";
        public Member_WorkoutPlan()
        {
            InitializeComponent();
        }

        private void muscleTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exerciseRoutineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void buildMusclesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Member_Options memberInterface = new Member_Options();
            this.Hide();
            memberInterface.Show();
        }

        private void toolStripMenuItem8_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox10_Click(object sender, EventArgs e)
        {

        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {

            // FITNESS GOAL
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string fitness_goal = checkedListBox1.Items[i].ToString();
                    member_fitness_goal = fitness_goal;
                    Console.WriteLine(fitness_goal);

                    // SQL query to insert the string into the table
                    string query = "INSERT INTO FitnessGoals (goal_name) VALUES (@fitness_goal)";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
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

                    Member_Login ml = new Member_Login();
                    int MemberId = ml.GetMemberId();
                    int goalId = GetGoalId(connectionString, fitness_goal);
                    DateTime startDate = DateTime.Today;
                    DateTime EndDate = startDate.AddMonths(1);
                    string query2 = "INSERT INTO WorkoutPlans (member_id, goal_id, start_date, end_date) VALUES (@MemberId, @goalId,@startDate, @EndDate)";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@MemberId", MemberId);
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

                }
            }


            // EXERCISE NAME AND MUSCLE TARGET ID IN EXERCISES TABLE
            if (checkedListBox2.GetItemChecked(0))
            {
                string exercise_name = checkedListBox2.Items[0].ToString();
                string muscle_name = "arms";
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


            // TARGET MUSCLE ON WHICH DAY

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();



                // Get muscle names from textboxes
                string MondayMuscle = comboBox3.Text;
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

                // Insert into MemberMuscleTargets
                InsertMemberMuscleTarget(connection, MondayMuscleId, "Monday");
                InsertMemberMuscleTarget(connection, TuesdayMuscleId, "Tuesday");
                InsertMemberMuscleTarget(connection, WednesdayMuscleId, "Wednesday");
                InsertMemberMuscleTarget(connection, ThursdayMuscleId, "Thursday");
                InsertMemberMuscleTarget(connection, FridayMuscleId, "Friday");
                InsertMemberMuscleTarget(connection, SaturdayMuscleId, "Saturday");
                InsertMemberMuscleTarget(connection, SundayMuscleId, "Sunday");


            }




            //INSERTION IN PLAN DETAILS

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

            Console.WriteLine("Data inserted successfully.");
            Console.ReadLine();
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

        static void InsertMemberMuscleTarget(SqlConnection connection, int muscleId, string targetDay)
        {
            Member_Login ml = new Member_Login();
            int MemberId = ml.GetMemberId();
            string selectQuery = "SELECT COUNT(*) FROM MemberMuscleTargets WHERE member_id = @MemberId AND muscle_id = @MuscleId AND target_day = @TargetDay;";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@MemberId", MemberId);
                command.Parameters.AddWithValue("@MuscleId", muscleId);
                command.Parameters.AddWithValue("@TargetDay", targetDay);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                {
                    // Insert into MemberMuscleTargets if not already present
                    string insertQuery = "INSERT INTO MemberMuscleTargets (member_id, muscle_id, target_day) VALUES (@MemberId, @MuscleId, @TargetDay);";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@MemberId", MemberId);
                        insertCommand.Parameters.AddWithValue("@MuscleId", muscleId);
                        insertCommand.Parameters.AddWithValue("@TargetDay", targetDay);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Member_WorkoutPlan_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                	 SELECT
	pd.plan_id,
    wp.member_id,
    fg.goal_name,
    wp.start_date,
    wp.end_date,
    mt.muscle_name,
    mmt.target_day,
    e.exercise_name,
    pd.sets,
    pd.reps,
    pd.rest_intervals
FROM
    WorkoutPlans wp
    JOIN FitnessGoals fg ON wp.goal_id = fg.goal_id
    JOIN PlanDetails pd ON wp.plan_id = pd.plan_id
    JOIN Exercises e ON pd.exercise_id = e.exercise_id
    JOIN MusclesTargeted mt ON e.muscle_target_id = mt.muscle_id
    JOIN MemberMuscleTargets mmt ON mt.muscle_id = mmt.muscle_id AND wp.member_id = mmt.member_id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView2.DataSource = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


            try
            {
                Member_Login ml = new Member_Login();
                int member_id = ml.GetMemberId();


                //  string connectionString = "Data Source=TM\\SQLEXPRESS;Initial Catalog=\"Flex Trainer\";Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                 SELECT
	pd.plan_id,
    fg.goal_name,
    wp.start_date,
    wp.end_date,
    mt.muscle_name,
    mmt.target_day,
    e.exercise_name,
    pd.sets,
    pd.reps,
    pd.rest_intervals
FROM
    WorkoutPlans wp
    JOIN FitnessGoals fg ON wp.goal_id = fg.goal_id
    JOIN PlanDetails pd ON wp.plan_id = pd.plan_id
    JOIN Exercises e ON pd.exercise_id = e.exercise_id
    JOIN MusclesTargeted mt ON e.muscle_target_id = mt.muscle_id
    JOIN MemberMuscleTargets mmt ON mt.muscle_id = mmt.muscle_id AND wp.member_id = mmt.member_id
WHERE
    wp.member_id = @member_id;
";

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

            Member_Login m1 = new Member_Login();
            int memberUserID = m1.GetMemberId(); // Replace with the actual member user ID

            int trainerId;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string trainerIdQuery = @"
                    SELECT 
                        Trainer.trainer_id
                    FROM 
                        Trainer
                    INNER JOIN 
                        Member_Trainer_Association ON Trainer.trainer_id = Member_Trainer_Association.trainer_id
                    INNER JOIN 
                        gym_member ON Member_Trainer_Association.member_id = gym_member.member_id
                    WHERE 
                        gym_member.member_id = @memberUserID";

                using (SqlCommand trainerIdCommand = new SqlCommand(trainerIdQuery, connection))
                {
                    trainerIdCommand.Parameters.AddWithValue("@memberUserID", memberUserID);
                    object result = trainerIdCommand.ExecuteScalar();
                    if (result != null)
                    {
                        trainerId = (int)result;

                        string sqlQuery = @"
                            SELECT 
                                wp.plan_id,
                                wp.member_id,
                                fg.goal_name,
                                wp.start_date,
                                wp.end_date,
                                mt.muscle_name,
                                mmt.target_day,
                                e.exercise_name,
                                pd.sets,
                                pd.reps,
                                pd.rest_intervals
                            FROM 
                                WorkoutPlans wp
                            INNER JOIN 
                                FitnessGoals fg ON wp.goal_id = fg.goal_id
                            INNER JOIN 
                                MemberMuscleTargets mmt ON wp.member_id = mmt.member_id
                            INNER JOIN 
                                MusclesTargeted mt ON mmt.muscle_id = mt.muscle_id
                            INNER JOIN 
                                PlanDetails pd ON wp.plan_id = pd.plan_id
                            INNER JOIN 
                                Exercises e ON pd.exercise_id = e.exercise_id
                            WHERE 
                                wp.trainer_id = @trainerId";

                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@trainerId", trainerId);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Create a DataTable to hold the data
                                DataTable dataTable = new DataTable();

                                // Load the data from the reader into the DataTable
                                dataTable.Load(reader);

                                // Bind the DataTable to your DataGridView
                               // dataGridView3.DataSource = dataTable;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Trainer not found for the logged-in member.");
                    }
                }
            }
        }
    }
}



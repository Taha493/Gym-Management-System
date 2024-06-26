using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Portal
{
    public partial class Trainer_Options : Form
    {
        public Trainer_Options()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trainer_WorkoutPlan trainerWorkout = new Trainer_WorkoutPlan();
            this.Hide();
            trainerWorkout.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trainer_DietPlan trainerDietPlan = new Trainer_DietPlan();
            this.Hide();
            trainerDietPlan.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trainer_Appointment appointment = new Trainer_Appointment();
            this.Hide();
            appointment.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trainer_Feedback trainerFeedback = new Trainer_Feedback();
            this.Hide();
            trainerFeedback.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }
    }
}

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
    public partial class GymOwner_Options : Form
    {
        public GymOwner_Options()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_MemberReport gymOwner_MemberReport = new GymOwner_MemberReport();
            this.Hide();
            gymOwner_MemberReport.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_TrainerReport gymOwner_TrainerReport = new GymOwner_TrainerReport();
            this.Hide();
            gymOwner_TrainerReport.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_AddTrainer gymOwner_AddTrainer = new GymOwner_AddTrainer();
            this.Hide();
            gymOwner_AddTrainer.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_AccountManagement ManageAccount= new GymOwner_AccountManagement();
            this.Hide();
            ManageAccount.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_GymRegister register = new GymOwner_GymRegister();
            this.Hide();
            register.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

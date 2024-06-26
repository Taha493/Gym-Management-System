using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Data Source=TM\SQLEXPRESS;Initial Catalog=Gym;Integrated Security=True

namespace Gym_Portal
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Admin_Login admin = new Admin_Login();
            this.Hide();
            admin.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Member_Login memberLogin = new Member_Login();
            this.Hide();
            memberLogin.Show();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trainer_Login trainerLogin = new Trainer_Login();
            this.Hide();
            trainerLogin.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GymOwner_Login gym_owner = new GymOwner_Login();
            this.Hide();
            gym_owner.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}

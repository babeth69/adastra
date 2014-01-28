﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace edb_tool
{
    public partial class Login : Form
    {
        MainForm mainform;

        public Login(MainForm mainform)
        {
            InitializeComponent();
            this.CenterToScreen();

            this.mainform = mainform;

            label3.Visible = false;

            this.CenterToScreen();
            this.AcceptButton = button1;
        }

        //checks the password
        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;

            mainform.curr.UserID = -1;
            bool autheticated = false;
            try
            {
                //var o = new localhost.HelloExample();
               // userid = o.Authenticate(textBox1.Text, textBox2.Text);
                ProviderFactory.SetWebProvider((string)comboBox1.SelectedValue);

                autheticated = Helper.VerifyPassword(textBox1.Text, textBox2.Text);
 
                //ProviderFactory.GetDataProvider().VerifyUserPassword(textBox1.Text, textBox2.Text, out mainform.curr.UserID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
                return;
            }

            if (autheticated)
            {
                //TODO: optimize code not to use all users
                GUser user = (from GUser u in ProviderFactory.GetDataProvider().ListUsers()
                              where u.Username == textBox1.Text
                              select u).First();

                mainform.curr.UserID = user.iduser;
                mainform.Text += " - " + user.Username;

                mainform.tabControl1.Enabled = true;
                this.Close();
            }
            else
            {
                label3.Visible = true;
            }
        }

        //disable X button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = ProviderFactory.GetWebProvidersList();
        }
    }
}
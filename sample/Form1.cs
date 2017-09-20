using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            if (IsValidated())
            {
                bool usernamev, passwordv;

                GetAuthenticated(out usernamev, out passwordv);
                {

                    if (usernamev && passwordv)
                    {
                        MessageBox.Show("Successfully Login");
                    }
                    else
                    {
                        if (!usernamev)
                        {
                            MessageBox.Show("username not correct");
                        }
                        else
                        {
                            MessageBox.Show("password not correct");
                        }
                    }






                }
            }
        }
        //private void GetAuthenticated(out bool usernamev, out bool passwordv)

        private bool GetAuthenticated(out bool usernamev, out bool passwordv)
        {
            string connctionstring = ConfigurationManager.ConnectionStrings["t"].ConnectionString;
            SqlConnection conn = new SqlConnection(connctionstring);
            SqlCommand cmd = new SqlCommand("sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            cmd.Parameters.Add("@isusername", SqlDbType.Bit).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ispassword", SqlDbType.Bit).Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@username", usernametextBox.Text);
            cmd.Parameters.AddWithValue("@password", passwordtextBox.Text);
            cmd.ExecuteNonQuery();


            usernamev =(bool)cmd.Parameters["@isusername"].Value;
            passwordv = (bool)cmd.Parameters["@ispassword"].Value;

            return true;

        }






        private bool IsValidated()
        {
            if (usernametextBox.Text.Trim() == string.Empty)
            {
               MessageBox.Show("username is required you cannot empty this");
                return false;
            }
            if (passwordtextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("password is required you cannot empty this");
                return false;
            }
            return true;
        }
    }
}

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

namespace CompuStoreX
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string sql = "SELECT * FROM UserInfo WHERE Usedd = '" + this.txtUserId.Text.ToUpper() + "' AND Password = '" + this.txtPassword.Text + "';";

            using (SqlConnection sqlcon = new SqlConnection("Data Source=LAPTOP-HASIB;Initial Catalog=NSummerDB;Persist Security Info=True;User ID=sa;Password=P@$$w0rd"))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    var name = ds.Tables[0].Rows[0]["Usedd"].ToString();
                    MessageBox.Show("Valid User");
                    this.Hide();

                    // Check the Type column (index 1) for user role
                    if (ds.Tables[0].Rows[0]["Type"].ToString().ToLower() == "admin")
                    {
                        new FormAdmin(name, this).Show();
                    }
                    else if (ds.Tables[0].Rows[0]["Type"].ToString().ToLower().Equals("salesman"))
                    {
                        new FormMember(name, this).Show();
                    }
                    else
                    {
                        MessageBox.Show("Unknown user type");
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid User");
                }
            }
        }
    }
}
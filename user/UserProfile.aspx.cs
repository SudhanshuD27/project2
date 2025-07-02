using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2.user
{
    public partial class UserProfile : System.Web.UI.Page
    {
        SqlConnection con;
        int userId = 1; // Hardcoded for now

        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            con = new SqlConnection(connStr);
            con.Open();

            if (!IsPostBack)
            {
                string q = $"EXEC sp_GetUserInfoById {userId}";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["FullName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                }
                reader.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Prepare parameters
            string nameParam = string.IsNullOrEmpty(name) ? "NULL" : $"'{name}'";
            string emailParam = string.IsNullOrEmpty(email) ? "NULL" : $"'{email}'";
            string passwordParam = string.IsNullOrEmpty(password) ? "NULL" : $"'{password}'";

            string q = $"EXEC sp_UpdateUserProfileFlexible {userId}, {nameParam}, {emailParam}, {passwordParam}";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();

            lblMessage.Text = "Profile updated successfully.";
        }
    }
}
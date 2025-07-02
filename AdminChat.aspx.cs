using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class AdminChat : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        int adminId = 2; // fixed admin
        int selectedUserId; // selected user to chat with

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get user id from query string
                if (Request.QueryString["userid"] != null)
                {
                    selectedUserId = Convert.ToInt32(Request.QueryString["userid"]);
                    ViewState["SelectedUserId"] = selectedUserId;

                    LoadUserInfo();
                    LoadChat();
                }
            }
            else
            {
                // get back from viewstate
                selectedUserId = Convert.ToInt32(ViewState["SelectedUserId"]);
            }
        }

        // Show user name on top
        private void LoadUserInfo()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = $"SELECT FullName FROM [User] WHERE UserId = {selectedUserId}";
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    lblUserName.Text = "Chatting with: " + result.ToString();
                }
            }
        }

        // Show chat messages
        private void LoadChat()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = $"EXEC sp_GetChatMessages {adminId}, {selectedUserId}";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptChat.DataSource = dt;
                rptChat.DataBind();
            }
        }

        // Send message
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();

            if (message != "")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = $"EXEC sp_SendChatMessage {adminId}, {selectedUserId}, '{message}', 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }

                txtMessage.Text = "";
                LoadChat(); // refresh
            }
        }
    }
}
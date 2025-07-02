using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2.user
{
    public partial class Chat : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        int userId = 1; // User logged in
        int adminId = 2; // Fixed admin ID

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChat();
            }
        }

        private void LoadChat()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = $@"
                    SELECT * FROM Chat
                    WHERE (SenderId = {userId} AND ReceiverId = {adminId})
                       OR (SenderId = {adminId} AND ReceiverId = {userId})
                    ORDER BY SentAt";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptChat.DataSource = dt;
                rptChat.DataBind();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (message != "")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = $"EXEC sp_SendChatMessage {userId}, {adminId}, '{message.Replace("'", "''")}', 0";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }

                txtMessage.Text = "";
                LoadChat();
            }
        }
    }
}
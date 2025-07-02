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
    public partial class Subscription : System.Web.UI.Page
    {
        SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            con = new SqlConnection(connStr);
            con.Open();

            if (!IsPostBack)
            {
                string q = "EXEC sp_GetAllSubscriptionPlans";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int planId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);
            int userId = 1; // hardcoded for now (replace later with Session["UserId"])

            if (e.CommandName == "ViewCourses")
            {
                string q = "EXEC sp_GetSubCoursesByPlanId " + planId;
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                Panel1.Visible = true;
            }

            if (e.CommandName == "Subscribe")
            {
                // Check if this user already has an active subscription for this plan
                string checkQuery = $"EXEC sp_CheckActiveSubscription {userId}, {planId}";
                SqlCommand cmd = new SqlCommand(checkQuery, con);
                object result = cmd.ExecuteScalar();

                int count = 0;
                if (result != null && result != DBNull.Value)
                {
                    count = Convert.ToInt32(result);
                }

                if (count > 0)
                {
                    lblMessage.Text = "You already have an active subscription for this plan.";
                    Panel1.Visible = false;
                }
                else
                {
                    Response.Redirect("Payment.aspx?type=subscription&planid=" + planId + "&userid=" + userId);
                }
            }
        }
    }
}

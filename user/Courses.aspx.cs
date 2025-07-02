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
    public partial class Courses : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                SqlCommand cmd = new SqlCommand("sp_GetAllSubCourses", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptCourses.DataSource = dt;
                rptCourses.DataBind();
            }
        }

        protected void btnAddToCart_Command(object sender, CommandEventArgs e)
        {
            int userId = 1; // login
            int subCourseId = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                SqlCommand cmd = new SqlCommand("sp_AddToCart", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@SubCourseId", subCourseId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("Cart.aspx");
        }
    }
}

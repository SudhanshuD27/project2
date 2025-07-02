using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;


namespace Project2.user
{
    public partial class Payment : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        int userId = 1;
        int planId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!int.TryParse(Request.QueryString["planid"], out planId))
                {
                    Response.Write("Invalid plan ID");
                    return;
                }

                LoadSubscriptionPlan();
            }
        }

        private void LoadSubscriptionPlan()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = "EXEC sp_GetSubscriptionPlanById " + planId;
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptSubscription.DataSource = dt;
                rptSubscription.DataBind();

                if (dt.Rows.Count > 0)
                {
                    ViewState["Amount"] = dt.Rows[0]["Price"];
                    ViewState["Duration"] = dt.Rows[0]["DurationInDays"];
                    lblAmount.Text = "₹" + dt.Rows[0]["Price"].ToString();
                }
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            decimal amount = Convert.ToDecimal(ViewState["Amount"]);
            int duration = Convert.ToInt32(ViewState["Duration"]);

            string keyId = "rzp_test_Kl7588Yie2yJTV";
            string keySecret = "6dN9Nqs7M6HPFMlL45AhaTgp";

            RazorpayClient client = new RazorpayClient(keyId, keySecret);

            string receiptId = "rcpt_" + Guid.NewGuid().ToString("N").Substring(0, 20);

            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", amount * 100 },
                { "currency", "INR" },
                { "receipt", receiptId },
                { "payment_capture", 1 }
            };

            Order order = client.Order.Create(options);
            string orderId = order["id"].ToString();

            var user = GetUserDetails();

            string script = $@"
                var options = {{
                    'key': '{keyId}',
                    'amount': {amount * 100},
                    'currency': 'INR',
                    'name': 'E-Learning Subscription',
                    'description': 'Plan Payment',
                    'order_id': '{orderId}',
                    'handler': function (response) {{
                        window.location.href = 'Invoice.aspx?payment_id=' + response.razorpay_payment_id + '&planid={planId}&userid={userId}';
                    }},
                    'prefill': {{
                        'name': '{user.name}',
                        'email': '{user.email}'
                    }},
                    'theme': {{
                        'color': '#3399cc'
                    }}
                }};
                var rzp1 = new Razorpay(options);
                rzp1.open();";

            ClientScript.RegisterStartupScript(this.GetType(), "rzpScript", script, true);
        }

        private (string name, string email) GetUserDetails()
        {
            string name = "", email = "";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("EXEC sp_GetUserInfoById " + userId, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    name = reader["FullName"].ToString();
                    email = reader["Email"].ToString();
                }
            }
            return (name, email);
        }
    }
}
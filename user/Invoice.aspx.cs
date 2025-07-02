using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Project2.user
{
    public partial class Invoice : System.Web.UI.Page
    {
        SqlConnection con;
        int userId = 1; // hardcoded

        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            con = new SqlConnection(connStr);
            con.Open();

            if (!IsPostBack)
            {
                if (Request.QueryString["payment_id"] != null && Request.QueryString["planid"] != null)
                {
                    string paymentId = Request.QueryString["payment_id"];
                    int planId = Convert.ToInt32(Request.QueryString["planid"]);

                    lblPaymentId.Text = paymentId;

                    DateTime purchaseDate = DateTime.Now;
                    DateTime expiryDate = purchaseDate.AddMonths(6);

                    // Step 1: Create order
                    string createOrder = $@"
                        DECLARE @PurchaseId INT;
                        EXEC sp_CreateOrder {userId}, {planId}, '{purchaseDate}', '{expiryDate}', @PurchaseId OUTPUT;
                        SELECT @PurchaseId;";
                    SqlCommand cmd1 = new SqlCommand(createOrder, con);
                    int purchaseId = Convert.ToInt32(cmd1.ExecuteScalar());

                    // Step 2: Grant Access
                    string grantAccess = $"EXEC sp_GrantCourseAccess {userId}, 'Subscription', {purchaseId}, '{purchaseDate}', '{expiryDate}'";
                    SqlCommand cmd2 = new SqlCommand(grantAccess, con);
                    cmd2.ExecuteNonQuery();

                    // Step 3: Plan Name
                    SqlCommand cmd3 = new SqlCommand("EXEC sp_GetPlanNameById @PlanId", con);
                    cmd3.Parameters.AddWithValue("@PlanId", planId);
                    object result = cmd3.ExecuteScalar();
                    lblPlanName.Text = result != null ? result.ToString() : "";

                    lblPurchaseDate.Text = purchaseDate.ToString("dd-MMM-yyyy");
                    lblExpiryDate.Text = expiryDate.ToString("dd-MMM-yyyy");

                    // Step 4: Courses
                    string q = $"EXEC sp_GetSubCoursesByPlanId {planId}";
                    SqlDataAdapter da = new SqlDataAdapter(q, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    ViewState["Courses"] = dt;
                    ViewState["PlanName"] = lblPlanName.Text;
                    ViewState["PaymentId"] = paymentId;
                    ViewState["PurchaseDate"] = purchaseDate.ToString("dd-MMM-yyyy");
                    ViewState["ExpiryDate"] = expiryDate.ToString("dd-MMM-yyyy");
                }
            }
        }

        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["Courses"] as DataTable;
            string planName = ViewState["PlanName"].ToString();
            string paymentId = ViewState["PaymentId"].ToString();
            string purchaseDate = ViewState["PurchaseDate"].ToString();
            string expiryDate = ViewState["ExpiryDate"].ToString();

            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            doc.Add(new Paragraph("Invoice", FontFactory.GetFont("Arial", 18, Font.BOLD)));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Payment ID: " + paymentId));
            doc.Add(new Paragraph("Plan: " + planName));
            doc.Add(new Paragraph("Purchase Date: " + purchaseDate));
            doc.Add(new Paragraph("Expiry Date: " + expiryDate));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Courses Included:"));
            doc.Add(new Paragraph(" "));

            PdfPTable table = new PdfPTable(1);
            table.AddCell("Course Title");

            foreach (DataRow row in dt.Rows)
            {
                table.AddCell(row["Title"].ToString());
            }

            doc.Add(table);
            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice.pdf");
            Response.OutputStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
            Response.Flush();
            Response.End();
        }
    }
}
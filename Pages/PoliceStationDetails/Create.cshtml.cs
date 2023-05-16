using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Police_Management_System.Pages.GuestDetails.IndexModel;
using static Police_Management_System.Pages.PoliceStationDetails.IndexModel;

namespace Police_Management_System.Pages.PoliceStationDetails
{
    public class CreateModel : PageModel
    {
        public PoliceInfo listpolice = new PoliceInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            listpolice.PoliceStationId = Convert.ToInt32(Request.Form["PoliceStationId"]);
            listpolice.PoliceStationName = Request.Form["PoliceStationName"];
            listpolice.Address = Request.Form["Address"];
            if (listpolice.PoliceStationId == 0 || listpolice.PoliceStationName.Length == 0 || listpolice.Address.Length == 0)
            {
                errorMessage = "all the fields are required";
                return;
            }

            try
            {
                string conncectionString = "Data Source=SP13889\\SQLEXPRESS;Initial Catalog=\"Police Management System\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conncectionString))
                {
                    connection.Open();
                    string sql = "insert into PoliceStationDetails" + "(PoliceStationId, PoliceStationName ,Address) Values (@PoliceStationId,@PoliceStationName,@Address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PoliceStationId", listpolice.PoliceStationId);
                        command.Parameters.AddWithValue("@PoliceStationName", listpolice.PoliceStationName);
                        command.Parameters.AddWithValue("@Address", listpolice.Address);
                        





                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            listpolice.PoliceStationId = 0; listpolice.PoliceStationName = ""; listpolice.Address = "";
            successMessage = "New Details added successfully";


            Response.Redirect("/PoliceStationDetails/Index");
        }

    }
}

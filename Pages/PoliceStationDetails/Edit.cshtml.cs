using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Police_Management_System.Pages.GuestDetails.IndexModel;
using System.Data.SqlClient;
using static Police_Management_System.Pages.PoliceStationDetails.IndexModel;

namespace Police_Management_System.Pages.PoliceStationDetails
{
    public class EditModel : PageModel
    {
        public PoliceInfo listpolice = new PoliceInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string PoliceStationId = Request.Query["id"];

            try
            {
                string conncectionString = "Data Source=SP13889\\SQLEXPRESS;Initial Catalog=\"Police Management System\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conncectionString))
                {
                    connection.Open();
                    string sql = "select * from PoliceStationDetails where PoliceStationId =@PoliceStationId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PoliceStationId", PoliceStationId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               
                                listpolice.PoliceStationId = reader.GetInt32(0);
                                listpolice.PoliceStationName = reader.GetString(1);
                                listpolice.Address = reader.GetString(2);
                               

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            listpolice.PoliceStationId = Convert.ToInt32(Request.Form["PoliceStationId"]);
            listpolice.PoliceStationName = Request.Form["PoliceStationName"];
            listpolice.Address = Request.Form["Address"];
           

            if (listpolice.PoliceStationName.Length == 0 || listpolice.Address.Length == 0)
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
                    string sql = "update PoliceStationDetails SET PoliceStationName=@PoliceStationName , Address=@Address where PoliceStationId =@PoliceStationId";
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
            successMessage = "New detail added successfully";

            Response.Redirect("/PoliceStationDetails");
        }
    }
}

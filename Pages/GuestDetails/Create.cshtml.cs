using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Police_Management_System.Pages.GuestDetails.IndexModel;

namespace Police_Management_System.Pages.GuestDetails
{
    public class CreateModel : PageModel
    {
       public GuestInfo guestInfo = new GuestInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            guestInfo.GuestId = (Request.Form["GuestId"]);
            guestInfo.FirstName = Request.Form["FirstName"];
            guestInfo.LastName = Request.Form["LastNAme"];
            guestInfo.Address = Request.Form["Address"];
            guestInfo.City = Request.Form["City"];
            guestInfo.State = Request.Form["State"];
            guestInfo.Country = Request.Form["Country"];
            guestInfo.Pincode = Request.Form["Pincode"];

            //if (guestInfo.GuestId.Length == 0 || guestInfo.FirstName.Length == 0 || guestInfo.LastName.Length == 0 || guestInfo.Address.Length == 0 || guestInfo.City.Length == 0 || guestInfo.State.Length == 0 || guestInfo.Country.Length == 0 || guestInfo.Pincode.Length ==0)
            //{
            //    errorMessage = "all the fields are required";
            //    return;
            //}

            try
            {
                string conncectionString = "Data Source=SP13889\\SQLEXPRESS;Initial Catalog=\"Police Management System\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conncectionString))
                {
                    connection.Open();
                    string sql = "insert into GuestDetails" + "(GuestId,FirstName , LastName,Address,City,State,Country,Pincode) Values " + "(@GuestId,@FirstName,@Lastname,@Address,@City,@State,@Country,@Pincode);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@GuestId", guestInfo.GuestId);
                        command.Parameters.AddWithValue("@FirstName", guestInfo.FirstName);
                        command.Parameters.AddWithValue("@LastName", guestInfo.LastName);
                        command.Parameters.AddWithValue("@Address", guestInfo.Address);
                        command.Parameters.AddWithValue("@City", guestInfo.City);
                        command.Parameters.AddWithValue("@State", guestInfo.State);
                        command.Parameters.AddWithValue("@Country", guestInfo.Country);
                        command.Parameters.AddWithValue("@Pincode", guestInfo.Pincode);






                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            guestInfo.GuestId = ""; guestInfo.FirstName = ""; guestInfo.LastName = ""; guestInfo.Address = ""; guestInfo.City = ""; guestInfo.State = ""; guestInfo.Country = ""; guestInfo.Pincode = "";
            successMessage = "New guest added successfully";

            Response.Redirect("/GuestDetails/Index");

        }

    }
}

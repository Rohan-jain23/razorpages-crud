using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Police_Management_System.Pages.GuestDetails
{
    public class IndexModel : PageModel
    {
        public List<GuestInfo> listguest = new List<GuestInfo>();
        public void OnGet()
        {
            try
            {
                string conncectionString = "Data Source=SP13889\\SQLEXPRESS;Initial Catalog=\"Police Management System\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conncectionString))
                {
                    connection.Open();
                    string sql = "select * from GuestDetails";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                GuestInfo guest = new GuestInfo();
                                guest.GuestId = reader.GetString(0);
                                guest.FirstName = reader.GetString(1);
                                guest.LastName = reader.GetString(2);
                                guest.Address = reader.GetString(3);
                                guest.City = reader.GetString(4);
                                guest.State = reader.GetString(5);
                                guest.Country = reader.GetString(6);
                                guest.Pincode = reader.GetString(7);


                                listguest.Add(guest);



                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
        public class GuestInfo
        {
            public string GuestId;
            public string FirstName;
            public string LastName;
            public string Address;
            public string City;
            public string State;
            public string Country;
            public string Pincode;


        }
    }
}
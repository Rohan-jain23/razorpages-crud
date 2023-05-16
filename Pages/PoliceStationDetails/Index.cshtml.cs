using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Police_Management_System.Pages.PoliceStationDetails
{
    public class IndexModel : PageModel
    {
        public List<PoliceInfo> listpolice = new List<PoliceInfo>();
        public void OnGet()
        {
 
                try
                {
                    string conncectionString = "Data Source=SP13889\\SQLEXPRESS;Initial Catalog=\"Police Management System\";Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(conncectionString))
                    {
                        connection.Open();
                        string sql = "select * from PoliceStationDetails";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    PoliceInfo police = new PoliceInfo();
                                    police.PoliceStationId = reader.GetInt32(0);
                                    police.PoliceStationName = reader.GetString(1);
                                    police.Address = reader.GetString(2);
                                   
                                    


                                    listpolice.Add(police);



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
            public class PoliceInfo
            {
                public int? PoliceStationId;
                public string PoliceStationName;
                public string Address;
                
            }
        }
    }

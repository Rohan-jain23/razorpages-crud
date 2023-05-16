using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Police_Management_System.Pages.GuestDetails.IndexModel;
using System.Data.SqlClient;
using static Police_Management_System.Pages.PoliceStationDetails.IndexModel;

namespace Police_Management_System.Pages.ProblemDetails
{
    public class IndexModel : PageModel
    {
        public List<PoliceStation> PoliceStationInfo = new List<PoliceStation>();
        public List<ProblemInfo> ProblemStore = new List<ProblemInfo>();
        string policeStationName = "";
        

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
                                PoliceStation station = new PoliceStation();
                                station.PoliceStationId = reader.GetInt32(0);
                                station.PoliceStationName = reader.GetString(1);
                                station.Address = reader.GetString(2);
                                PoliceStationInfo.Add(station);
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
        public void OnPost()
        {
            int id =Convert.ToInt32( Request.Form["policeid"]);
            policeStationName = Request.Form["stationNmae"];
            string date = Convert.ToString(Request.Form["BookingFrom"]);
            DateTime pdate; 
            pdate = Convert.ToDateTime(date);

            string newDate = pdate.ToString("d");
            string conncectionString = "Data Source=SP13889\\SQLEXPRESS;Initial Catalog=\"Police Management System\";Integrated Security=True";

            string query = "select * from ProblemDetails where PoliceStationId= "+id+" and Date='"+newDate+"'";
            using (SqlConnection connection = new SqlConnection(conncectionString))
            {
                connection.Open();
                string view = "select * from PoliceStationDetails where PoliceStationId="+ id + "";
                using (SqlCommand cmd = new SqlCommand(view, connection))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        if(read.Read())
                        {
                          policeStationName= read.GetString(1);
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            ProblemInfo problem = new ProblemInfo();
                            problem.ProblemId = read.GetInt32(0);
                            problem.Description = read.GetString(3);
                            problem.isResolved= read.GetBoolean(4);

                            ProblemStore.Add(problem);
                        }
                    }
                }
            }


        }

    }
        public class PoliceStation
        { 
        public int PoliceStationId { get; set; }
        public string PoliceStationName { get; set; }
        public string Address { get; set; }
          

        }

    public class ProblemInfo { 
        public int ProblemId { get; set; }
        public string Description { get; set;}
        public bool isResolved { get; set; }

        public DateTime Date { get; set; }
        
    }


 }

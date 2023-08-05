using MedicalNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MedicalNews.Controllers
{
    [Route("/api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {

            var list1 = new List<DataModel>();
            var list2 = new List<DataModel>();
            SqlConnection connection = new SqlConnection("Server=MSI\\SQLEXPRESS; Database=learnDB; Trusted_Connection=True; TrustServerCertificate=True;");

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "usp_two_tables";
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();

            var sdr = cmd.ExecuteReader();

            try
            {
                while (sdr.Read())
                {
                    list1.Add(new DataModel()
                    {
                        Id = Convert.ToInt32(sdr["id"]),
                        Data = sdr["data_string"].ToString()
                    });
                }

                if (sdr.NextResult())
                {

                    while (sdr.Read())
                    {
                        list2.Add(new DataModel()
                        {
                            Id = Convert.ToInt32(sdr["id"]),
                            Data = sdr["data_string"].ToString()
                        });
                    }
                }

                return Ok(new { list1, list2 });
            }
            catch (Exception)
            {
                return BadRequest("Cannot create list");
                throw;
            }
        }
    }
}

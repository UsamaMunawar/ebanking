using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace ebanking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CurrencyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM ebankingdb.currency_rates";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EBankingDBCon");
            MySqlDataReader myReader;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new(query, mySqlConnection))
                {
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mySqlConnection.Close();

                }

            }
            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            string query = @"SELECT * FROM ebankingdb.currency_rates where short_name=@CurrencyID";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EBankingDBCon");
            MySqlDataReader myReader;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new(query, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("CurrencyID", id);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mySqlConnection.Close();

                }

            }
            return new JsonResult(table);
        }
    }
}

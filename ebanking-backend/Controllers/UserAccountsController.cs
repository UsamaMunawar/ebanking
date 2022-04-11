using ebanking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace ebanking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserAccountsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM ebankingdb.user_accounts";
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

        [HttpGet("{acNumber}")]
        public JsonResult Get(string acNumber)
        {
            string query = @"SELECT * FROM ebankingdb.user_accounts where account_number=@AccountNumber";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EBankingDBCon");
            MySqlDataReader myReader;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new(query, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("AccountNumber", acNumber);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mySqlConnection.Close();

                }

            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(UserAccounts userAccounts)
        {
            string query = @"INSERT INTO `ebankingdb`.`user_accounts` (`first_name`, `last_name`, `username`, `password`, `accont_number`, `pin_code`, `account_type`, `balance`) VALUES (@FirstName, @LastName, @Username, @Password, @AccountNumber, @PinCode, @AccountType, @Balance);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EBankingDBCon");
            MySqlDataReader myReader;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new(query, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("@FirstName", userAccounts.FirstName);
                    mySqlCommand.Parameters.AddWithValue("@LastName", userAccounts.LastName);
                    mySqlCommand.Parameters.AddWithValue("@Username", userAccounts.Username);
                    mySqlCommand.Parameters.AddWithValue("@Password", userAccounts.Password);
                    mySqlCommand.Parameters.AddWithValue("@AccountNumber", userAccounts.AccountNumber);
                    mySqlCommand.Parameters.AddWithValue("@PinCode", userAccounts.PinCode);
                    mySqlCommand.Parameters.AddWithValue("@AccountType", userAccounts.AccountType);
                    mySqlCommand.Parameters.AddWithValue("@Balance", userAccounts.Balance);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mySqlConnection.Close();

                }

            }
            return new JsonResult("User Account Created Successfully");
        }
    }
}

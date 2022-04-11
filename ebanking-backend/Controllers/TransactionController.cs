using ebanking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace ebanking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"SELECT * FROM ebankingdb.transactions";
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
        public JsonResult Get(int id)
        {
            string query = @"SELECT * FROM ebankingdb.transactions where id=@TransactionId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EBankingDBCon");
            MySqlDataReader myReader;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new(query, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("TransactionId", id);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mySqlConnection.Close();

                }

            }
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("NewTransaction")]
        public JsonResult Post([FromBody] Transaction transaction)
        {
            string sqlDataSource = _configuration.GetConnectionString("EBankingDBCon");
            MySqlDataReader myReader;

            string query1 = @"SELECT * FROM ebankingdb.user_accounts where id=@AccountFromID";
            string query2 = @"SELECT * FROM ebankingdb.user_accounts where id=@AccountToID";
            DataTable user_from = new DataTable();
            DataTable user_to = new DataTable();

            //string query = @"SELECT * FROM ebankingdb.transactions where id=@TransactionId";
            //DataTable table = new DataTable();

            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlDataSource))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new(query1, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("AccountFromID", transaction.AccountFrom);
                    myReader = mySqlCommand.ExecuteReader();
                    user_from.Load(myReader);
                    myReader.Close();

                }
                using (MySqlCommand mySqlCommand = new(query2, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("AccountToID", transaction.AccountTo);
                    myReader = mySqlCommand.ExecuteReader();
                    user_to.Load(myReader);
                    myReader.Close();
                }
                if(user_from.Rows.Count > 0 && user_to.Rows.Count > 0)
                {
                    double src_balance =Convert.ToDouble(user_from.Rows[0].ItemArray[8]);
                    double dest_balance =Convert.ToDouble(user_to.Rows[0].ItemArray[8]);
                    if (src_balance < transaction.TransactionAmmount)
                    {
                        mySqlConnection.Close();
                        return new JsonResult("Insufficient Balance");
                    } else
                    {
                        string update_user_from = @"UPDATE `ebankingdb`.`user_accounts` SET `balance` = @FromNewBalance WHERE id=@FromID;";
                        string update_user_to = @"UPDATE `ebankingdb`.`user_accounts` SET `balance` = @ToNewBalance WHERE id=@ToID;";
                        string query3 = @"INSERT INTO `ebankingdb`.`transactions` (`account_from`, `account_to`, `transaction_date`, `transaction_ammount`, `transaction_currency`)
                                        VALUES (@AccountFrom, @AccountTo, @TransactionDate, @TransactionAmmount, @TransactionCurrency);";

                        using (MySqlCommand mySqlCommand = new(query3, mySqlConnection))
                        {
                            mySqlCommand.Parameters.AddWithValue("@AccountFrom", transaction.AccountFrom);
                            mySqlCommand.Parameters.AddWithValue("@AccountTo", transaction.AccountTo);
                            mySqlCommand.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                            mySqlCommand.Parameters.AddWithValue("@TransactionAmmount", transaction.TransactionAmmount);
                            mySqlCommand.Parameters.AddWithValue("@TransactionCurrency", transaction.TransactionCurrency);
                            myReader = mySqlCommand.ExecuteReader();
                            myReader.Close();

                        }

                        using (MySqlCommand mySqlCommand = new(update_user_from, mySqlConnection))
                        {
                            mySqlCommand.Parameters.AddWithValue("FromNewBalance", src_balance - transaction.TransactionAmmount);
                            mySqlCommand.Parameters.AddWithValue("FromID", transaction.AccountFrom);
                            myReader = mySqlCommand.ExecuteReader();
                            myReader.Close();
                        }

                        using (MySqlCommand mySqlCommand = new(update_user_to, mySqlConnection))
                        {
                            mySqlCommand.Parameters.AddWithValue("ToNewBalance", dest_balance + transaction.TransactionAmmount);
                            mySqlCommand.Parameters.AddWithValue("ToID", transaction.AccountTo);
                            myReader = mySqlCommand.ExecuteReader();
                            myReader.Close();
                        }


                        mySqlConnection.Close();
                        return new JsonResult("Transaction Successfull");
                    }
                }
                else { 
                    mySqlConnection.Close(); 
                    return new JsonResult("Account Not Found");
                }

            }
            return new JsonResult(user_to);
        }
    }
}

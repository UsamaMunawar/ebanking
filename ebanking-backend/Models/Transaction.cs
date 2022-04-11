namespace ebanking_backend.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmmount { get; set; }
        public int TransactionCurrency { get; set; }
    }
}

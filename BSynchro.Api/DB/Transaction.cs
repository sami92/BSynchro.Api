namespace BSynchro.Api.DB
{
    public class Transaction
    {
        public int Id { get; set; }
        public int  AccountId { get; set; }
        public float Amount { get; set; }
        public string TransactionType { get; set; }
        public  DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}

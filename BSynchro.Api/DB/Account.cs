namespace BSynchro.Api.DB
{
    public class Account
    {
        public int Id { get; set; }
        public string No { get; set; }
        public float Balance { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public ICollection<Transaction>? Transactions { get; set; }

    }
}

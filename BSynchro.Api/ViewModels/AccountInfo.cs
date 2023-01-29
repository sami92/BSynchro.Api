using BSynchro.Api.Models;

namespace BSynchro.Api.ViewModels
{
    public class AccountInfo
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public float Balance { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
    }
}

namespace BSynchro.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SureName { get; set; }
        public string Mobile { get; set; }
        public DateTime  CreatedTime { get; set; } = DateTime.Now;


    }
}

namespace BSynchro.Api.Profiles
{
    public class TransactionProfile : AutoMapper.Profile
    {
        public TransactionProfile()
        {
            CreateMap<DB.Transaction, Models.Transaction>();
        }
    }
}

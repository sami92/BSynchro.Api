namespace BSynchro.Api.Profiles
{
    public class AccountProfile:AutoMapper.Profile
    {
        public AccountProfile()
        {
            CreateMap<DB.Account, Models.Account>();
            CreateMap<DB.Account, ViewModels.AccountInfo>();
        }
    }
}


using BSynchro.Api.Models;
namespace BSynchro.Api.Interfaces
{
    

    public interface IAccountService
    {
        Task<(bool IsSuccess, IEnumerable<Account> Accounts, string ErrorMessage)> GetAccounts();
        Task<(bool IsSuccess, IEnumerable<ViewModels.AccountInfo> Accounts, string ErrorMessage)> GetCustomerAccounts(int id);
        Task<(bool IsSuccess, Account Account, string ErrorMessage)> OpenCustomerAccount(ViewModels.AccountCreateModel account);
  
    }


}

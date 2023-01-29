using AutoMapper;
using BSynchro.Api.DB;
using BSynchro.Api.Interfaces;
using BSynchro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BSynchro.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        public AccountService(DataContext dbContext, ILogger<AccountService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        public async Task<(bool IsSuccess, Models.Account Account, string ErrorMessage)> OpenCustomerAccount(ViewModels.AccountCreateModel account)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == account.CustomerId);
                if (customer == null)
                {
                    return (false, null, "Customer Not Found");
                }
                var newAccount = new DB.Account
                {
                    Id = new Random().Next(0, 1000000),
                    Balance = account.initCredit,
                    CreatedTime = DateTime.Now,
                    CustomerId = customer.Id,
                    No = new Random().Next(0, 1000000).ToString()
                };
                _dbContext.Accounts.Add(newAccount);
                if (account.initCredit > 0)
                {
                    var newTransaction = new DB.Transaction
                    {
                        Id = new Random().Next(0, 1000000),
                        AccountId = newAccount.Id,
                        CreatedTime = DateTime.Now,
                        Amount = account.initCredit,
                        TransactionType = "Credit"
                    };
                    _dbContext.Transactions.Add(newTransaction);
                }
                _dbContext.SaveChanges();
                var result = _mapper.Map<DB.Account, Models.Account>(newAccount);
                return (true, result, "");

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, IEnumerable<Models.Account> Accounts, string ErrorMessage)> IAccountService.GetAccounts()
        {
            try
            {
                var accounts = await _dbContext.Accounts.ToListAsync();
                if (accounts != null && accounts.Any())
                {
                    var result = _mapper.Map<IEnumerable<DB.Account>, IEnumerable<Models.Account>>(accounts);
                    return (true, result, null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, IEnumerable<ViewModels.AccountInfo> Accounts, string ErrorMessage)> IAccountService.GetCustomerAccounts(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer == null)
                {
                    return (false, null, "Customer Not Found");
                }
                var customerAccounts = await _dbContext.Accounts.Where(c => c.CustomerId == id)
                                                    .Include(t => t.Transactions).ToListAsync();
                if (customerAccounts == null)
                {
                    return (false, null, "Customer Has No Accounts");
                }
                var result = _mapper.Map<IEnumerable<ViewModels.AccountInfo>>(customerAccounts);
                foreach (var obj in result)
                {
                    obj.CustomerName = customer.FirstName;
                    obj.CustomerSurname = customer.SureName;                    
                }
                return (true, result, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add(new DB.Customer { Id = 1, FirstName = "Sami", SureName = "Daoud", Mobile = "+96399000000" });
                _dbContext.SaveChanges();
            }
        }
    }

}

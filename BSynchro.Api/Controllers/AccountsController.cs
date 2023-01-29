using Microsoft.AspNetCore.Mvc;
using BSynchro.Api.Interfaces;
using BSynchro.Api.Models;
using BSynchro.Api.ViewModels;

namespace BSynchro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        
        private readonly IAccountService _accountService;
        
        public AccountsController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerAccounts(int customerId)
        {
            var result = await _accountService.GetCustomerAccounts(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Accounts);
            }
            else                 
                return BadRequest(result.ErrorMessage);
        }
        [HttpPost]
        public async Task<IActionResult> OpenAccount([FromBody] AccountCreateModel account)
        {
            var result = await _accountService.OpenCustomerAccount(account);
            if (result.IsSuccess)
            {
                return Ok(result.Account);
            }
            else
                return BadRequest(result.ErrorMessage);
        }
    }
}

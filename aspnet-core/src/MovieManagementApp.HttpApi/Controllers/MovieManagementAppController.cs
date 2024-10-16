using MovieManagementApp.MyAccounts;
using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;

namespace MovieManagementApp.Controllers
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(AccountController))]
    public class MyAccountController : AccountController
    {
        private readonly IMyAccountRepository _myAccountRepository;
        public MyAccountController(
            IAccountAppService accountAppService,
            IMyAccountRepository myAccountRepository)
            : base(accountAppService)
        {
            _myAccountRepository = myAccountRepository;
        }

    }
}



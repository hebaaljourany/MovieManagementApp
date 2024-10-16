using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MovieManagementApp.Application.Contracts.MyAccounts;
using System;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace MovieManagementApp.MyAccounts
{
    public class MyAccountAppService :
            AccountAppService,
            IAccountAppService,
            ITransientDependency
    {
        private readonly IMyAccountRepository _myAccountRepository;

        public MyAccountAppService(
             IMyAccountRepository myAccountRepository,
            IIdentityRoleRepository roleRepository,
            IdentityUserManager userManager,
            IAccountEmailer accountEmailer,
            IdentitySecurityLogManager identitySecurityLogManager,
            IOptions<IdentityOptions> identityOptions)
            : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {
            _myAccountRepository = myAccountRepository;

        }
        public async override Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            var userDto = await base.RegisterAsync(input);
            var user = await UserManager.FindByIdAsync(userDto.Id.ToString());
            await base.UserManager.AddToRoleAsync(user, "User");
            // Your custom logic to add UserId to MyAccount entity
            var myAccount = new MyAccount
            {
                UserId = userDto.Id,
                // Other properties initialization
            };
            await _myAccountRepository.InsertAsync(myAccount);

            return userDto;
        }

    }
}

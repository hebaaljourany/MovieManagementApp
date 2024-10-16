using MovieManagementApp.MyAccounts;
using MovieManagementApp.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class MyAccountRepository : EfCoreRepository<MovieManagementAppDbContext, MyAccount, Guid>, IMyAccountRepository
{
    public MyAccountRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.MyAccounts
{
    public interface IMyAccountRepository : IRepository<MyAccount, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}

using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.MyLists
{
    public interface IMyListRepository : IRepository<MyList, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}

using MovieManagementApp.Application.Contracts.MyLists;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.MyLists
{
    public class MyListAppService :
        CrudAppService<
            MyList, // The MyList entity
            MyListDto, // Used to show my lists
            Guid, // Primary key of the my list entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateMyListDto>, // Used to create/update a my list
        IMyListAppService // Implement the IMyListAppService
    {
        public MyListAppService(IRepository<MyList, Guid> repository)
            : base(repository)
        {
        }
    }
}

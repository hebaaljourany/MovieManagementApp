using MovieManagementApp.MyLists;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MovieManagementApp.Application.Contracts.MyLists
{
    public interface IMyListAppService :
        ICrudAppService< // Defines CRUD methods
            MyListDto, // Used to show my lists
            Guid, // Primary key of the my list entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateMyListDto> // Used to create/update a my list
    {
    }
}

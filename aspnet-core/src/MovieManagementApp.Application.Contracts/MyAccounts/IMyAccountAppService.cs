using MovieManagementApp.MyAccounts;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MovieManagementApp.Application.Contracts.MyAccounts
{
    public interface IMyAccountAppService :
        ICrudAppService< // Defines CRUD methods
            MyAccountDto, // Used to show accounts
            Guid, // Primary key of the account entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateMyAccountDto> // Used to create/update an account
    {
    }
}

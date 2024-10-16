using System;
using Volo.Abp.Application.Dtos;


namespace MovieManagementApp.Movies
{
    public class CategoryLookupDto : EntityDto<Guid>
    {
        public string CategoryName { get; set; }
    }
}


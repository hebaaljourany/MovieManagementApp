using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.Categories
{
    public class CategoryDto : AuditedEntityDto<Guid>
    {
        public string CategoryName { get; set; }
        public List<Guid> MovieIds { get; set; }

    }
}

using System;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.MyLists
{
    public class MyListDto : AuditedEntityDto<Guid>
    {
        public Guid MyAccountId { get; set; }
        public Guid MovieId { get; set; }
    }
}

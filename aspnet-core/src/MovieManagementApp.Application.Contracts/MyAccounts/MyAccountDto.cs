using System;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.MyAccounts
{
    public class MyAccountDto : AuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }
    }
}

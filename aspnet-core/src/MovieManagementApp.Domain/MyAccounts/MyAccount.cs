using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MovieManagementApp.MyAccounts
{
    public class MyAccount : FullAuditedEntity<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
    }
}

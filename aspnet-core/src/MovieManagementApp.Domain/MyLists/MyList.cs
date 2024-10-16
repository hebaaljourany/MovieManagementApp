using MovieManagementApp.MyAccounts;
using MovieManagementApp.Movies;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MovieManagementApp.MyLists
{
    public class MyList : AuditedEntity<Guid>
    {
        public Guid MyAccountId { get; set; }
        public Guid MovieId { get; set; }

        // Navigation properties
        public virtual MyAccount MyAccount { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

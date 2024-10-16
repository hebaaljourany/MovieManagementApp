using MovieManagementApp.MyAccounts;
using MovieManagementApp.Movies;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MovieManagementApp.Ratings
{
    public class Rating : AuditedEntity<Guid>
    {
        public Guid MyAccountId { get; set; }
        public Guid MovieId { get; set; }
        

        [Range(0, 5)] // Assuming a rating scale from 1 to 5
        public int RatingValue { get; set; }

        // Navigation properties
        public virtual Movie Movie { get; set; }
        public virtual MyAccount MyAccount { get; set; }
    }
}

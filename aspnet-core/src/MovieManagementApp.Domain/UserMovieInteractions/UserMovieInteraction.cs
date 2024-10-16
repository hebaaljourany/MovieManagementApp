using MovieManagementApp.MyAccounts;
using MovieManagementApp.Movies;
using System;
using System.ComponentModel.DataAnnotations; // For data annotations
using Volo.Abp.Domain.Entities.Auditing; // For auditing features

namespace MovieManagementApp.UserMovieInteractions
{

    // UserMovieInteractions entity class inheriting from FullAuditedEntity
    public class UserMovieInteraction : FullAuditedEntity<Guid>
    {
        // Foreign key for the user
        public Guid MyAccountId { get; set; }

        // Foreign key for the movie
        public Guid MovieId { get; set; }

        // Type of interaction (watched or downloaded)
        [Required]
        public InteractionType Interaction { get; set; }

      
        // Navigation properties for relationships
        public virtual MyAccount MyAccount { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

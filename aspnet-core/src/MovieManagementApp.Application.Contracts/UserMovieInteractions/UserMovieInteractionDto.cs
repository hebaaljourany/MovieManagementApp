using System;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.UserMovieInteractions
{
    public class UserMovieInteractionDto : AuditedEntityDto<Guid>
    {
        public Guid MyAccountId { get; set; }
        public Guid MovieId { get; set; }
        public InteractionType Interaction { get; set; }

    }
 
}

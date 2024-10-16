using System;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.Ratings
{
    public class RatingDto : AuditedEntityDto<Guid>
    {
        public Guid MyAccountId { get; set; }
        public Guid MovieId { get; set; }
        public int RatingValue { get; set; }
    }
}

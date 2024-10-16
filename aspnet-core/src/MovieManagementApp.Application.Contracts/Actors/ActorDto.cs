using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.Actors
{
    public class ActorDto : AuditedEntityDto<Guid>
    {
        public string ActorName { get; set; }
        public List<Guid> MovieIds { get; set; }
    }
}

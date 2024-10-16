using System;
using Volo.Abp.Application.Dtos;


namespace MovieManagementApp.Movies
{
    public class ActorLookupDto : EntityDto<Guid>
    {
        public string ActorName { get; set; }
    }
}


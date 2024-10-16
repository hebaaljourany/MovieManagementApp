using System;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.MovieActors
{
    public class MovieActorDto : EntityDto<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
    }
}

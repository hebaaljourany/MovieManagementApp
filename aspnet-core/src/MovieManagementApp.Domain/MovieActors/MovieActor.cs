using MovieManagementApp.Actors;
using MovieManagementApp.Movies;
using System;
using Volo.Abp.Domain.Entities;

namespace MovieManagementApp.MovieActors
{
    public class MovieActor : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }

        // Navigation properties
        public virtual Movie Movie { get; set; }
        public virtual Actor Actor { get; set; }
    }
}

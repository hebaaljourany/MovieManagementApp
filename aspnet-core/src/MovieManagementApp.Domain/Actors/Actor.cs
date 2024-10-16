using MovieManagementApp.MovieActors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MovieManagementApp.Actors
{
    public class Actor : FullAuditedEntity<Guid>
    {
        [Required]
        public string ActorName { get; set; }
        //Many-to-Many with Movie Entity
        public List<MovieActor> MovieActors { get; set; }
    }
}

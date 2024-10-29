using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementApp.Application.Contracts.Actors
{
    public class CreateUpdateActorDto
    {
        [Required]
        [StringLength(128)]
        public string ActorName { get; set; }
        [Url]
        public string ActorImage { get; set; }
        public List<Guid> MovieIds { get; set; }


    }
}

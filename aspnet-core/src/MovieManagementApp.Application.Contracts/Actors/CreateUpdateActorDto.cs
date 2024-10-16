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
        public List<Guid> MovieIds { get; set; }

        // [DataType(DataType.Date)]
        // public DateTime? BirthDate { get; set; }

        //[StringLength(512)]
        //public string Biography { get; set; }
    }
}

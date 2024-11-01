using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace MovieManagementApp.Application.Contracts.Actors
{
    public class CreateUpdateActorDto
    {
        [Required]
        [StringLength(128)]
        public string ActorName { get; set; }
        public IRemoteStreamContent ActorImageBlob { get; set; }
        public string Thumbnail { get; set; }

        public List<Guid> MovieIds { get; set; }


    }
}

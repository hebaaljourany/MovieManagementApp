using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementApp.Application.Contracts.Movies
{
    public class CreateUpdateMovieDto
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        [Required]
        [Range(1, 300)]

        public int Duration { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [StringLength(10)]
        public string AgeRating { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Url]
        public string PosterUrl { get; set; }
        [Required]
        [Url]
        public string VideoUrl { get; set; }

        public List<Guid> ActorIds { get; set; }
        public List<Guid> CategoryIds { get; set; }

    }
}

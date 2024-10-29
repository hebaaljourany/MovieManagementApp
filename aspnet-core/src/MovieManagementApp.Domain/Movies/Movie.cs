using MovieManagementApp.MovieActors;
using MovieManagementApp.MovieCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For data annotations
using Volo.Abp.Domain.Entities.Auditing; // For auditing features

namespace MovieManagementApp.Movies
{
    // Movie entity class inheriting from FullAuditedEntity
    public class Movie : FullAuditedAggregateRoot<Guid>
    {
        // Title of the movie, required and limited to 255 characters
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        public string MovieBlob {  get; set; }
        [Required]
        public string PosterBlob { get; set; }

        // Release date of the movie, required
        public DateTime ReleaseDate { get; set; }
        [Required]
        // Duration of the movie in minutes, must be between 1 and 300
        [Range(1, 300)]
        public int Duration { get; set; }

        // Description of the movie, limited to 1000 characters
        [StringLength(1000)]
        public string Description { get; set; }

        // Age rating of the movie (e.g., PG-13), limited to 10 characters
        [Required]
        [StringLength(10)]
        public string AgeRating { get; set; }


        // Many-to-Many with Actor Entity
        public List<MovieActor> MovieActors { get; set; }

        // Many-to-Many with Category Entity
        public List<MovieCategory> MovieCategories { get; set; }

        public float AverageRating { get; set; }
        public int TotalViews { get; set; }
        public int TotalDownloads { get; set; }

    }
}

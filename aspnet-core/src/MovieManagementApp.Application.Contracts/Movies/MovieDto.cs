using MovieManagementApp.Actors;
using MovieManagementApp.Categories;
using MovieManagementApp.Ratings;
using MovieManagementApp.UserMovieInteractions;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.Movies
{
    public class MovieDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string AgeRating { get; set; }
        public string PosterUrl { get; set; }
        public string VideoUrl { get; set; }

        public List<ActorDto> Actors { get; set; }
        public List<CategoryDto> Categories { get; set; }

        public List<RatingDto> Ratings { get; set; }
        //public List<UserMovieInteractionDto> UserMovieInteractions { get; set; }
        

        // إضافات جديدة
        public float AverageRating { get; set; }
        public int TotalViews { get; set; }
        public int TotalDownloads { get; set; }

        public List<Guid> ActorIds { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}

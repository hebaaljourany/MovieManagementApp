using MovieManagementApp.Categories;
using MovieManagementApp.Movies;
using System;
using Volo.Abp.Domain.Entities;


namespace MovieManagementApp.MovieCategories
{
    public class MovieCategory : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual Movie Movie { get; set; }
        public virtual Category Category { get; set; }
    }
}

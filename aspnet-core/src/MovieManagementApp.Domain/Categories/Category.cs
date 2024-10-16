using MovieManagementApp.MovieCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace MovieManagementApp.Categories
{
    public class Category : FullAuditedEntity<Guid>
    {
        [Required]
        public string CategoryName { get; set; }
        //Many-to-Many with Movie Entity
        public List<MovieCategory> MovieCategories { get; set; }
    }
}

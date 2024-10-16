using System;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.MovieCategories
{
    public class MovieCategoryDto : EntityDto<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

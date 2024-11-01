using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MovieManagementApp.Movies
{
    public class GetMovieInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public Guid? ActorId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

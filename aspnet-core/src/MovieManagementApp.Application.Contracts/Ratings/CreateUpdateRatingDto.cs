using System;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementApp.Application.Contracts.Ratings
{
    public class CreateUpdateRatingDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        public float Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime RatingDate { get; set; } = DateTime.Now;
    }
}

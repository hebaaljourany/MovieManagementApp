using System;
using System.ComponentModel.DataAnnotations;
using MovieManagementApp.UserMovieInteractions;


namespace MovieManagementApp.Application.Contracts.UserMovieInteractions
{
    public class CreateUpdateUserMovieInteractionDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        public InteractionType Interaction { get; set; }

        [DataType(DataType.Date)]
        public DateTime InteractionDate { get; set; } = DateTime.Now;
    }


}

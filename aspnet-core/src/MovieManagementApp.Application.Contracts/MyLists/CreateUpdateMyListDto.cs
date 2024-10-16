using System;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementApp.Application.Contracts.MyLists
{
    public class CreateUpdateMyListDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

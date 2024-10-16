using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementApp.Application.Contracts.Categories
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(128)]
        public string CategoryName { get; set; }
        public List<Guid> MovieIds { get; set; }



    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementApp.Application.Contracts.MyAccounts
{
    public class CreateUpdateMyAccountDto
    {
        [Required]
        [StringLength(128)]
        public string Username { get; set; }

        //[Required]
        //[StringLength(256)]
        //public string Email { get; set; }

       // [Required]
       // [StringLength(64)]
       // public string Password { get; set; }

       // public bool IsActive { get; set; } = true;
    }
}

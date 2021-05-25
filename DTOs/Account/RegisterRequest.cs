using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_hero.DTOs.Account
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public int? RoleId { get; set; }
    }
}

﻿using HP.Domain.Person;
using System.ComponentModel.DataAnnotations;

namespace HP.Api.DTO
{
    public class CreatePersonDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
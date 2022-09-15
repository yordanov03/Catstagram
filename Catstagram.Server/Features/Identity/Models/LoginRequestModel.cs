﻿using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Identity.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

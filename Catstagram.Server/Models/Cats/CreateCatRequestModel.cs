﻿using System.ComponentModel.DataAnnotations;
using static Catstagram.Server.Data.Validation.Cat;

namespace Catstagram.Server.Models.Cats
{
    public class CreateCatRequestModel
    {
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}

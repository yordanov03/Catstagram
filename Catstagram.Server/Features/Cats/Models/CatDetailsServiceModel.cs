﻿namespace Catstagram.Server.Features.Cats.Models
{
    public class CatDetailsServiceModel : CatListingServiceModel
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
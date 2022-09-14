using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Cats
{
    public class CatListingResponseModel
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}

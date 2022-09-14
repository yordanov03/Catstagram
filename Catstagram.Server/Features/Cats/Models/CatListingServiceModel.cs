using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Cats.Models
{
    public class CatListingServiceModel
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}

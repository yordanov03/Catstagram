using System.ComponentModel.DataAnnotations;
using static Catstagram.Server.Data.Validation.Cat;

namespace Catstagram.Server.Features.Cats.Models
{
    public class UpdateCatRequestModel
    { 
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using static Catstagram.Server.Data.Validation.User;

namespace Catstagram.Server.Data.Models
{
    public class Profile
    {
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public string Website { get; set; }

        [MaxLength(MaxBiographyDescription)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public bool isPrivate { get; set; }
    }
}

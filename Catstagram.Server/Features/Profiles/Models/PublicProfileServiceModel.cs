namespace Catstagram.Server.Features.Profiles.Models
{
    public class PublicProfileServiceModel : ProfileServiceModel
    {
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public string Website { get; set; }

        public string Biography { get; set; }

        public string Gender { get; set; }

        public bool IsPrivate { get; set; }
    }
}

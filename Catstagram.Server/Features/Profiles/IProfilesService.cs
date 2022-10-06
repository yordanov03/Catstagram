using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Profiles
{
    public interface IProfilesService
    {
        Task<ProfileServiceModel> ByUser(string userId);
        Task<Result> Update(
            string userId,
            string email,
            string username,
            string name,
            string mainPhotoUrl,
            string website,
            string biography,
            Gender gender,
            bool isPrivate);
    }
}

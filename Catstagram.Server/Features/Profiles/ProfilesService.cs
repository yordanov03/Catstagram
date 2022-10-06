using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using User = Catstagram.Server.Data.Models.User;

namespace Catstagram.Server.Features.Profiles
{
    public class ProfilesService : IProfilesService
    {
        private readonly CatstagramDbContext data;

        public ProfilesService(CatstagramDbContext data)
        => this.data = data;

        public async Task<ProfileServiceModel> ByUser(string userId)
        => await this.data
            .Users
            .Where(u => u.Id == userId)
            .Select(u => new ProfileServiceModel
            {
                Name = u.Profile.Name,
                Biography = u.Profile.Biography,
                Gender = u.Profile.Gender.ToString(),
                MainPhotoUrl = u.Profile.MainPhotoUrl,
                Website = u.Profile.Website,
                IsPrivate = u.Profile.isPrivate
            })
            .FirstOrDefaultAsync();

        public async Task<Result> Update(string userId, string email, string username, string name, string mainPhotoUrl, string website, string biography, Gender gender, bool isPrivate)
        {
            var user = await this.data
                .Users
                .Include(u=>u.Profile)
                .FirstOrDefaultAsync(p=>p.Id == userId);

            if (user == null)
            {
                return "User does not exist!";
            }

            if (user.Profile == null)
            {
                user.Profile = new Profile();
            }

            var result = await this.ChangeProfileEmail(user, userId, email);
            if (result.Failure)
            {
                return result;
            }

            result = await this.ChangeUsername(user, userId, username);
            if (result.Failure)
            {
                return result;
            }

            this.ChangeProfile(
                user.Profile,
                name,
                mainPhotoUrl,
                website,
                biography,
                gender,
                isPrivate);

            await this.data.SaveChangesAsync();
            return true;
        }

        private async Task<Result> ChangeProfileEmail(User user, string userId, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
            {
                var emailExists = await this.data
                .Users
                    .AnyAsync(u => u.Id != userId && u.Email == email);

                if (emailExists)
                {
                    return "The provided email is already taken!";
                }
                user.Email = email;
            }

            return true;
        }

        private async Task<Result> ChangeUsername(User user, string userId, string username)
        {
            if (!string.IsNullOrWhiteSpace(username) && user.UserName != username)
            {
                var usernameExists = await this.data
                    .Users
                    .AnyAsync(u => u.Id != userId && u.UserName == username);

                if (usernameExists)
                {
                    return "The provided username is already taken!";
                }

                user.UserName = username;
            }

            return true;
        }

        private void ChangeProfile(
            Profile profile,
            string name,
            string mainPhotoUrl,
            string website,
            string biography,
            Gender gender,
            bool isPrivate)
        {
            if (!string.IsNullOrEmpty(name) && profile.Name != name)
            {
                profile.Name = name;
            }

            if (!string.IsNullOrEmpty(mainPhotoUrl) && profile.MainPhotoUrl != mainPhotoUrl)
            {
                profile.MainPhotoUrl = mainPhotoUrl;
            }

            if (!string.IsNullOrEmpty(website) && profile.Website != website)
            {
                profile.Website = website;
            }

            if (!string.IsNullOrEmpty(biography) && profile.Biography != biography)
            {
                profile.Biography = biography;
            }

            if (!string.IsNullOrEmpty(gender.ToString()) && profile.Gender != gender)
            {
                profile.Gender = gender;
            }

            if (profile.isPrivate != isPrivate)
            {
                profile.isPrivate = isPrivate;
            }
        }
    }
}

using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Profiles
{
    public class ProfilesController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly ICurrentUserService currentUserService;

        public ProfilesController(IProfileService profileService, ICurrentUserService currentUserService)
        {
            this.profileService = profileService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProfileServiceModel>> MyProfile()
        => await this.profileService.ByUser(this.currentUserService.GetUserId());
    }
}

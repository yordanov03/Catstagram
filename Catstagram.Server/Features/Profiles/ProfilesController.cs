using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Extensions;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Profiles
{
    public class ProfilesController : ApiController
    {
        private readonly IProfilesService profilesService;
        private readonly ICurrentUserService currentUserService;

        public ProfilesController(IProfilesService profilesService, ICurrentUserService currentUserService)
        {
            this.profilesService = profilesService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileServiceModel>> MyProfile()
        => await this.profilesService.ByUser(this.currentUserService.GetUserId());

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProfileRequestModel model)
        {
            var userId = this.User.GetId();
            var result = await this.profilesService
                .Update(
                userId,
                model.Email,
                model.Username,
                model.Name,
                model.MainPhotoUrl,
                model.Website,
                model.Biography,
                model.Gender,
                model.isPrivate);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}

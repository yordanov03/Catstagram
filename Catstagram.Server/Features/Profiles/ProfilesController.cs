using Catstagram.Server.Features.Follows;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Extensions;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Catstagram.Server.Infrastructure.WebConstants;

namespace Catstagram.Server.Features.Profiles
{
    public class ProfilesController : ApiController
    {
        private readonly IProfilesService profilesService;
        private readonly ICurrentUserService currentUserService;
        private readonly IFollowService followService; 

        public ProfilesController(IProfilesService profilesService,
            ICurrentUserService currentUserService,
            IFollowService followService)
        {
            this.profilesService = profilesService;
            this.currentUserService = currentUserService;
            this.followService = followService;
        }

        [HttpGet]
        public async Task<ProfileServiceModel> MyProfile()
        => await this.profilesService.ByUser(this.currentUserService.GetUserId(), allInformation: true);

        [HttpGet]
        [AllowAnonymous]
        [Route(RouteId)]
        public async Task<ProfileServiceModel> Details(string id)
        {
            var includeAllInformation = await this.followService.IsFollower(id, this.currentUserService.GetUserId());

            if (!includeAllInformation)
            {
                var userIsPrivate = !await this.profilesService.IsPublic(id);
            }

            return await this.profilesService.ByUser(id);
        }

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

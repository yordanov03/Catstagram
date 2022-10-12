using Catstagram.Server.Features.Follows.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Follows
{
    public class FollowsController : ApiController
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IFollowService followService;

        public FollowsController(ICurrentUserService currentUserService,
            IFollowService followService)
        {
            this.currentUserService = currentUserService;
            this.followService = followService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Follow(FollowRequestModel model)
        {
            var result = await this.followService.Follow(
                model.UserId,
                this.currentUserService.GetUserId());

            if (result.Failure)
            {
                return this.BadRequest(result.Error);
            }

            return Ok();
        }
    }
}

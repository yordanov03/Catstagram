using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Extensions;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Catstagram.Server.Infrastructure.WebConstants;

namespace Catstagram.Server.Features.Cats
{
    public class CatsController : ApiController
    {
        private readonly ICatsService catsService;
        private readonly ICurrentUserService currentUser;

        public CatsController(ICatsService catsService) => this.catsService = catsService;

        [HttpGet]
        [Route("mycats")]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = this.currentUser.GetUserId();
            return await this.catsService.ByUser(userId);
        }

        [HttpGet]
        [Route(RouteId)]
        public async Task<CatDetailsServiceModel> Details(int id)
            => await this.catsService.Details(id);


        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult<Cat>> Create(CreateCatRequestModel model)
        {
            var userId = User.GetId();
            var cat = this.catsService.Create(model.ImageUrl, model.Description, userId);

            return CreatedAtAction(nameof(this.Create), cat.Id);
        }

        [HttpPut]
        [Route(RouteId)]
        public async Task<ActionResult> Update(int id, UpdateCatRequestModel model)
        {
            var userId = this.User.GetId();
            var updated = await this.catsService
                .Update(id, model.Description, userId);

            if (updated.Failure)
            {
                return BadRequest(updated.Error);
            }

            return Ok();
        }

        [HttpDelete]
        [Route(RouteId)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.User.GetId();

            var deleted = await this.catsService.Delete(id, userId);

            if (!deleted.Failure)
            {
                return BadRequest(deleted.Error);
            }

            return Ok();
        }
    }
}

using Catstagram.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Cats
{
    public class CatsController : ApiController
    {
        private readonly ICatsService catsService;

        public CatsController(ICatsService catsService) => this.catsService = catsService;


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            var userId = User.GetId();
            var id = this.catsService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }
    }
}

using Catstagram.Server.Data.Models;
using Catstagram.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Catstagram.Server.Features.Cats
{
    public class CatsController : ApiController
    {
        private readonly ICatsService catsService;

        public CatsController(ICatsService catsService) => this.catsService = catsService;


        [HttpPost]
        [Authorize]
        [Route(nameof(Create))]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<Cat>> Create(CreateCatRequestModel model)
        {
            var userId = User.GetId();
            var cat = this.catsService.Create(model.ImageUrl, model.Description, userId);

            return CreatedAtAction(nameof(this.Create), cat.Id);
        }
    }
}

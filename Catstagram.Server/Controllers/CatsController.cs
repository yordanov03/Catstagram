using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Infrastructure;
using Catstagram.Server.Models.Cats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{
    public class CatsController : ApiController
    {
        private readonly CatstagramDbContext data;

        public CatsController(CatstagramDbContext data)
        {
            this.data = data;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            var userId = this.User.GetId();
            var cat = new Cat
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                UserId = userId,
            };

            this.data.Add(cat);
            await this.data.SaveChangesAsync();
            return Created(nameof(this.Create), cat.Id);
        }
    }
}

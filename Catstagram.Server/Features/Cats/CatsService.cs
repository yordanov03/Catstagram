using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Cats.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Cats
{
    public class CatsService : ICatsService
    {
        private readonly CatstagramDbContext data;

        public CatsService(CatstagramDbContext data) => this.data = data;

        public async Task<int> Create(string imageUrl, string description, string userId)
        {
            var cat = new Cat
            {
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId,
            };

            data.Add(cat);
            await data.SaveChangesAsync();

            return cat.Id;
        }

        public async Task<IEnumerable<CatListingServiceModel>> ByUser(string userId)
        {
            return await this.data
                .Cats
                .Where(c=>c.UserId == userId)
                .Select(c=> new CatListingServiceModel 
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();
        }

        public Task<CatDetailsServiceModel> Details(int id)
            =>this.data
                 .Cats
                 .Where(c => c.Id == id)
                 .Select(c => new CatDetailsServiceModel
                 {
                     Id = c.Id,
                     UserId = c.UserId,
                     ImageUrl = c.ImageUrl,
                     Description = c.Description,
                     Username = c.User.UserName
                 })
                 .FirstOrDefaultAsync();

        public async Task<bool> Update(int id, string description, string userId)
        {
            var cat = await this.data
                .Cats
                .Where(c => c.Id == id && c.User.Id == userId)
                .FirstOrDefaultAsync();

            if(cat == null)
            {
                return false;
            }

            cat.Description = description;
            await this.data.SaveChangesAsync();
            return true;
        }
    }
}

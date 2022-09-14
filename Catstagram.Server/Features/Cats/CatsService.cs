using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Cats
{
    public class CatsService : ICatsService
    {
        private readonly CatstagramDbContext data;

        public CatsService(CatstagramDbContext data)
        {
            this.data = data;
        }

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

        public async Task<IEnumerable<CatListingResponseModel>> ByUser(string userId)
        {
            return await this.data
                .Cats
                .Where(c=>c.UserId == userId)
                .Select(c=> new CatListingResponseModel 
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();
        }
    }
}

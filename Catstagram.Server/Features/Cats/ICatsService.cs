﻿using Catstagram.Server.Features.Cats.Models;

namespace Catstagram.Server.Features.Cats
{
    public interface ICatsService
    {
        Task<int> Create(string imageUrl, string description, string userId);

        Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);

        Task<CatDetailsServiceModel> Details(int id);

        Task<bool> Update(int id, string description, string userId);

        Task<bool> Delete(int id, string userId);
    }
}

using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Cats
{
    public interface ICatsService
    {
        Task<int> Create(string imageUrl, string description, string userId);

        Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);

        Task<CatDetailsServiceModel> Details(int id);

        Task<Result> Update(int id, string description, string userId);

        Task<Result> Delete(int id, string userId);
    }
}

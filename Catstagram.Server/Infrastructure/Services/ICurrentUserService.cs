namespace Catstagram.Server.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        string GetUserName();
        string GetUserId();
    }
}

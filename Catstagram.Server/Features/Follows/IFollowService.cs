﻿using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Follows
{
    public interface IFollowService
    {
        Task<Result> Follow(string userId, string followerId);
        Task<bool> IsFollower(string userId, string followerId);
    }
}

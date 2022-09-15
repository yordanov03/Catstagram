﻿using Catstagram.Server.Data.Models;

namespace Catstagram.Server.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJWTToken(User user, string secret);
    }
}

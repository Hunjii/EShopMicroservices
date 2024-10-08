﻿using Microsoft.AspNetCore.Authorization;

namespace Ordering.Infrastructure.Authentication
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString())
        {
            
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.WebApi.Core.Identity
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly bool IsAuthenticated;

        public IdentityUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            IsAuthenticated = _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }

        public Guid GetUserId()
        {
            return IsAuthenticated ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetUserToken()
        {
            return IsAuthenticated ? _accessor.HttpContext.User.GetUserToken() : "";
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.WebApi.Core.Identity
{
    public interface IIdentityUserService
    {
        Guid GetUserId();
        string GetUserToken();
        HttpContext GetHttpContext();
    }
}

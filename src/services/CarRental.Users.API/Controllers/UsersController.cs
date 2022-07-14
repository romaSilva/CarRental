using CarRental.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Users.API.Controllers
{
    public class UsersController : MainController
    {
        public UsersController()
        {

        }

        public async Task<IActionResult> CreateOperator()
        {
            return CustomResponse();
        }

        public async Task<IActionResult> CreateCustomer()
        {
            return CustomResponse();
        }
    }
}

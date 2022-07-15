using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Core.DomainObjects
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Operator = "Operator";
            public const string Customer = "Customer";
        }
        public static class Claims
        {
            public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        }
    }
}

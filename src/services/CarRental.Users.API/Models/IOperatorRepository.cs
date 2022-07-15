using CarRental.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Users.API.Models
{
    public interface IOperatorRepository : IRepository<Operator>
    {
        Task<IEnumerable<Operator>> GetAll();
        Task<Operator> GetById(Guid operatorId);
        Task<Operator> GetByCompanyRegistration(string companyRegistration);
        void Add(Operator newOperator);
    }
}

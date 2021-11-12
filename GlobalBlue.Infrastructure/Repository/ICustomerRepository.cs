using GlobalBlue.Infrastructure.Persistence;
using System.Collections.Generic;

namespace GlobalBlue.Infrastructure.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        List<Customer> GetCustomers();
    }
}

using GlobalBlue.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace GlobalBlue.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext ctx) : base(ctx)
        {
        }

        public List<Customer> GetCustomers()
        {
            return Query().OrderBy(c => c.FirstName).ToList();
        }
    }
}
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GlobalBlue.Infrastructure.Persistence;
using GlobalBlue.Infrastructure.Profiles;
using GlobalBlue.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GlobaBlue.Infrastructure.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly Fixture fixture;
        private readonly CustomerRepository _repository;
        private DataContext context;
        private readonly IMapper mapper;

        public CustomerRepositoryTests()
        {
            fixture = new Fixture();
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            mapper = mockMapper.CreateMapper();

            var builder = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            context = new DataContext(builder.Options);
        }
        [Fact]
        public void Should_return_fake_customer()
        {
            var customer = fixture.Create<Customer>();


            context.Customers.Add(customer);
            context.SaveChanges();

            var customerRepository = new CustomerRepository(context);

            // Act
            var result = customerRepository.GetCustomers();

            //Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(1);
            result.First().Should().BeEquivalentTo(customer);

        }

        [Fact]
        public async Task Should_create_new_customer()
        {
            var customer = fixture.Create<Customer>();
           
            var customerRepository = new CustomerRepository(context);

            // Act
            customerRepository.Create(customer);
            await customerRepository.SaveChanges();
            //Assert
            context.Customers.SingleOrDefault(c=>c.FirstName== customer.FirstName).Should().Be(customer);

        }

        [Fact]
        public async Task Should_update_customer()
        {
            var customer = fixture.Create<Customer>();

            var customerRepository = new CustomerRepository(context);

            // Act
            customerRepository.Create(customer);
            await customerRepository.SaveChanges();

            customer.FirstName= fixture.Create<string>();

            customerRepository.Update(customer);
            await customerRepository.SaveChanges();
            //Assert
            context.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName).Should().Be(customer);

        }

        [Fact]
        public async Task Should_delete_customer()
        {
            var customer = fixture.Create<Customer>();

            var customerRepository = new CustomerRepository(context);

            // Act
            customerRepository.Create(customer);
            await customerRepository.SaveChanges();
            var db= context.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName);
            customerRepository.Delete(db.Id);
            await customerRepository.SaveChanges();
            //Assert
            context.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName).Should().BeNull();
        }
    }
}

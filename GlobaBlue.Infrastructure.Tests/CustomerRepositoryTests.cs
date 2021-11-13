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
        private DataContext context;
       

        public CustomerRepositoryTests()
        {
            fixture = new Fixture();
            
         
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
            var customer = fixture.Build<Customer>()
                                  .Without(c=>c.Id)
                                  .Create();
           
            var customerRepository = new CustomerRepository(context);

            // Act
            var created=await customerRepository.Create(customer);

            //Assert    
            Assert.NotEqual(0, created.Id);
            context.Customers.SingleOrDefault(c=>c.FirstName== customer.FirstName).Should().Be(customer);

        }

        [Fact]
        public async Task Should_update_customer()
        {
            var customer = fixture.Create<Customer>();

            var customerRepository = new CustomerRepository(context);

            // Act
            await customerRepository.Create(customer);           

            customer.FirstName= fixture.Create<string>();

            await customerRepository.Update(customer);
       
            //Assert
            context.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName).Should().Be(customer);

        }

        [Fact]
        public async Task Should_delete_customer()
        {
            var customer = fixture.Create<Customer>();

            var customerRepository = new CustomerRepository(context);

            // Act
            await customerRepository.Create(customer);
            
            var db= context.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName);
            await customerRepository.Delete(db.Id);
            
            //Assert
            context.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName).Should().BeNull();
        }
    }
}

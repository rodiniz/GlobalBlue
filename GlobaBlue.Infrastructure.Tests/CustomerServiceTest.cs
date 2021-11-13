using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using GlobalBlue.Dtos;
using GlobalBlue.Infrastructure.Persistence;
using GlobalBlue.Infrastructure.Profiles;
using GlobalBlue.Infrastructure.Repository;
using GlobalBlue.Infrastructure.Services;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GlobaBlue.Infrastructure.Tests
{
    public class CustomerServiceTest
    {
        private readonly Fixture fixture;
        private readonly ICustomerRepository _repository;
        private readonly CustomerService customerService;
        public CustomerServiceTest()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());
            var config = new MapperConfiguration((cfg => cfg.AddProfile(new CustomerProfile())));
            fixture.Register<IMapper>(() => new Mapper(config));
            _repository= fixture.Freeze<ICustomerRepository>();
            customerService = fixture.Create<CustomerService>();
        }
        [Fact]
        public async Task Should_call_created_and_savechangescalled()
        {
            // Arrange
            var model = fixture.Create<CustomerDto>();
            
            // Act          

            await customerService.Create(model);

            // Assert
            await _repository.Received(1).Create(Arg.Any<Customer>());
    
        }

        [Fact]
        public async Task Should_call_update_and_savechangescalled()
        {
            // Arrange
            var model = fixture.Create<CustomerDto>();

            // Act          

            await customerService.Update(model);

            // Assert
            await _repository.Received(1).Update(Arg.Any<Customer>());
           
        }

        [Fact]
        public async Task Should_call_delete_and_savechangescalled()
        {
            // Arrange
            var id = fixture.Create<int>();

            // Act          

            await customerService.Delete(id);

            // Assert
            await _repository.Received(1).Delete(id);
            
        }
    }
}

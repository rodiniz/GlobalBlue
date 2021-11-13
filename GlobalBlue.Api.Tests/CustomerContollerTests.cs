using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using GlobalBlue.Api.Controllers;
using GlobalBlue.Dtos;
using GlobalBlue.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GlobalBlue.Api.Tests
{
    public class CustomerContollerTests
    {
        private readonly Fixture fixture;
        private readonly CustomerController controller;
        public IBaseService<CustomerDto> _service;
        public CustomerContollerTests()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());
            fixture.Customize(new ControllerCustomization());
            _service = Substitute.For<IBaseService<CustomerDto>>();
            controller = new CustomerController(_service);
        }
        [Fact]
        public void Should_return_a_list_ofcustomers_when_list_is_called()
        {
            // Arrange
            var customers = fixture.CreateMany<CustomerDto>(3).ToList();
            _service.GetAll().Returns(customers);
            // Act          

            var result = controller.List();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CustomerDto>>>(result);
            OkObjectResult res = result.Result as OkObjectResult;
            res.Value.Should().BeEquivalentTo(customers);
        }
        [Fact]
        public void Should_return_a_customer_when_get_is_called()
        {
            // Arrange
            var customer = fixture.Create<CustomerDto>();
            int id=fixture.Create<int>();
            _service.Get(id).Returns(customer);
            // Act          

            var result = controller.Get(id);

            // Assert
            Assert.IsType<ActionResult<CustomerDto>>(result);
            OkObjectResult res = result.Result as OkObjectResult;
            res.Value.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task Should_call_update_when_put_is_called()
        {
            // Arrange
            var customer = fixture.Create<CustomerDto>();
          
          
            // Act          

            var result = await controller.Put(customer);

            // Assert

            result.Should().BeOfType<OkResult>();
            await _service.Received(1).Update(customer);
           
        }

        [Fact]
        public async Task Should_call_create_when_post_is_called()
        {
            // Arrange
            var customer = fixture.Create<CustomerDto>();


            // Act          

            var result = await controller.Post(customer);

            // Assert

            result.Should().BeOfType<OkObjectResult>();
            await _service.Received(1).Create(customer);

        }

    }
}

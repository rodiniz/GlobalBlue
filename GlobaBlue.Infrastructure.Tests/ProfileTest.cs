using AutoMapper;
using GlobalBlue.Infrastructure.Profiles;
using Xunit;

namespace GlobaBlue.Infrastructure.Tests
{
    public class ProfileTest
    {
        [Fact]
        public void Should_not_throw_exception_when_mapping_user_entity_to_user()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CustomerProfile()));
            configuration.AssertConfigurationIsValid();
        }
    }
}
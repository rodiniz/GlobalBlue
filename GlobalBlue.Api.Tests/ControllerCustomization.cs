using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GlobalBlue.Api.Tests
{
    public class ControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<BindingInfo>(b => b.OmitAutoProperties());
        }
    }
}
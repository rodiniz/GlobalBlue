using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

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

using AutoMapper;
using GlobalBlue.Dtos;
using GlobalBlue.Infrastructure.Persistence;

namespace GlobalBlue.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}
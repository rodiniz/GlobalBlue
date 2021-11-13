using AutoMapper;
using GlobalBlue.Dtos;
using GlobalBlue.Infrastructure.Persistence;
using GlobalBlue.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalBlue.Infrastructure.Services
{
    public class CustomerService : IBaseService<CustomerDto>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<CustomerDto> Create(CustomerDto model)
        {
            var entity= await _repository.Create(_mapper.Map<Customer>(model));
            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task Delete(int idEntity)
        {
            await _repository.Delete(idEntity);            
        }

        public CustomerDto Get(int id)
        {
            return _mapper.Map<CustomerDto>(_repository.Get(id));
        }

        public List<CustomerDto> GetAll()
        {
            return _mapper.Map<List<CustomerDto>>(_repository.GetCustomers());
        }

        public async Task Update(CustomerDto model)
        {
            await _repository.Update(_mapper.Map<Customer>(model));           
        }
    }
}

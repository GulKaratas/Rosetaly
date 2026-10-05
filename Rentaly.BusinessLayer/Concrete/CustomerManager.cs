using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.EntityFramework;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IMapper _mapper;
        public CustomerManager(ICustomerDal customerDal, IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
        }
        public async Task TDeleteAsync(int id)
        {
            await _customerDal.DeleteAsync(id);
        }

        public async Task<GetCustomerByIdDto> TGetByIdAsync(int id)
        {
            var value = await _customerDal.GetByIdAsync(id);
            return _mapper.Map<GetCustomerByIdDto>(value);
        }

        public async Task<List<ResultCustomerDto>> TGetListAsync()
        {
            var values = await _customerDal.GetListAsync();
            return _mapper.Map<List<ResultCustomerDto>>(values);

        }

        public async Task TInsertAsync(CreateCustomerDto createCustomerDto)
        {
            var value = _mapper.Map<Customer>(createCustomerDto);
            await _customerDal.InsertAsync(value);

        }

        public async Task TUpdateAsync(UpdateCustomerDto updateCustomerDto)
        {
            var value = _mapper.Map<Customer>(updateCustomerDto);
            await _customerDal.UpdateAsync(value);
        }
    }
}

using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface ICustomerService 
    {
        Task<List<ResultCustomerDto>> TGetListAsync();
        Task<GetCustomerByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateCustomerDto createCustomerDto);
        Task TUpdateAsync(UpdateCustomerDto updateCustomerDto);
        Task TDeleteAsync(int id);
    }
}

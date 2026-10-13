using Rentaly.DtoLayer.CarDtos;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IRentalService 
    {
        Task<List<ResultRentalDto>> TGetListAsync();
        Task<GetRentalByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateRentalDto createRentalDto);
        Task TUpdateAsync(UpdateRentalDto updateRentalDto);
        Task TDeleteAsync(int id);
        Task<int> TGetCountAsync();
    }
}

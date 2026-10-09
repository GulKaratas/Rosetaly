using Rentaly.DtoLayer.About;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IAboutService 
    {
        Task<List<ResultAboutDto>> TGetListAsync();
        Task<ResultAboutDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateAboutDto createAboutDto);
        Task TUpdateAsync(UpdateAboutDto updateAboutDto);
        Task TDeleteAsync(int id);
    }
}

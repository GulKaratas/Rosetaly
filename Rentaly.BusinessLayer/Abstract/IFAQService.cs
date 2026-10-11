using Rentaly.DtoLayer.FAQDtos;
using Rentaly.DtoLayer.Testimonial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IFAQService
    {
        Task<List<ResultFAQDto>> TGetListAsync();
        Task<GetFAQByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateFAQDto createFAQDto);
        Task TUpdateAsync(UpdateFAQDto updateFAQDto);
        Task TDeleteAsync(int id);
    }
}

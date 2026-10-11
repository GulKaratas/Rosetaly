using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.Testimonial;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface ITestimonialService
    {
        Task<List<ResultTestimonialDto>> TGetListAsync();
        Task<GetTestimonialByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateTestimonialDto createTestimonialDto);
        Task TUpdateAsync(UpdateTestimonialDto updateTestimonialDto);
        Task TDeleteAsync(int id);
    }
}

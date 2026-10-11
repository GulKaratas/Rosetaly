using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.Testimonial;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class TestimonialManager : ITestimonialService
    {
        private readonly ITestimonialDal _testimonialDal;
        private readonly IMapper _mapper;


        public TestimonialManager(ITestimonialDal testimonialDal, IMapper mapper)
        {
            _testimonialDal = testimonialDal;
            _mapper = mapper;
        }
        public async Task TDeleteAsync(int id)
        {
            await _testimonialDal.DeleteAsync(id);
        }

        public async Task<GetTestimonialByIdDto> TGetByIdAsync(int id)
        {
            var testimonial = await _testimonialDal.GetByIdAsync(id);
            return _mapper.Map<GetTestimonialByIdDto>(testimonial);
        }

        public async Task<List<ResultTestimonialDto>> TGetListAsync()
        {
            var testimonials = await _testimonialDal.GetListAsync();
            return _mapper.Map<List<ResultTestimonialDto>>(testimonials);
        }

        public async Task TInsertAsync(CreateTestimonialDto createTestimonialDto)
        {
            var testimonial = _mapper.Map<Testimonial>(createTestimonialDto);
            await _testimonialDal.InsertAsync(testimonial);
        }

        public async Task TUpdateAsync(UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonial = _mapper.Map<Testimonial>(updateTestimonialDto);
            await _testimonialDal.UpdateAsync(testimonial);
        }
    }
}

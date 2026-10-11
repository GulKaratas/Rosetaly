using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.FAQDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class FAQManager : IFAQService
    {
        private readonly IFAQDal _faqDal;
        private readonly IMapper _mapper;

        public FAQManager(IFAQDal faqDal, IMapper mapper)
        {
            _faqDal = faqDal;
            _mapper = mapper;
        }
        public async Task TDeleteAsync(int id)
        {
            await _faqDal.DeleteAsync(id);
        }

        public async Task<GetFAQByIdDto> TGetByIdAsync(int id)
        {
            var faq = await _faqDal.GetByIdAsync(id);
            return _mapper.Map<GetFAQByIdDto>(faq);
        }

        public async Task<List<ResultFAQDto>> TGetListAsync()
        {
            var faqs = await _faqDal.GetListAsync();
            return _mapper.Map<List<ResultFAQDto>>(faqs);
        }

        public async Task TInsertAsync(CreateFAQDto createFAQDto)
        {
            var faq = _mapper.Map<FAQ>(createFAQDto);
            await _faqDal.InsertAsync(faq);
        }

        public async Task TUpdateAsync(UpdateFAQDto updateFAQDto)
        {
            var faq = _mapper.Map<FAQ>(updateFAQDto);
            await _faqDal.UpdateAsync(faq);
        }
    }
}

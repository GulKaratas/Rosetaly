using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.About;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;
        private readonly IMapper _mapper;

        public AboutManager(IAboutDal aboutDal, IMapper mapper)
        {
            _aboutDal = aboutDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id) => await _aboutDal.DeleteAsync(id);

        public async Task<ResultAboutDto> TGetByIdAsync(int id)
        {
            var about = await _aboutDal.GetByIdAsync(id);
            return _mapper.Map<ResultAboutDto>(about);
        }

        public async Task<List<ResultAboutDto>> TGetListAsync()
        {
            var abouts = await _aboutDal.GetListAsync();
            return _mapper.Map<List<ResultAboutDto>>(abouts);
        }

        public async Task TInsertAsync(CreateAboutDto createAboutDto)
        {
            var about = _mapper.Map<About>(createAboutDto);
            await _aboutDal.InsertAsync(about);
        }

        public async Task TUpdateAsync(UpdateAboutDto updateAboutDto)
        {
            var about = _mapper.Map<About>(updateAboutDto);
            await _aboutDal.UpdateAsync(about);
        }
    }
}

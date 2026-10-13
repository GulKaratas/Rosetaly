using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.EntityFramework;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.DtoLayer.FAQDtos;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly IMapper _mapper;
        public RentalManager(IRentalDal rentalDal,IMapper mapper)
        {
            _rentalDal = rentalDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _rentalDal.DeleteAsync(id);
        }

        public async Task<GetRentalByIdDto> TGetByIdAsync(int id)
        {
            var rental = await _rentalDal.GetByIdAsync(id);
            return _mapper.Map<GetRentalByIdDto>(rental);
        }

        public async Task<int> TGetCountAsync()
        {
            return await _rentalDal.CountAsync();
        }

        public async Task<List<ResultRentalDto>> TGetListAsync()
        {
            var rental = await _rentalDal.GetListAsync();
            return _mapper.Map<List<ResultRentalDto>>(rental);
        }

        public async Task TInsertAsync(CreateRentalDto createRentalDto)
        {
            var rental = _mapper.Map<Rental>(createRentalDto);
            await _rentalDal.InsertAsync(rental);
        }

        public async Task TUpdateAsync(UpdateRentalDto updateRentalDto)
        {
            var rental = _mapper.Map<Rental>(updateRentalDto);
            await _rentalDal.UpdateAsync(rental);
        }
    }
}

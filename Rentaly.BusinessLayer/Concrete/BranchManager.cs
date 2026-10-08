using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class BranchManager : IBranchService
    {
        private readonly IBranchDal _branchDal;
        private readonly IMapper _mapper;

        public BranchManager(IBranchDal branchDal, IMapper mapper)
        {
            _branchDal = branchDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _branchDal.DeleteAsync(id);
        }

        public async Task<GetBranchByIdDto> TGetByIdAsync(int id)
        {
            var value = await _branchDal.GetByIdAsync(id);
            return _mapper.Map<GetBranchByIdDto>(value);
        }

        public async Task<List<ResultBranchDto>> TGetListAsync()
        {
            var value = await _branchDal.GetListAsync();
            return _mapper.Map<List<ResultBranchDto>>(value);
        }

        public async Task TInsertAsync(CreateBranchDto createBranchDto)
        {
            var value = _mapper.Map<Branch>(createBranchDto);
            await _branchDal.InsertAsync(value);
        }

        public async Task TUpdateAsync(UpdateBranchDto updateBranchDto)
        {
            var value = _mapper.Map<Branch>(updateBranchDto);
            await _branchDal.UpdateAsync(value);
        }
    }
}

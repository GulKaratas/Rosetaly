using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IBranchService 
    {
        Task<List<ResultBranchDto>> TGetListAsync();
        Task<GetBranchByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateBranchDto createBranchDto);
        Task TUpdateAsync(UpdateBranchDto updateBranchDto);
        Task TDeleteAsync(int id);
    }
}

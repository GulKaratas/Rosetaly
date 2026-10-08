using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface ICategoryService 
    {
        Task<List<ResultCategoryDto>> TGetListAsync();
        Task<GetCategoryByIdDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateCategoryDto createCategoryDto);
        Task TUpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task TDeleteAsync(int id);
    }
}

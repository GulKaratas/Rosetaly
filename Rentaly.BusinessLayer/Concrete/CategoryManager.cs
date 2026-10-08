using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _categoryDal.DeleteAsync(id);
        }

        public async Task<GetCategoryByIdDto> TGetByIdAsync(int id)
        {
            var category = await _categoryDal.GetByIdAsync(id);
            return _mapper.Map<GetCategoryByIdDto>(category);
        }

        public async Task<List<ResultCategoryDto>> TGetListAsync()
        {
            var categories = await _categoryDal.GetListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(categories);
        }

        public async Task TInsertAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryDal.InsertAsync(value);
        }

        public async Task TUpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            await _categoryDal.UpdateAsync(value);
        }
        }
    }


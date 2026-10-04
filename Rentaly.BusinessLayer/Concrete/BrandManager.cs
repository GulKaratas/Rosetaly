using FluentValidation;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;


namespace Rentaly.BusinessLayer.Concrete
{
    public class BrandManager : IBrandService
    {
        public readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public async Task TDeleteAsync(int id)
        {
            await _brandDal.DeleteAsync(id);
        }

        public async Task<Brand> TGetByIdAsync(int id)
        {
            return await _brandDal.GetByIdAsync(id);
        }

        public async Task<List<Brand>> TGetListAsync()
        {
            return await _brandDal.GetListAsync();
        }

        public async Task TInsertAsync(Brand entity)
        {
            var validator = new BrandValidator();
            var result = validator.Validate(entity);
            if(!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            await _brandDal.InsertAsync(entity);
        }


        public async Task TUpdateAsync(Brand entity)
        {
            await _brandDal.UpdateAsync(entity);

        }
    }
}

using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Concrete
{
    public class OurFeatureManager : IOurFeatureService
    {
        private readonly IOurFeatureDal _ourFeatureDal;

        public OurFeatureManager(IOurFeatureDal ourFeatureDal)
        {
            _ourFeatureDal = ourFeatureDal;
        }
        public async Task TDeleteAsync(int id)
        {
            await _ourFeatureDal.DeleteAsync(id);
        }

        public async Task<OurFeature> TGetByIdAsync(int id)
        {
            return await _ourFeatureDal.GetByIdAsync(id);
        }

        public async Task<int> TGetCountAsync()
        {
            return await _ourFeatureDal.CountAsync();
        }

        public async Task<List<OurFeature>> TGetListAsync()
        {
            return await _ourFeatureDal.GetListAsync();
        }

        public async Task TInsertAsync(OurFeature entity)
        {
            await _ourFeatureDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(OurFeature entity)
        {
            await _ourFeatureDal.UpdateAsync(entity);
        }
    }
}

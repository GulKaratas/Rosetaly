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
    public class BranchManager : IBranchService
    {
        private readonly IBranchDal _branchDal;

        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }
        public async Task TDeleteAsync(int id)
        {
            await _branchDal.DeleteAsync(id);
        }

        public async Task<Branch> TGetByIdAsync(int id)
        {
            return await _branchDal.GetByIdAsync(id);
        }
        

        public async Task<List<Branch>> TGetListAsync()
        {
            return await _branchDal.GetListAsync();
        }

        public async Task TInsertAsync(Branch entity)
        {
            await _branchDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Branch entity)
        {
            await _branchDal.UpdateAsync(entity);
        }
    }
}

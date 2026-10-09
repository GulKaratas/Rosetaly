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
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;

        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }

        public async Task TDeleteAsync(int id) => await _reviewDal.DeleteAsync(id);
        public async Task<Review> TGetByIdAsync(int id) => await _reviewDal.GetByIdAsync(id);

        public async Task<int> TGetCountAsync()
        {
            return await _reviewDal.CountAsync();
        }
        public async Task<int> TGetDistinctCustomerCountAsync()
        {
            return await _reviewDal.GetDistinctCustomerCountAsync();
        }
        public async Task<List<Review>> TGetListAsync() => await _reviewDal.GetListAsync();
        public async Task TInsertAsync(Review entity) => await _reviewDal.InsertAsync(entity);
        public async Task TUpdateAsync(Review entity) => await _reviewDal.UpdateAsync(entity);

    }
}

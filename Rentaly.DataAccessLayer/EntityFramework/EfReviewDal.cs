using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfReviewDal : GenericRepository<Review>, IReviewDal
    {
        private readonly RentalyContext _context;

        public EfReviewDal(RentalyContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> GetDistinctCustomerCountAsync()
        {
            return await _context.Reviews
                                 .Select(r => r.CustomerId)
                                 .Distinct()
                                 .CountAsync();
        }
    }
}

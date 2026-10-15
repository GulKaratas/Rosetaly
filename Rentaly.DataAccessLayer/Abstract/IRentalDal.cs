using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DataAccessLayer.Abstract
{
    public interface IRentalDal : IGenericDal<Rental>
    {
        Task<bool> HasApprovedOverlapAsync(int carId, DateTime pickup, DateTime returnDate, int excludeRentalId);
        Task<List<int>> GetApprovedOverlapCarIdsAsync(DateTime pickup, DateTime returnDate);
    }
}

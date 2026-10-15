
using System;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IEmailService
    {
        Task SendReservationApprovedEmailAsync(
            string email,
            string customerName,
            string carName,
            DateTime pickupDate,
            DateTime returnDate,
            decimal totalPrice,
            string discountCode);
    }
}

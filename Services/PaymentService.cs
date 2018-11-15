using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetPayments();

    }
    public class PaymentService : IPaymentService
    {
        public IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public Task<List<Payment>> GetPayments()
        {
            throw new NotImplementedException();
        }
    }
}
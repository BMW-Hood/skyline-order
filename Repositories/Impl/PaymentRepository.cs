using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{

    public interface IPaymentRepository : IRepository<Payment>
    {
        
    }

    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDatabaseFactory databaseFactory, AppSettings settings) : base(databaseFactory, settings)
        {         
        }

    }
}

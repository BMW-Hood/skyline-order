using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Common.CustomExceptions;

namespace Repositories
{

    public interface IPaymentRepository : IRepository<Payment>
    {
        /// <summary>
        /// 查询支付记录
        /// </summary>
        /// <param name="payChannel"></param>
        /// <param name="payStatus"></param>
        /// <param name="orderNo"></param>
        /// <param name="date"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (int total, IList<Payment> payments) QueryPayments(List<PayChannel> payChannel, List<PayStatus> payStatus, string orderNo, DateTime? date, int pageIndex, int pageSize);

        /// <summary>
        /// 获取支付记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (int total, IList<Payment> payments) GetPayments(int pageIndex, int pageSize);
    }

    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDatabaseFactory databaseFactory, IAppSettings settings) : base(databaseFactory, settings)
        {
        }


        public (int, IList<Payment>) GetPayments(int pageIndex, int pageSize)
        {
            //throw new BusinessException(BusinessException.ErrorDescriptor.USER_NOT_LOGIN);
            var pageData = GetListByPage(x => true, x => x.PayTime, pageIndex, pageSize);
            return pageData;
        }

        public (int, IList<Payment>) QueryPayments(List<PayChannel> payChannels, List<PayStatus> payStatus, string orderNo, DateTime? date, int pageIndex=1, int pageSize=10)
        {
            var query = DbSet.AsQueryable();
            if (payChannels != null && payChannels.Any())
            {
                query = query.Where(x => payChannels.Contains(x.Channel));
            }
            if (payChannels != null&&payStatus.Any())
            {
                query = query.Where(x=>payStatus.Contains(x.Status));
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(x=>x.OrderNO==orderNo);
            }
            if (date!=null)
            {
                query = query.Where(x => x.PayTime>date&&x.PayTime<date.Value.AddDays(1));
            }
            var count = query.Count();
            query = query.OrderByDescending(x => x.PayTime);
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return (count, list == null ? new List<Payment>() : list.ToList());
        }
    }

}

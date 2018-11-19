using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

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
        (int total, IList<Payment> payments) QueryPayments(List<PayChannel> payChannel, List<PayStatus> payStatus, string orderNo, DateTime date, int pageIndex, int pageSize);

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
            throw new Exception("test");
            var pageData = GetListByPage(x=>true,x=>x.PayTime,pageIndex,pageSize);
            return pageData;
        }

        public (int, IList<Payment>) QueryPayments(List<PayChannel> payChannels, List<PayStatus> payStatus, string orderNo, DateTime date, int pageIndex, int pageSize)
        {
            Expression<Func<Payment, bool>> where = x => payChannels.Contains(x.Channel) && payStatus.Contains(x.Status)
            && (!string.IsNullOrWhiteSpace(orderNo) 
            && x.OrderNO == orderNo) 
            && x.PayTime > date 
            && x.PayTime < date.AddDays(1);
            //根据PayTime排序
            var pageData = GetListByPage(where, x => x.PayTime, pageIndex, pageSize);
            return pageData;
        }


    }
}

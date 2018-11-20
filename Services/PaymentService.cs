using AutoMapper;
using Contracts.Dtos;
using Contracts.Requests;
using Contracts.Responses;
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
        PaymentViewResponse GetPayments(int pageIndex,int pageSize);
        PaymentViewResponse QueryPayments(PaymentQueryRequest request);

    }
    public class PaymentService : IPaymentService
    {
        private IPaymentRepository _paymentRepository;
        private IMapper _mapper { get; set; }
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public PaymentViewResponse GetPayments(int pageIndex, int pageSize)
        {
           var result = _paymentRepository.GetPayments(pageIndex, pageSize);
           var total = result.total;
            var payments= _mapper.Map<IList<PaymentDto>>(result.payments);
            PaymentViewResponse response = new PaymentViewResponse()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Payments = payments,
                Total = total
            };
            return response;
        }

        public PaymentViewResponse QueryPayments(PaymentQueryRequest request)
        {
            var result = _paymentRepository.QueryPayments(request.PayChannels,request.PayStatuses,request.OrderNO,request.PayTime, request.PageIndex, request.PageSize);
            var total = result.total;
            var payments = _mapper.Map<IList<PaymentDto>>(result.payments);
            PaymentViewResponse response = new PaymentViewResponse()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Payments = payments,
                Total = total
            };
            return response;
        }

        

    }
}
using AutoMapper;
using Common.Interceptors;
using Contracts.Dtos;
using Contracts.Requests;
using Contracts.Responses;
using OpenTracing;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IPaymentService
    {

        PaymentViewResponse GetPayments(int pageIndex, int pageSize);

        PaymentViewResponse QueryPayments(PaymentQueryRequest request);
    }

    public class PaymentService : IPaymentService
    {
        private IPaymentRepository _paymentRepository;
        private IMapper _mapper;
        private ITracer _tracer;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ITracer tracer)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _tracer = tracer;
        }

        public PaymentViewResponse GetPayments(int pageIndex, int pageSize)
        {
            using (IScope childScope = _tracer.BuildSpan("MySql SELECT").StartActive(finishSpanOnDispose: true))
            {
                childScope.Span.Log(DateTimeOffset.Now, "Msql Start");
                var result = _paymentRepository.GetPayments(pageIndex, pageSize);
                childScope.Span.Log(DateTimeOffset.Now, "Msql Finish");
                var total = result.total;
                var payments = _mapper.Map<IList<PaymentDto>>(result.payments);
                PaymentViewResponse response = new PaymentViewResponse()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Payments = payments,
                    Total = total
                };
                return response;
            }

        }

        public PaymentViewResponse QueryPayments(PaymentQueryRequest request)
        {
            var result = _paymentRepository.QueryPayments(request.PayChannels, request.PayStatuses, request.OrderNO, request.PayTime, request.PageIndex, request.PageSize);
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
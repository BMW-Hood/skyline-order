using Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Responses
{
   public class PaymentViewResponse
    {
        public IList<PaymentDto> Payments { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}

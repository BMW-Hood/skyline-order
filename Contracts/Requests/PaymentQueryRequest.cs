using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Requests
{
   public class PaymentQueryRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<PayChannel> PayChannels { get; set; }
        public List<PayStatus> PayStatuses { get; set; }
        public string OrderNO { get; set; } 
        public DateTime PayTime { get; set; }
    }
}

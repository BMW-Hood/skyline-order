using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contracts.Dtos
{
   public class PaymentDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string OrderNO { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentNO { get; set; }
        [Required]
        public PayChannel Channel { get; set; }
        [Required]
        public PayStatus Status { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public DateTime PayTime { get; set; }
    }
}

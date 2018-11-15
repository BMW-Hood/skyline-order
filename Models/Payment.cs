using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("payment")]
    public class Payment:Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    public enum PayChannel
    {
        AliPay,
        WeChat,
        Card,
    }
    public enum PayStatus
    {
        Success,
        Failed
    }
}

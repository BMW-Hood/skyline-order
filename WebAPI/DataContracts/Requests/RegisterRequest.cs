using Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DataContracts.Requests
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public DateTime? Birthday { get; set; }
        public virtual string EnCodePassword => EncryptHelper.Md5(Password);
    }
}
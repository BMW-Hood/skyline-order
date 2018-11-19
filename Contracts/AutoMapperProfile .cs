using AutoMapper;
using Contracts.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
   public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Payment,PaymentDto>();
            CreateMap<PaymentDto, Payment>();
        }
    }
}

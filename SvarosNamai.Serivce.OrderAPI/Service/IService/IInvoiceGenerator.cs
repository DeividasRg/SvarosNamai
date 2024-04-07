﻿using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service.IService
{
    public interface IInvoiceGenerator
    {
        Task<ResponseDto> GenerateInvoice(int orderId);
    }
}
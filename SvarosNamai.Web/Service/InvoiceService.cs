using SvarosNamai.Web.Models;
using SvarosNamai.Web.Service.IService;
using SvarosNamai.Web.Utility;

namespace SvarosNamai.Web.Service
{
    public class InvoiceService : IInvoiceService
    {

        private readonly IBaseService _baseService;

        public InvoiceService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetInvoice(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.InvoiceAPIBase + "/api/invoice/DownloadInvoice/" + orderId
            });
        }
    }
}

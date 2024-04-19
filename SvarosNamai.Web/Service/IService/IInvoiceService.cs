using SvarosNamai.Web.Models;

namespace SvarosNamai.Web.Service.IService
{
	public interface IInvoiceService
	{
		Task<ResponseDto> GetInvoice(int orderId);
	}
}

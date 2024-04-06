using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Data;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Document = iText.Layout.Document;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class InvoiceGenerator : IInvoiceGenerator
    {


        private readonly AppDbContext _db;
        private ResponseDto _response;

        public InvoiceGenerator(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }




        public async Task<ResponseDto> GenerateInvoice(int orderId)
        {
            try
            {

                string outputPath = "C:\\Users\\deivi\\Desktop\\example.pdf";


                using (PdfWriter writer = new PdfWriter(outputPath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    { 
                        using(Document document = new Document(pdf)) 
                        {
                            Paragraph paragraph = new Paragraph("Hello, World!");
                            document.Add(paragraph);
                            document.Close();
                        }
                    }
                }

                _response.Message = "Pdf Created";
                




            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}

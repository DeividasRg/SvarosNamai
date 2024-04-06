using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SvarosNamai.Serivce.OrderAPI.Models;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using SvarosNamai.Service.OrderAPI.Data;
using SvarosNamai.Service.OrderAPI.Models.Dtos;
using System;
using System.Collections;
using System.Linq;
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
                var order = _db.Orders.Find(orderId);
                var orderlines = _db.OrderLines.Where(u => u.Order == order);
                String path = @"C:\\Users\\deivi\\Desktop\\example.pdf";
                PdfWriter writer = new PdfWriter(path);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                Paragraph header = new Paragraph("Sąskaita faktūra")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
                document.Add(header);

                Paragraph details = new Paragraph()
                    .AddTabStops(new TabStop(100, TabAlignment.RIGHT))
                    .Add("Date: ").Add(DateTime.Now.ToShortDateString()).Add("\n")
                    .Add("Invoice #: ").Add($"SN - {orderId}").Add("\n")
                    .Add("Customer: ").Add($"{order.Name} {order.LastName}").Add("\n")
                    .Add("Address: ").Add($"{order.Street} {order.HouseNo}{order.HouseLetter}, {order.City}").Add("\n")
                    .Add("Email: ").Add($"{order.Email}");
                document.Add(details);

                Table table = new Table(1)
                    .UseAllAvailableWidth();


                Cell cell = new Cell().Add(new Paragraph("Paslauga"));
                table.AddHeaderCell(cell);


                foreach(var line in orderlines)
                {
                    Cell productNameCell = new Cell().Add(new Paragraph(line.ProductName));
                    table.AddCell(productNameCell);
                }


                document.Add(table);

                
                Paragraph total = new Paragraph($"Total: {order.Price}")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBold();
                document.Add(total);


                document.Close();


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

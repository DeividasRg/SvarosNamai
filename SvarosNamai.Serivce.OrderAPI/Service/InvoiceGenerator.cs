using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
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
using System.ComponentModel.Design;
using AngleSharp.Css.Dom;
using iText.Layout.Borders;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class InvoiceGenerator : IInvoiceGenerator
    {


        private readonly AppDbContext _db;
        private ResponseDto _response;
        private readonly IErrorLogger _error;

        public InvoiceGenerator(AppDbContext db, IErrorLogger error)
        {
            _db = db;
            _response = new ResponseDto();
            _error = error;
        }


        public async Task<ResponseDto> GenerateInvoice(int orderId)
        {
            try
            {
                var order = _db.Orders.Find(orderId);
                var orderlines = _db.OrderLines.Where(u => u.Order == order);
                String path = $@"C:\Users\dragauskas\OneDrive - barbora.lt\Desktop\\SN-{order.OrderId}.pdf";
                PdfWriter writer = new PdfWriter(path);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", PdfEncodings.IDENTITY_H);
                document.SetFont(font);

                Paragraph header = new Paragraph($"Sąskaita faktūra: SN - {order.OrderId}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold();
                document.Add(header);

                document.Add(new Paragraph().SetMarginBottom(20));

                
                Table detailsTable = new Table(2)
                    .UseAllAvailableWidth();


                Paragraph details = new Paragraph()
                    .AddTabStops(new TabStop(100, TabAlignment.RIGHT))
                    .Add("Data: ").Add(DateTime.Now.ToShortDateString()).Add("\n")
                    .Add("Užsakymo nr. : ").Add($"{orderId}").Add("\n")
                    .Add("Klientas: ").Add($"{order.Name} {order.LastName}").Add("\n")
                    .Add("Adresas: ").Add($"{order.Street} {order.HouseNo}{order.HouseLetter}, {order.City}").Add("\n")
                    .Add("El.pašto adresas: ").Add($"{order.Email}").Add("\n")
                    .Add("Telefono nr.: ").Add($"{order.PhoneNumber}");

                
                Paragraph companyDetails = new Paragraph()
                    .AddTabStops(new TabStop(100, TabAlignment.RIGHT))
                    .Add("Company Name: ").Add("MB \"Švaros Namai\"").Add("\n")
                    .Add("Address: ").Add("Taikos g. 13, Vilnius").Add("\n")
                    .Add("Email: ").Add("greta@svarosnamai.lt").Add("\n")
                    .Add("Phone: ").Add("+370 648 (71)806")
                    .SetTextAlignment(TextAlignment.RIGHT); 

                
                Cell detailsCell = new Cell().Add(details)
                                               .SetBorder(Border.NO_BORDER);
                Cell companyDetailsCell = new Cell().Add(companyDetails)
                                                    .SetBorder(Border.NO_BORDER);
                detailsTable.AddCell(detailsCell);
                detailsTable.AddCell(companyDetailsCell);

                document.Add(detailsTable);

                
                document.Add(new Paragraph().SetMarginBottom(20));

                Paragraph paslaugaHeader = new Paragraph("Paslaugos")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold();
                    

                document.Add(paslaugaHeader);

                document.Add(new Paragraph().SetMarginBottom(5));

                Table table = new Table(1)
                    .UseAllAvailableWidth();

                foreach (var line in orderlines)
                {
                    Cell productNameCell = new Cell().Add(new Paragraph(line.ProductName));
                    table.AddCell(productNameCell);
                }

                document.Add(table);

                Paragraph total = new Paragraph($"Suma: {order.Price} €")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBold();
                document.Add(total);

                document.Close();

                _response.Result = path;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }













    }
}

using iText.Bouncycastle.Crypto;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SvarosNamai.Serivce.InvoiceAPI.Service.IService;
using SvarosNamai.Service.InvoiceAPI.Data;
using SvarosNamai.Service.InvoiceAPI.Models;
using SvarosNamai.Service.InvoiceAPI.Models.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SvarosNamai.Service.InvoiceAPI.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IErrorLogger _error;

        public InvoiceController(AppDbContext db, IErrorLogger error)
        {
            _db = db;
            _response = new ResponseDto();
            _error = error;
        }

        [HttpPost("GenerateInvoice")]
        public async Task<ResponseDto> GenerateInvoice(OrderForInvoiceDto order)
        {
            try
            {
                if(_db.Invoices.Any(u => u.OrderId == order.OrderId))
                {
                    throw new Exception("Invoice already generated for this orderId");
                }

                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filepath = Path.Combine(directoryPath, $"{order.OrderId}_{order.Street} {order.HouseNo}{order.HouseLetter} {order.ApartmentNo}" + ".pdf");



                PdfWriter writer = new PdfWriter(filepath);
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
                    .Add("Užsakymo nr. : ").Add($"{order.OrderId}").Add("\n")
                    .Add("Klientas: ").Add($"{order.Name} {order.LastName}").Add("\n")
                    .Add("Adresas: ").Add($"{order.Street} {order.HouseNo}{order.ApartmentNo}{order.HouseLetter}, {order.City}").Add("\n")
                    .Add("El.pašto adresas: ").Add($"{order.Email}").Add("\n")
                    .Add("Telefono nr.: ").Add($"{order.PhoneNumber}");


                Paragraph companyDetails = new Paragraph()
                    .AddTabStops(new TabStop(100, TabAlignment.RIGHT))
                    .Add("Pavadinimas: ").Add("MB \"Švaros Namai\"").Add("\n")
                    .Add("Adresas: ").Add("Taikos g. 13, Vilnius").Add("\n")
                    .Add("El.pašto adresas: ").Add("greta@svarosnamai.lt").Add("\n")
                    .Add("Telefono nr.: ").Add("+370 648 (71)806")
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

                
                Table table = new Table(2)
                    .UseAllAvailableWidth();

                
                Cell productNameHeaderCell = new Cell().Add(new Paragraph("Pavadinimas").SetBold());
                Cell productPriceHeaderCell = new Cell().Add(new Paragraph("Kaina").SetBold());
                table.AddHeaderCell(productNameHeaderCell);
                table.AddHeaderCell(productPriceHeaderCell);

                foreach (var line in order.Lines)
                {
                    Cell productNameCell = new Cell().Add(new Paragraph(line.ProductName));
                    Cell productPriceCell = new Cell().Add(new Paragraph($"{line.Price.ToString()} €"));
                    table.AddCell(productNameCell);
                    table.AddCell(productPriceCell);
                }

                document.Add(table);

                Paragraph total = new Paragraph($"Suma: {order.Lines.Sum(line => line.Price)} €")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBold();
                document.Add(total);

                document.Close();

                Invoice invoice = new Invoice()
                {
                    OrderId = order.OrderId,
                    InvoiceName = $"{order.OrderId}_{order.Street} {order.HouseNo}{order.HouseLetter} {order.ApartmentNo}",
                    Path = filepath
                };
                
                await _db.Invoices.AddAsync(invoice);
                await _db.SaveChangesAsync();

                byte[] bytes = System.IO.File.ReadAllBytes(filepath);


                _response.Result = JsonConvert.SerializeObject(bytes);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _error.LogError(_response.Message);
            }
            return _response;
        }


        [HttpGet("DownloadInvoice/{orderId}")]
        public async Task<IActionResult> DownloadInvoice(int orderId)
        {
            try
            {

                var invoice = _db.Invoices.Where(u => u.OrderId == orderId).FirstOrDefault();
                if(invoice != null)
                {
                return PhysicalFile(invoice.Path, "application/octet-stream", $"{invoice.InvoiceName}.pdf");
                }
                else
                {
                    throw new Exception("Invoice doesn't exist");
                }

               

            }
            catch(Exception ex)
            {
                _error.LogError(_response.Message);
                return new NotFoundObjectResult(ex.Message);
            }
        }
    }
}


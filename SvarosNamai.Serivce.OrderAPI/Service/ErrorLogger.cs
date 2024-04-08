
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SvarosNamai.Serivce.OrderAPI.Service.IService;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SvarosNamai.Serivce.OrderAPI.Service
{
    public class ErrorLogger : IErrorLogger
    {
        public async Task<IActionResult> LogError(string log)
        {
            log = $"{DateTime.Now} {log}";
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Errors");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, $"Errors{DateTime.Today.ToString("yyyy-MM-dd")}.txt");


            using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {

                var logWithNewLine = $"{log}{Environment.NewLine}";
                var bytes = Encoding.UTF8.GetBytes(logWithNewLine);
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }

            return new OkResult();

        }
    }
}

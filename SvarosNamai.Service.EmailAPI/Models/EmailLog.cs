using System.ComponentModel.DataAnnotations;

namespace SvarosNamai.Service.EmailAPI.Models
{
    public class EmailLog
    {
        [Key]
        public int LogId { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public bool WasSent { get; set; }
        public bool HadAttachment { get; set; }
        public DateTime Time {  get; set; }
    }
}

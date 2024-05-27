namespace SvarosNamai.Web.Models
{
    public class AvailableTimeSlotsDto
    {
        public string DayName { get; set; }
        public DateOnly DayDate {  get; set; }
        public int OpenSlots { get; set; }
        public int OrderCount { get; set; }
        public int AvailableSlots { get; set; } 

    }
}

namespace LecturerClaimSystem.Models
{
    public class Module
    {
        //For modules
        public int Id { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string Programme { get; set; }
        public int NumberOfHOurs { get; set; }
        public int NumberOfHours { get; internal set; }
        public decimal HourlyRate { get; set; }
        public decimal Payment => NumberOfHOurs * HourlyRate; // Computed property for the payment of a single module

    }
}

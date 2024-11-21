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
        public decimal HourlyRate { get; set; }

        // Computed property for the payment of a single module
        public decimal Payment => NumberOfHOurs * HourlyRate;

    }
}

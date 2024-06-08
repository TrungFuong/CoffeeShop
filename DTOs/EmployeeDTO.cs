namespace CoffeeShop.DTOs
{
    public class EmployeeRequestDTO
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public int EmployeeWorkingHour { get; set; }
        public Guid AccountId { get; set; }
    }
    public class EmployeeResponseDTO
    {
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public int EmployeeWorkingHour { get; set; }
        public Guid AccountId { get; set; }
    }
}

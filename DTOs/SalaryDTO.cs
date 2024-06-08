namespace CoffeeShop.DTOs
{
    public class SalaryRequestDTO
    {
        public Guid EmployeeId { get; set; }
        public int PayrateId { get; set; }
        public decimal TotalSalary { get; set; }
    }
    public class SalaryResponseDTO {
        public int SalaryId { get; set; }   
        public Guid EmployeeId { get; set; }
        public int PayrateId { get; set; }
        public decimal TotalSalary { get; set; }
    }

}

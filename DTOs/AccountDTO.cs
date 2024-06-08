namespace CoffeeShop.DTOs
{
    public class AccountRequestDTO
    {
        public Guid AccountId { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public int RoleId { get; set; }
    }
    public class AccountResponseDTO
    {
        public string AccountUsername { get; set; }
        public int RoleId { get; set; }
    }
}

namespace RoleBasedAuthenticationBackend.DTOs
{
    public class DateWiseStockReportDto
    {
        public string ProductName { get; set; }
        public int OpeningStock { get; set; }
        public int TotalSoldQuantity { get; set; }
        public int ClosingStock { get; set; }
    }
}

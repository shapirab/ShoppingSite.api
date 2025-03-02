namespace ShoppingSite.api.Data.DataModels.DTO
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}

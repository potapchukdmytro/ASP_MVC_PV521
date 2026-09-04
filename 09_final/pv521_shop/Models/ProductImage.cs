namespace _01_intro.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsPreview { get; set; } = false;

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}

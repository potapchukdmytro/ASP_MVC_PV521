using _01_intro.Models;

namespace _01_intro.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Preview { get; set; }
        public int Amount { get; set; }
        public Category? Category { get; set; }
    }
}
using _01_intro.Models;

namespace _01_intro.ViewModels
{
    public class CatalogVM
    {
        public IEnumerable<ProductVM> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
        public int Page { get; set; } = 1;
        public int LastPage { get; set; } = 1;
        public string? Category { get; set; } = null;
        public int[] Pages { get; set; } = [1];
    }
}

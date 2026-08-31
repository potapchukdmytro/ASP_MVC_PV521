using _01_intro.Models;

namespace _01_intro.ViewModels
{
    public class CatalogVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public int page = 1;
        public int pages = 1;
    }
}

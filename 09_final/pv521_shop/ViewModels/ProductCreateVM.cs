using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace _01_intro.ViewModels
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Назва товару обов'язкова")]
        public string Name { get; set; } = string.Empty;
        [Range(0.0, double.MaxValue, ErrorMessage = "Ціна повинна бути додатньою")]
        public double Price { get; set; }
        public string? Description { get; set; }

        public IFormFile? Preview { get; set; }
        public List<IFormFile> Images { get; set; } = [];

        [Range(0, int.MaxValue, ErrorMessage = "Кількість повинна бути додатньою")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Категорія обов'язкова")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = [];
    }
}

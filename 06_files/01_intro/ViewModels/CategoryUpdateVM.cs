using System.ComponentModel.DataAnnotations;

namespace _01_intro.ViewModels
{
    public class CategoryUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [MaxLength(50, ErrorMessage = "Максимальна довжина 50 символів")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [MaxLength(255, ErrorMessage = "Максимальна довжина 255 символів")]
        public string? Image { get; set; }
    }
}

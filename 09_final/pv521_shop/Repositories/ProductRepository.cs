using _01_intro.Data;
using _01_intro.Models;
using _01_intro.Services;
using _01_intro.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _01_intro.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;
        private readonly string _imagesPath;

        public ProductRepository(ApplicationDbContext context, ImageService imageService, IWebHostEnvironment env)
        {
            _context = context;
            _imageService = imageService;
            _imagesPath = Path.Combine(env.WebRootPath, "images", "products");
        }

        public IQueryable<Product> Products => _context.Products.AsNoTracking();

        public async Task<string?> CreateAsync(ProductCreateVM viewModel)
        {
            var model = new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId,
                Amount = viewModel.Amount
            };

            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();

            string folderPath = Path.Combine(_imagesPath, model.Id.ToString());

            if (viewModel.Preview != null)
            {
                string? previewName = await _imageService.SaveImageAsync(viewModel.Preview, folderPath);
                if (!string.IsNullOrEmpty(previewName))
                {
                    model.Images.Add(new ProductImage { Name = previewName, IsPreview = true });
                }
            }

            if(viewModel.Images.Count > 0)
            {
                var names = await _imageService.SaveImagesAsync(viewModel.Images, folderPath);

                var images = names
                    .Where(n => n != null)
                    .Select(n => new ProductImage { Name = n!, IsPreview = false });
                model.Images.AddRange(images);
            }

            await _context.SaveChangesAsync();

            return null;
        }
    }
}

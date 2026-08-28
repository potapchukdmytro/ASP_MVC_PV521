namespace _01_intro.Services
{
    public class ImageService
    {
        public async Task<string?> SaveImageAsync(IFormFile image, string dirPath)
        {
            var types = image.ContentType.Split("/");

            if (types.Length != 2 || types[0] != "image")
            {
                return null;
            }

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            // string ext = types[1];
            string ext = Path.GetExtension(image.FileName);
            string imageName = $"{Guid.NewGuid()}{ext}";
            string imagePath = Path.Combine(dirPath, imageName);

            // Через 2 потоки
            //using (var fileStream = new FileStream(imagePath, FileMode.CreateNew))
            //{
            //    using (var imageStream = vm.Image.OpenReadStream())
            //    {
            //        imageStream.CopyTo(fileStream);
            //    }
            //}

            // Через CopyTo
            using (var fileStream = new FileStream(imagePath, FileMode.CreateNew))
            {
                await image.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public async Task<IEnumerable<string?>> SaveImagesAsync(IEnumerable<IFormFile> images, string dirPath)
        {
            var tasks = new List<Task<string?>>();

            foreach(var image in images)
            {
                tasks.Add(SaveImageAsync(image, dirPath));
            }

            var res = await Task.WhenAll(tasks);

            return res ?? [];
        }
    }
}

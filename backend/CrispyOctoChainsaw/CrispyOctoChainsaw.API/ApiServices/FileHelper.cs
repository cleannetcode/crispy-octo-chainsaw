using CSharpFunctionalExtensions;

namespace CrispyOctoChainsaw.API.ApiServices
{
    public class FileHelper
    {
        public static Result<bool> CompareFileNames(string pathToDirectory, string fileName)
        {
            if (!Directory.Exists(pathToDirectory))
            {
                return Result.Failure<bool>("Directory is not exist.");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return Result.Failure<bool>("File name is null is null or white space.");
            }

            var fileNames = new DirectoryInfo(pathToDirectory).GetFiles().Select(x => x.Name);
            var result = fileNames.FirstOrDefault(x => x == fileName);
            return result != null ? true : false;
        }

        public static async Task<Result<string>> SaveFile(string pathToDirectory, IFormFile file)
        {
            if (file == null)
            {
                return Result.Failure<string>("File is null");
            }

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var imagePath = Path.Combine(pathToDirectory, imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}

using AutoFixture;
using CrispyOctoChainsaw.API.ApiServices;
using Microsoft.AspNetCore.Http;

namespace CrispyOctoChainsaw.UnitTests
{
    public class FileHelperTests : IAsyncLifetime
    {
        private readonly Fixture _fixture;
        private readonly string _contentRootPath;
        private readonly string _directoryName;
        private readonly string _pathToDirectory;

        public FileHelperTests()
        {
            _fixture = new Fixture();
            _directoryName = @"Images";
            var baseDirectory = AppContext.BaseDirectory;
            _contentRootPath = @$"{Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName}/";
            _pathToDirectory = _contentRootPath + _directoryName;
        }

        [Theory]
        [InlineData("13858463-ee98-4327-949a-609722d75d77.jpg")]
        public async Task Compare_file_name_from_request_with_file_name_from_api_when_files_equals(string fileNameFromApi)
        {
            // arrange
            var fileNameFromRequest = fileNameFromApi;

            // act
            var result = FileHelper.CompareFileNames(_pathToDirectory, fileNameFromRequest);

            // assert
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Compare_file_name_from_request_with_file_name_from_api_when_files_not_equals()
        {
            // arrange
            var fileNameFromRequest = _fixture.Create<Guid>().ToString() + ".jpg";

            // act
            var result = FileHelper.CompareFileNames(_pathToDirectory, fileNameFromRequest);

            // assert
            Assert.False(result.Value);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Compare_file_name_from_request_with_file_name_from_api_when_directory_is_not_exist(
            string pathToDirectory)
        {
            // arrange
            var fileNameFromRequest = _fixture.Create<Guid>().ToString() + ".jpg";

            // act
            var result = FileHelper.CompareFileNames(pathToDirectory, fileNameFromRequest);

            // assert
            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Compare_file_name_from_request_with_file_name_from_api_when_fileName_is_not_invalid(
            string fileName)
        {
            // arrange
            // act
            var result = FileHelper.CompareFileNames(_pathToDirectory, fileName);

            // assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Save_file_when_file_is_not_null()
        {
            // arrange
            var images = new DirectoryInfo(_pathToDirectory).GetFiles();
            var imageName = images[0].Name;
            var path = _pathToDirectory + @$"/{imageName}";
            using var stream = new MemoryStream(File.ReadAllBytes(path));
            var fromFile = new FormFile(stream, 0, stream.Length, "fromFile", "fromFile.jpg");

            // act
            var result = await FileHelper.SaveFile(_pathToDirectory, fromFile);
            var imagesAfterAddNewImage = new DirectoryInfo(_pathToDirectory).GetFiles();

            // assert;
            Assert.NotNull(result.Value);
            Assert.False(result.IsFailure);
            Assert.True(imagesAfterAddNewImage.Length > 1);
        }

        [Fact]
        public async Task Save_file_when_file_is_null()
        {
            // arrange
            // act
            var result = await FileHelper.SaveFile(_pathToDirectory, null);

            // assert
            Assert.True(result.IsFailure);
        }

        public async Task DisposeAsync()
        {
            await ClearFileDirectory(_pathToDirectory);
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        private async Task ClearFileDirectory(string pathToDirectory)
        {
            var files = new DirectoryInfo(pathToDirectory).GetFiles();
            foreach (var file in files)
            {
                if (file != files[0])
                {
                    file.Delete();
                }
            }
        }
    }
}

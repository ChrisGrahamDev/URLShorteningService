using URLShorteningService.DataAccess;
using URLShorteningService.Services.Interfaces;

namespace URLShorteningService2Test.ServiceTests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class FileSystemDataAccessTests
    {
        private Mock<IFileReaderService> _fileReaderService;
        private Mock<IFileUpdaterService> _fileUpdaterService;
        private Mock<ITokenGeneratorService> _tokenGeneratorService;
        private FileSystemDataAccess _fileSystemDataAccess;

        [SetUp]
        public void Setup()
        {
            _fileReaderService = new Mock<IFileReaderService>();
            _fileUpdaterService = new Mock<IFileUpdaterService>();
            _tokenGeneratorService = new Mock<ITokenGeneratorService>();
        }


        #region GetShortUrlTests

        [TestCase("shortUrl", "valid")]
        public void GetShortUrl_Returns_ValidShortUrl(string url, string valid)
        {
            (bool, string) test = (true, valid);

            _fileReaderService.Setup(x => x.CheckShortUrlExistsInFile(url)).Returns(test);

            _fileSystemDataAccess =
                new FileSystemDataAccess(_fileUpdaterService.Object, _fileReaderService.Object, _tokenGeneratorService.Object);

            string res = _fileSystemDataAccess.GetShortUrl(url);

            Assert.IsNotNull(res);
            Assert.That(res == valid);
        }

        [TestCase("shortUrl", "token")]
        public void GetShortUrl_Creates_And_Returns_ValidShortUrl(string url, string valid)
        {
            (bool, string) test = (false, valid);

            _fileReaderService.Setup(x => x.CheckShortUrlExistsInFile(url)).Returns(test);
            _tokenGeneratorService.Setup(x => x.GenerateToken()).Returns("token");
            _fileUpdaterService.Setup(x => x.CreateShortUrl("token", url));

            _fileSystemDataAccess =
                new FileSystemDataAccess(_fileUpdaterService.Object, _fileReaderService.Object, _tokenGeneratorService.Object);

            string res = _fileSystemDataAccess.GetShortUrl(url);

            Assert.IsNotNull(res);
            Assert.That(res == valid);
        }

        #endregion

    }
}
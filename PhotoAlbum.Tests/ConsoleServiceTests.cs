using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;

namespace PhotoAlbum.Tests;

[TestClass]
public class ConsoleServiceTests
{
    private ConsoleService _sut;
    private Mock<IPhotoServices> _mockedPhotoServices;
    private Mock<IConsoleIO> _mockedConsoleIO;
    private List<Photos> expectedResults;
    private string userInput; 

    [TestInitialize]
    public void BeforeEach()
    {
        expectedResults = new List<Photos>()
        {
            new Photos()
            {
                Id = 1,
                AlbumId = 1,
                URL = "www.test.com",
                thumbnailURL = "www.testthumbnail.com",
                Title = "photo 1"
                    
            },
            new Photos()
            {
                Id = 2,
                AlbumId = 1,
                URL = "www.test.com",
                thumbnailURL = "www.testthumbnail.com",
                Title = "photo 2"
            },
            new Photos()
            {
                Id = 3,
                AlbumId = 2,
                URL = "www.test.com",
                thumbnailURL = "www.testthumbnail.com",
                Title = "photo 3"
            }
        };
        userInput = "1";
        _mockedPhotoServices = new Mock<IPhotoServices>();
        _mockedConsoleIO = new Mock<IConsoleIO>();
        _mockedPhotoServices.Setup(x => x.GetPhotos(It.IsAny<int>())).ReturnsAsync(expectedResults);
        _mockedConsoleIO.Setup(x => x.ReadLine()).Returns(userInput.ToString);
        _sut = new ConsoleService(_mockedPhotoServices.Object, _mockedConsoleIO.Object);
    }

    [TestMethod]
    public void ConsoleServiceGetPhotosShouldCallPhotoServiceToGetPhotos()
    {
        _sut.GetPhotos(1);
        
        _mockedPhotoServices.Verify(x => x.GetPhotos(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public void WhenAppStartsThenConsoleShouldWriteAPrompt()
    {
        _sut.StartApp();
        _mockedConsoleIO.Verify(x =>x.WriteLine("Enter an Album Id to retrieve, or press Q to quit: "), Times.Once);
    }

    [TestMethod]
    public void WhenUserEntersOneThenGetPhotosIsCalledWithAOne()
    {
        _sut.StartApp();
        _mockedPhotoServices.Verify(x => x.GetPhotos(1), Times.Once);
    }
}
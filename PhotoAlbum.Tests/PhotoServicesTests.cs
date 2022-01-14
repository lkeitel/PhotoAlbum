using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;

namespace PhotoAlbum.Tests;

[TestClass]
public class PhotoServicesTests
{
    private PhotoServices _sut;
    private List<Photos> expectedResults;
    private Mock<IApiClient> mockApiClient;

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

        mockApiClient = new Mock<IApiClient>();

        mockApiClient.Setup(x => x.Get<List<Photos>>(It.IsAny<string>())).Returns(Task.FromResult(expectedResults));
        _sut = new PhotoServices(mockApiClient.Object);
    }
        
    [TestMethod]
    public void GetPhotosShouldGetListOfPhotosFromClient()
    {
        var results = _sut.GetPhotos(null).Result;
        results.Should().BeEquivalentTo(expectedResults);
        mockApiClient.Verify(x => x.Get<List<Photos>>("https://jsonplaceholder.typicode.com/photos"));
    }
        
    [TestMethod]
    public void GetPhotosWithAlbumModifierShouldGetListOfPhotosFromAlbum()
    {
        var expectedAlbumId = 1;
        var filteredResults = expectedResults.Where(x => x.AlbumId == expectedAlbumId).ToList();
        mockApiClient.Setup(x => x.Get<List<Photos>>($"https://jsonplaceholder.typicode.com/photos?albumId={expectedAlbumId}")).Returns(Task.FromResult(filteredResults));
        var results = _sut.GetPhotos(expectedAlbumId).Result;
        results.Should().BeEquivalentTo(filteredResults);
        mockApiClient.Verify(x => x.Get<List<Photos>>($"https://jsonplaceholder.typicode.com/photos?albumId={expectedAlbumId}"));
    }
}
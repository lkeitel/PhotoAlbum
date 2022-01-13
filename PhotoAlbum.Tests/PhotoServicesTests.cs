using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using PhotoAlbum.Models;

namespace PhotoAlbum.Tests
{
    [TestClass]
    public class PhotoServicesTests
    {
        private PhotoServices _sut;
        private List<Photos> expectedResults;
        private Mock<HttpMessageHandler> mockHttpMessageHandler;

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
            
            mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    HttpResponseMessage response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(JsonSerializer.Serialize(expectedResults))
                    };

                    return response;
                });
            HttpClient client = new HttpClient(mockHttpMessageHandler.Object);
            _sut = new PhotoServices(client);
        }
        
        [TestMethod]
        public void GetPhotosShouldGetListOfPhotosFromClient()
        {
            var results = _sut.GetPhotos().Result;
            results.Should().BeEquivalentTo(expectedResults);
        }
    }
}
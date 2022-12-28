using CRON.Api.Controllers.V1;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Test
{
    public class IndexTest
    {
        private IndexController _controller { get; }
        public IndexTest()
        {
            _controller = new IndexController();
        }

        [Fact]
        public void Get_ReturnsOk()
        {
            // Act
            var okResult =  _controller.Message() as OkObjectResult;

            // Assert
            var message = okResult.Value as string;
            Assert.Equal("Fullstack Challenge 20201026", message);
        }
    }
}

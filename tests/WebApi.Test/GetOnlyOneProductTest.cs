using CRON.Api.Controllers.V1;
using CRON.Application.DTO.Response;
using CRON.Domain.Entities;
using CRON.Services.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TestTools.FakerBuilder;

namespace WebApi.Test;

public class GetOnlyOneProductTest
{
    private ProductsController _controller { get; }
    private IReadOnlyProductService _service { get; }
    public GetOnlyOneProductTest()
    {
        _service = Substitute.For<IReadOnlyProductService>();
        _controller = new ProductsController(_service);
    }


    [Fact]
    public async Task Get_CodeValid_ReturnsOk()
    {
        // Arrange
        var product = ProductFakerBuilder.Builder();
        _service.GetProductByCode(product.Code).Returns(product);

         // Act
        var okResult = await _controller.Get(product.Code) as OkObjectResult;

        // Assert
        Assert.IsType<Product>(okResult.Value);
        Assert.Equal(product.Code, (okResult.Value as Product).Code);
    }


    [Theory]
    [InlineData("e343412")]
    [InlineData("dhashd5343v")]
    [InlineData("e343412ff")]
    public async Task Get_InvalidCode_ReturnsBad(string code)
    {
        // Act
        var okResult = await _controller.Get(code) as BadRequestObjectResult;

        // Assert
        Assert.IsType<ResponseError>(okResult.Value);

        var result = okResult.Value as ResponseError;
        Assert.Single(result.Errors); // verify if have only one item
    }

    [Fact]
    public async Task Get_ProductNotFound_ReturnsNotFound()
    {
        var code = "312312312456";
        // Arrange
        _service.GetProductByCode(code).ReturnsNull();

        // Act
        var okResult = await _controller.Get(code) as NotFoundObjectResult;

        // Assert
        Assert.IsType<ResponseError>(okResult.Value);
        var result = okResult.Value as ResponseError;
        Assert.Single(result.Errors); // verify if have only one item
    }
}
using CRON.Api.Controllers.V1;
using CRON.Application.DTO.Request;
using CRON.Application.DTO.Response;
using CRON.Domain.Entities;
using CRON.Services.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TestTools.FakerBuilder;

namespace WebApi.Test;
public class GetProductPageTest
{
    private ProductsController _controller { get; }
    private IReadOnlyProductService _service { get; }
    public GetProductPageTest()
    {
        _service = Substitute.For<IReadOnlyProductService>();
        _controller = new ProductsController(_service);
    }

    [Fact]
    public async Task Get_DefaultPage_ReturnsOk()
    {
        // Arrange
        // Default Values
        var pagination = new ProductFilterRequest
        {
            Page = 1,
            PageSize = 25
        };
        var list = ProductsListFakerBuilder.Builder(); // Lista de produtos
        var paginationReponse = new ProductsPaginationFilter(list.Count(), pagination.Page, pagination.PageSize);
        paginationReponse.Products = list;

        _service.GetProducts(pagination.Page, pagination.PageSize).Returns(paginationReponse);

        // Act
        var okResult = await _controller.GetByNumberPage(pagination) as OkObjectResult;

        // Assert
        Assert.IsType<ProductsPaginationFilter>(okResult.Value);
        Assert.Equal(paginationReponse.Products, (okResult.Value as ProductsPaginationFilter).Products);
    }


    [Fact]
    public async Task Get_ProductsNull_ReturnsNotFound()
    {
        // Arrange
        // Default Values
        var pagination = new ProductFilterRequest
        {
            Page = 1,
            PageSize = 25
        };
        var list = ProductsListFakerBuilder.Builder(); // Lista de produtos
        var paginationReponse = new ProductsPaginationFilter(list.Count(), pagination.Page, pagination.PageSize);
        paginationReponse.Products = null;

        _service.GetProducts(pagination.Page, pagination.PageSize).Returns(paginationReponse);

        // Act
        var okResult = await _controller.GetByNumberPage(pagination) as NotFoundObjectResult;

        var page = okResult.Value as ProductsPaginationFilter;

        // Assert
        Assert.IsType<ProductsPaginationFilter>(okResult.Value);
        Assert.Null(paginationReponse.Products);
    }
}


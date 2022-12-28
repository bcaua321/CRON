using CRON.Application.Constants;
using CRON.Application.DTO.Request;
using CRON.Application.DTO.Response;
using CRON.Domain.Entities;
using CRON.Services.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CRON.Api.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}")]
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    private IReadOnlyProductService ProductService { get; set; }
    public ProductsController(IReadOnlyProductService productService)
    {
        ProductService = productService;
    }

    [HttpGet("products/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseError))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseError))]
    public async Task<IActionResult> Get(string code)
    {
        if (Regex.IsMatch(code, @"\D+"))
            return BadRequest(new ResponseError(ResponseErrorsMessages.InvalidParameter));

        var result = await ProductService.GetProductByCode(code);

        if (result is null)
            return NotFound(new ResponseError(ResponseErrorsMessages.ProductDontExist));

        return Ok(result);
    }

    [HttpGet("products/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductsPaginationFilter))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProductsPaginationFilter))]
    public async Task<IActionResult> GetByNumberPage([FromQuery] ProductFilterRequest filter)
    {
        var result = await ProductService.GetProducts(filter.Page, filter.PageSize);

        if (result.Products is null)
            return NotFound(result);

        return Ok(result);
    }
}

using CRON.Domain.Entities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CRON.Application.DTO.Response;
public class ProductsPaginationFilter
{
    [JsonIgnore]
    public int Page { get; set; } = 1;

    [JsonIgnore]
    public int PageSize { get; set; } = 25;

    [DisplayName("current_page")]
    public int CurrentPage { get; set; }

    [DisplayName("total_pages")]
    public int TotalPages { get; set; }

    [DisplayName("total_count")]
    public int TotalCount { get; set; }

    [DisplayName("has_previous")]
    public bool HasPrevious { get; set; }

    [DisplayName("has_next")]
    public bool HasNext { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [DisplayName("products")]
    public List<Product> Products { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [DisplayName("errors")]
    public List<string> Errors { get; set; }

    public ProductsPaginationFilter(int totalProducts, int page, int pageSize)
    {
        PageSize = pageSize > PageSize ? PageSize : pageSize;
        Page = page < Page ? Page : page;
        TotalCount = totalProducts;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        CurrentPage = page;
    }

    public void AddError(string message)
    {
        if (Errors is null)
            Errors = new List<string>();
        Errors.Add(message);
    }
}
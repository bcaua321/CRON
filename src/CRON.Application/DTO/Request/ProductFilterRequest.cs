namespace CRON.Application.DTO.Request;
public class ProductFilterRequest
{
    // Not the same layout like Products Pagination Filter, because it's for query params
    public ProductFilterRequest()
    {
        Page = 1;
        PageSize = 25;
    }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
namespace CRON.Application.Intefaces.Scraping
{
    public interface IProductUrlScraping
    {
        List<string> GetProductsUrls(string url);
    }
}

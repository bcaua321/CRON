using CRON.Application.Intefaces.Scraping;
using HtmlAgilityPack;

namespace CRON.Scraping.ProductScraping;
public class ProductUrlScraping : IProductUrlScraping
{
    // Get all products urls from main page
    public List<string> GetProductsUrls(string url)
    {
        var web = new HtmlWeb();
        var htmlDocument = web.Load(url);

        // Take 100 produts links
        var anchorNodes = htmlDocument.DocumentNode.SelectNodes("//a[starts-with(@href, '/product')]").Take(100);

        if (anchorNodes is null)
            return null;

        return anchorNodes.Select(a => a.Attributes["href"].Value).ToList();
    }
}
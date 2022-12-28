using CRON.Domain.Entities;
using HtmlAgilityPack;

namespace CRON.Application.Intefaces.Scraping;
public interface IProductInfomationScraping
{
    void ProductsInfoBuilder(HtmlDocument htmlDocument, Product product);
}

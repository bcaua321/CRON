using CRON.Application.Intefaces.Scraping;
using CRON.Domain.Entities;
using HtmlAgilityPack;

namespace CRON.Scraping.ProductScraping;
public class ProductInfomartionScraping : IProductInfomationScraping
{
    public void ProductsInfoBuilder(HtmlDocument htmlDocument, Product product)
    {
        GetName(htmlDocument, product);
        GetQuantity(htmlDocument, product);
        GetCategories(htmlDocument, product);
        GetPackaging(htmlDocument, product);
        GetBrands(htmlDocument, product);
        GetUrlImage(htmlDocument, product);
        GetProductCodes(htmlDocument, product);
    }

    // Name taken from the title, because some products that do not have the name in the description
    private void GetName(HtmlDocument htmlDocument, Product product)
    {
        var nameNode = GetNodeByPattern(htmlDocument, "//h2[@property='food:name']");
        var name = nameNode.InnerText.Split("-");
        product.ProductName = name[0];
    }
    private void GetQuantity(HtmlDocument htmlDocument, Product product)
    {
        var quantityNode = GetNodeByPattern(htmlDocument, "//span[@id='field_quantity_value']");

        if (quantityNode is null)
        {
            product.Quantity = null;
            return;
        }

        product.Quantity = quantityNode.InnerText;
    }

    private void GetCategories(HtmlDocument htmlDocument, Product product)
    {
        var categoriesNode = GetNodeByPattern(htmlDocument, "//span[@id='field_categories_value']");

        if (categoriesNode is null)
        {
            product.Categories = null;
            return;
        }

        var categories = categoriesNode.Descendants("a").Select(x => x.InnerText).ToList();
        product.Categories = string.Join(", ", categories);
    }

    private void GetPackaging(HtmlDocument htmlDocument, Product product)
    {
        var packagingNode = GetNodeByPattern(htmlDocument, "//span[@id='field_packaging_value']");

        if (packagingNode is null)
        {
            product.Packaging = null;
            return;
        }

        var packaging = packagingNode.Descendants("a").Select(x => x.InnerText).ToList();
        product.Packaging = string.Join(", ", packaging);
    }

    private void GetBrands(HtmlDocument htmlDocument, Product product)
    {
        var brandsNode = GetNodeByPattern(htmlDocument, "//span[@id='field_brands_value']");

        if (brandsNode is null)
        {
            product.Packaging = null;
            return;
        }

        var brands = brandsNode.Descendants("a").Select(x => x.InnerText).ToList();
        product.Brands = string.Join(", ", brands);
    }

    public void GetUrlImage(HtmlDocument htmlDocument, Product product)
    {
        var imgNode = GetNodeByPattern(htmlDocument, "//img[@id='og_image']");

        if (imgNode is null)
        {
            product.ImageUrl = null;
            return;
        }

        var url = imgNode.Attributes["src"].Value;
        product.ImageUrl = url;
    }

    public void GetProductCodes(HtmlDocument htmlDocument, Product product)
    {
        var codeNode = htmlDocument.DocumentNode.SelectSingleNode("//p[@id='barcode_paragraph']");

        var code = codeNode.SelectSingleNode("//span[@id='barcode']").InnerText.ReplaceLineEndings().Trim();
        var barCodeExtension = GetExtensionFromBarcode(codeNode).ReplaceLineEndings().Trim();

        product.Code = code;
        product.BarCode = $"{code}{barCodeExtension}";
    }
    private string GetExtensionFromBarcode(HtmlNode codeElement)
    {
        var codeExtension = codeElement.LastChild.InnerText;
        return codeExtension;
    }

    private HtmlNode GetNodeByPattern(HtmlDocument htmlDocument, string pattern)
    {
        var productName = htmlDocument.DocumentNode.SelectSingleNode(pattern);
        return productName;
    }
}
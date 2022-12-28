using Bogus;
using CRON.Application.Constants;
using CRON.Domain.Entities;

namespace TestTools.FakerBuilder;
public class ProductFakerBuilder
{
    // Create a faker product
    public static Product Builder()
    {
        Random next = new Random();
        // 3017620422003
        var code = next.NextInt64(1, 100000000000);

        var product = new Faker<Product>()
            .RuleFor(x => x.Id, x => x.Random.Int(1, 100))
            .RuleFor(x => x.ProductName, x => x.Lorem.Word())
            .RuleFor(x => x.Brands, x => x.Random.Word())
            .RuleFor(x => x.Packaging, x => x.Lorem.Word())
            .RuleFor(x => x.Status, ProductStatus.Imported)
            .RuleFor(x => x.Categories, x => x.Lorem.Word())
            .RuleFor(x => x.Code, x => code.ToString())
            .RuleFor(x => x.Quantity, x => x.Random.Word())
            .RuleFor(x => x.Url, x => x.Internet.Url())
            .RuleFor(x => x.BarCode, x => $"{code} {x.Random.Word()}")
            .RuleFor(x => x.ImageUrl, x => x.Internet.Url());

        return product;
    }
}

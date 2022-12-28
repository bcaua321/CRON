using CRON.Domain.Entities;

namespace TestTools.FakerBuilder;
public class ProductsListFakerBuilder
{
    // Create a fake list of products
    public static List<Product> Builder()
    {
        var result = new List<Product>();
        for(int i = 1; i < 100; i++)
        {
            result.Add(ProductFakerBuilder.Builder());
        }

        return result;
    }
}
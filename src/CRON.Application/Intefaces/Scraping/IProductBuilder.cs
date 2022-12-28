using CRON.Domain.Entities;

namespace CRON.Application.Intefaces.Scraping
{
    public interface IProductBuilder
    {
        Task<List<Product>> GetProducts();
    }
}

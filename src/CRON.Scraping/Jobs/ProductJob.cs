using CRON.Application.Intefaces.Email;
using CRON.Application.Intefaces.Scraping;
using CRON.Domain.Entities;
using CRON.Domain.Repositories;
using Quartz;

namespace CRON.Scraping.Jobs;
public class ProductJob : IJob
{
    private IRegisterOnlyProductRepository ProductRepository { get; set; }
    private IProductBuilder ProductBuilder { get; set; }
    private IEmailSend EmailSend { get; set; }
    public ProductJob(IProductBuilder productBuilder, IRegisterOnlyProductRepository productRepository, IEmailSend emailSend)
    {
        ProductBuilder = productBuilder;
        ProductRepository = productRepository;
        EmailSend = emailSend;
    }

    // Method called by Sheduler in the configured time 
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Job Iniciado");

        try
        {
            var products = await ProductBuilder.GetProducts();
            await InsertProducts(products);
        } 
        catch
        {
            Console.WriteLine("Erro ao executar o job");
            await EmailSend.SendEmail("Erro de sincronização", $"Erro na execução do Job Scraping de Produtos, Data:{DateTime.Now}.");
        }

        Console.WriteLine("Job Finalizado");
    }

    public async Task InsertProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            var productExist = await ProductRepository.ExitsProductQuartz(product.Code);

            if (!productExist)
            {
                await AddProduct(product);
                continue;
            }

            await UpdateProduct(product);
        }
    }

    public async Task AddProduct(Product product)
    {
        try
        {
            await ProductRepository.RegisterProduct(product);
        }
        catch
        {
            await EmailSend.SendEmail("Erro ao adicionar produto", $"Erro na execução ao atualizar o Produto com o code {product.Code}, Data: {DateTime.Now}.");
        }
    }

    public async Task UpdateProduct(Product product)
    {
        try
        {
            await ProductRepository.UpdateProduct(product);
        }
        catch 
        {
            await EmailSend.SendEmail("Erro ao atualizar produto", $"Erro na execução ao atualizar o Produto com o code {product.Code}, Data: {DateTime.Now}.");
        }
    }
}
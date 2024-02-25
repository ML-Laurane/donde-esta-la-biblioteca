using BusinessLayer.Catalog;
using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    private static IHost CreateHostBuilder(IConfigurationBuilder configuration)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddScoped<ICatalogService, CatalogService>();
                services.AddScoped<ICatalogManager, CatalogManager>();

                services.AddScoped<IGenericRepository<Book>, BookRepository>();
                services.AddScoped<IGenericRepository<Author>, AuthorRepository>();
                services.AddScoped<IGenericRepository<Library>, LibraryRepository>();

                services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseSqlite("Data Source=.\\ressources\\Library.db;")
                });
            })
            .Build();
    }
}
using BusinessLayer.Catalog;
using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Services.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("LibraryDatabase")));

        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<ICatalogManager, CatalogManager>();
        builder.Services.AddScoped<IGenericRepository<Book>, BookRepository>();
        builder.Services.AddScoped<IGenericRepository<Author>, AuthorRepository>();
        builder.Services.AddScoped<IGenericRepository<Library>, LibraryRepository>();

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
using BusinessObjects.Entity;
using BusinessObjects.Enums;
using BusinessLayer.Catalog;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using DataAccessLayer.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManager.Hosting.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        // GET: api/book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _catalogService.ShowCatalog();
        }

        // GET api/book/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return _catalogService.FindBook(id);
        }

        // GET api/book/type/FANTASY
        [HttpGet("type/{type}")]
        public IEnumerable<Book> GetType(BookType type)
        {
            return _catalogService.ShowCatalog(type);
        }

        // GET api/book/topRatedBook
        [HttpGet("topRatedBook")]
        public Book GetBest()
        {
            return _catalogService.GetBestBook();
        }
    }
}
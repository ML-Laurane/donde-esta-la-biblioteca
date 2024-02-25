using BusinessObjects.Entity;
using BusinessLayer.Catalog;
using BusinessObjects.Enums;

namespace Services.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogManager _catalogManager;
        
        public CatalogService(ICatalogManager catalogManager)
        {
            _catalogManager = catalogManager;
        }

        public IEnumerable<Book> ShowCatalog()
        {
            return _catalogManager.GetCatalog();
        }

        public IEnumerable<Book> ShowCatalog(BookType type)
        {
            return _catalogManager.GetCatalog(type);
        }

        public Book FindBook(int id)
        {
            return _catalogManager.FindBook(id);
        }

        public List<Book> GetFANTASY()
        {
            return _catalogManager.GetFANTASY();
        }

        public Book GetBestBook()
        {
            return _catalogManager.GetBest();
        }
    }
}

using BusinessLayer.Catalog;
using BusinessObjects.Entity;

namespace Services.Services
{
    public class CatalogService : ICatalogService
    {
        private CatalogManager _catalogManager = new CatalogManager();

        public void ShowCatalog()
        {
            _catalogManager.DisplayCatalog();
        }

        public void ShowCatalog(Type type)
        {
            _catalogManager.DisplayCatalog();
        }

        public Book FindBook(int id)
        {
            return _catalogManager.FindBook(id);
        }
    }
}
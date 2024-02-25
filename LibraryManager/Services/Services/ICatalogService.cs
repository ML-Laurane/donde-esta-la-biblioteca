using BusinessLayer.Catalog;
using BusinessObjects.Entity;
using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface ICatalogService
    {
        public IEnumerable<Book> ShowCatalog();
        public IEnumerable<Book> ShowCatalog(BookType type);
        public Book FindBook(int id);
        public List<Book> GetFANTASY();
        public Book GetBestBook();
    }
}

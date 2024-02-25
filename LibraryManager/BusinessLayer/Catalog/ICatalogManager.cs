using BusinessObjects.Entity;
using BusinessObjects.Enums;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Catalog
{
    public interface ICatalogManager
    {
        public IEnumerable<Book> GetCatalog();
        public List<Book> GetCatalog(BookType type);
        public List<Book> GetFANTASY();
        public Book GetBest();
        public Book FindBook(int id);
    }
}

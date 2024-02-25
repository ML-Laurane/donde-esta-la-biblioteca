using BusinessObjects.Entity;
using BusinessObjects.Enums;
using DataAccessLayer.Repository;

namespace BusinessLayer.Catalog
{
    public class CatalogManager : ICatalogManager
    {
        public IGenericRepository<Book> _bookRepository;

        public CatalogManager(IGenericRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetCatalog()
        {
            return _bookRepository.GetAll();
        }

        public List<Book> GetCatalog(BookType type)
        {
            List<Book> books = this.GetCatalog().Where(book => type == book.Type).ToList();
            return books;
        }

        public List<Book> GetFANTASY()
        {
            List<Book> FANTASYBooks = this.GetCatalog().Where(book => BookType.FANTASY == book.Type).ToList();
            return FANTASYBooks;
        }
        public Book GetBest()
        {
            Book bestBook = this.GetCatalog().OrderByDescending(book => book.Rate).FirstOrDefault();
            return bestBook;
        }
        public Book FindBook(int id)
        {
            return _bookRepository.Get(id);
        }
    }
}

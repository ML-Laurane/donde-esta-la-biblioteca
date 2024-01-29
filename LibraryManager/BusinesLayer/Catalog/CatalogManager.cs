using BusinessObjects.Entity;
using DataAccessLayer.Data;
using DataAccessLayer.Repository;


namespace BusinessLayer.Catalog
{
    public class CatalogManager : ICatalogManager
    {
        private BookRepository _bookRepository;

        public CatalogManager(DataContext context)
        {
            _bookRepository = new BookRepository(context);
        }

        public async Task DisplayCatalog()
        {
            List<Book> books = await _bookRepository.GetAll();
            Console.WriteLine("Liste des livres :");
            foreach (Book book in books)
            {
                Console.WriteLine(book.Name);
            }
        }

        public async Task DisplayCatalog(Type type)
        {
            List<Book> books = await _bookRepository.GetAll();
            Console.WriteLine($"Liste des livres du type {type} :");
            foreach (Book book in books)
            {
                if (book.Type == type)
                {
                    Console.WriteLine(book.Name);

                }
            }
        }

        public async Task<Book> FindBook(int id)
        {
            Book book = await _bookRepository.Get(id);
            Console.WriteLine($"Book avec l'ID {book.Id} {book.Name}");
            return book;
        }

    }
}
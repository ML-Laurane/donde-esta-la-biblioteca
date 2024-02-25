using BusinessObjects.Entity;
using DataAccessLayer.Repository;
using DataAccessLayer.Contexts;

/// <summary>
/// Summary description for BookRepository
/// </summary>

public class BookRepository : IGenericRepository<Book>
{
    private LibraryContext _libraryContext;
    public BookRepository(LibraryContext context)
    {
        _libraryContext = context;
    }

    

    public List<Book> GetAll()
    {
        return _libraryContext._books.ToList();
    }

    public Book Get(int id)
    {
        return _libraryContext._books.Where(x => x.Id.Equals(id)).FirstOrDefault();
    }
}
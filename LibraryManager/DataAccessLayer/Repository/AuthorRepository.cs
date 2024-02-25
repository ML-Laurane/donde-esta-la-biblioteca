using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;

namespace DataAccessLayer.Repository
{
    public class AuthorRepository : IGenericRepository<Author>
    {
        private LibraryContext _libraryContext;
        public AuthorRepository(LibraryContext context)
        {
            _libraryContext = context;
        }

        public List<Author> GetAll()
        {
            return _libraryContext._authors.ToList();
        }

        public Author Get(int id)
        {
            return _libraryContext._authors.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
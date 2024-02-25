using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;

namespace DataAccessLayer.Repository
{
    public class LibraryRepository : IGenericRepository<Library>
    {
        private LibraryContext _libraryContext;
        public LibraryRepository(LibraryContext context)
        {
            _libraryContext = context;
        }
        
        public List<Library> GetAll()
        {
            return _libraryContext._libraries.ToList();
        }

        public Library Get(int id)
        {
            return _libraryContext._libraries.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
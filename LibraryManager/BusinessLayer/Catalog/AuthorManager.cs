using BusinessObjects.Entity;
using BusinessObjects.Enums;
using DataAccessLayer.Repository;

namespace BusinessLayer.Catalog
{
    public class AuthorManager : IAuthorManager
    {
        public IGenericRepository<Author> _authorRepository;

        public AuthorManager(IGenericRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<Author> GetAuthors()
        {
            return _authorRepository.GetAll();
        }

        public Author FindAuthor(int id)
        {
            return _authorRepository.Get(id);
        }
    }
}

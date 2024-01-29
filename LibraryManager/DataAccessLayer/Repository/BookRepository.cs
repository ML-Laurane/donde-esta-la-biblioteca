using BusinessObjects.Entity;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class BookRepository : ARepository<Book>
    {
        public BookRepository(DataContext context) : base(context)
        {
        }
    }
}

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
    public interface IAuthorManager
    {
        public List<Author> GetAuthors();
        public Author FindAuthor(int id);
    }
}

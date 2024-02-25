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
    public interface ILibraryManager
    {
        public List<Library> GetLibraries();
        public Library GetLibrary(int id);
    }
}

using BusinessObjects.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Entity
{
    [Table("book")]
    public class Book : AEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public BookType Type  { get; set; }
        public int Rate { get; set; }
        [ForeignKey("Author")]
        public int Id_author { get; set; }

        public Book(int id, string name, int pages, BookType type, int rate, int id_author)
        {
            this.Id = id;
            this.Name = name;
            this.Pages = pages;
            this.Type = type;
            this.Rate = rate;
            this.Id_author = id_author;
        }


    }
}

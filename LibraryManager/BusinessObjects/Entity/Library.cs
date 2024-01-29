namespace BusinessObjects.Entity
{
    public class Library : Entity
    {
        public string Name { get; set; }

        public string Adress { get; set; }


        public ICollection<Book> Books { get; set; }
    }
}
namespace ORMMapping.Entity
{
    public class BookPublisher
    {
        public Publisher publisher {  get; set; }
        public int PublisherId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

    }
}

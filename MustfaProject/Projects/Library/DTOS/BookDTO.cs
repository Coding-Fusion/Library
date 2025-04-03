namespace Library.DTOS
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string CategoryId { get; set; }

        public CategoryDTO category { get; set; }
        public AuthorDTO author { get; set; }


    }
}

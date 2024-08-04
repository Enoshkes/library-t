namespace LibraryT.Models
{
    public class ShelfModel
    {
        public int Id { get; set; }
        public required int Width { get; set; }
        public required int Height { get; set; }
        public LibraryModel? Library { get; set; }
        public int LibraryId { get; set; }
        public List<BookSetModel> BookSets { get; set; } = [];

    }
}
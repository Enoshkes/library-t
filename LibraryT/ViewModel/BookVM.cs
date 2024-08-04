namespace LibraryT.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }
        public int BookSetId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

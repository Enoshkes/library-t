using System.ComponentModel.DataAnnotations;

namespace LibraryT.Models
{
    public class BookSetModel
    {
        public int Id { get; set; }
        [Required, StringLength(128)]
        public required string Name { get; set; }
        public ShelfModel? Shelf { get; set; }
        public int ShelfId { get; set; }
        public List<BookModel> Books { get; set; } = [];

    }
}
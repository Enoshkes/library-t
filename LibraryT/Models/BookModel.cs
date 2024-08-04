using System.ComponentModel.DataAnnotations;

namespace LibraryT.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public required string Title { get; set; }

        [Required, StringLength(100)]
        public required string Genre { get; set; }
        public required int Width {  get; set; } 
        public required int Height { get; set; }
        public BookSetModel BookSet { get; set; }
        public int BookSetId { get; set; }
    }
}
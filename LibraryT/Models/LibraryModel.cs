using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryT.Models
{
    [Index(nameof(Genre), IsUnique = true)]
    public class LibraryModel
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public required string Genre { get; set; }
        public List<ShelfModel> Shelves { get; set; } = [];
    }
}

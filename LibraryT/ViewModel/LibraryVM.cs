using System.ComponentModel.DataAnnotations;

namespace LibraryT.ViewModel
{
    public class LibraryVM
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Genre { get; set; } = string.Empty;
    }
}

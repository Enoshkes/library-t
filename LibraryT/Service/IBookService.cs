using LibraryT.Models;
using LibraryT.ViewModel;

namespace LibraryT.Service
{
    public interface IBookService
    {
        Task<List<BookModel>> GetBooksByBookSetIdAsync(int bookSetId);

        Task<BookModel> CreateBookByBookVM(BookVM bookVM);

        Task<bool> IsHeightLessThan10(BookVM bookVM);
    }
}

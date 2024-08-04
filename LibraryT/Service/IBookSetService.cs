using LibraryT.Models;
using LibraryT.ViewModel;

namespace LibraryT.Service
{
    public interface IBookSetService
    {
        Task<List<BookSetModel>> GetBookSetsByShelfIdAsync(int shelfId);

        Task<BookSetModel> CreateBookSetByBookSetVMAsync(BookSetVM bookSetVM);
    }
}

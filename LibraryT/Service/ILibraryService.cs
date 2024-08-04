using LibraryT.Models;
using LibraryT.ViewModel;

namespace LibraryT.Service
{
    public interface ILibraryService
    {
        Task<List<LibraryModel>> GetLibrariesAsync();
        
        Task<bool> IsExistsByGenreAsync(string genre);

        Task<LibraryModel> CreateLibraryAsync(LibraryVM libraryVM);

        Task<LibraryModel?> FindLibraryByIdAsync(int id);

        Task<LibraryModel?> DeleteByIdAsync(int id);
    }
}

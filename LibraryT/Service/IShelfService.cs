using LibraryT.Models;
using LibraryT.ViewModel;

namespace LibraryT.Service
{
    public interface IShelfService
    {
        Task<List<ShelfModel>> GetAllShelfAsync();

        Task<List<ShelfModel>> GatAllShelfByLibraryIdAsync(int libraryId);

        Task<ShelfModel> CreateShelfByShelfVMAsync(ShelfVM shelfVM);

        Task<ShelfModel?> FindShelfByIAsync(int shelfId);

        Task<ShelfModel> DeleteByShelfIdAsync(int shelfId);
    }
}

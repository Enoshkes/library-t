using LibraryT.Data;
using LibraryT.Models;
using LibraryT.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryT.Service
{
    public class ShelfService(ApplicationDbContext context, ILibraryService libraryService) : IShelfService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILibraryService _libraryService = libraryService;

        public async Task<ShelfModel> CreateShelfByShelfVMAsync(ShelfVM shelfVM)
        {
            var library = await _libraryService.FindLibraryByIdAsync(shelfVM.LibraryId)
                ?? throw new Exception($"Library by the id {shelfVM.LibraryId} does not exists");

            var model = new ShelfModel()
            {
                Height = shelfVM.Height,
                Width = shelfVM.Width,
                LibraryId = shelfVM.LibraryId,
            };

            library.Shelves.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ShelfModel> DeleteByShelfIdAsync(int shelfId)
        {
            var shelf = await FindShelfByIAsync(shelfId)
                ?? throw new Exception($"Shelf by the id {shelfId} does not exists");

            _context.Shelves.Remove(shelf);
            await _context.SaveChangesAsync();
            return shelf;
        }

        public async Task<ShelfModel?> FindShelfByIAsync(int shelfId) =>
            await _context.Shelves.FindAsync(shelfId);

        public async Task<List<ShelfModel>> GatAllShelfByLibraryIdAsync(int libraryId)
        {
            var library = await _context.Libraries
                 .Include(lib => lib.Shelves)
                 .FirstOrDefaultAsync(lib => lib.Id == libraryId)
                 ?? throw new Exception($"Shelf by the library id {libraryId} does not exists");

            return library.Shelves.ToList();
        }

        public async Task<List<ShelfModel>> GetAllShelfAsync() =>
            await _context.Shelves.ToListAsync();
    }
}

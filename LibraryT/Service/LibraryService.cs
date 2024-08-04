using LibraryT.Data;
using LibraryT.Models;
using LibraryT.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryT.Service
{
    public class LibraryService(ApplicationDbContext context) : ILibraryService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<LibraryModel> CreateLibraryAsync(LibraryVM libraryVM)
        {
            if (await IsExistsByGenreAsync(libraryVM.Genre))
            {
                throw new Exception(
                    $"Library by the genre {libraryVM.Genre} is alreay exists"
                );
            }
            LibraryModel model = new () { Genre = libraryVM.Genre };
            await _context.Libraries.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<LibraryModel?> DeleteByIdAsync(int id)
        {
            var toDelete = await FindLibraryByIdAsync(id)
                ?? throw new Exception($"Library by the id {id} does not exists");

            _context.Remove(toDelete);
            await _context.SaveChangesAsync();
            return toDelete;
        }

        public async Task<LibraryModel?> FindLibraryByIdAsync(int id) =>
            await _context.Libraries.FindAsync(id);

        public async Task<List<LibraryModel>> GetLibrariesAsync() =>
            await _context.Libraries.ToListAsync();

        public async Task<bool> IsExistsByGenreAsync(string genre) =>
            await _context.Libraries.AnyAsync(x => x.Genre == genre);
        
    }
}

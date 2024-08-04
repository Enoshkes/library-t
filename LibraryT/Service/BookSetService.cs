using LibraryT.Data;
using LibraryT.Models;
using LibraryT.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryT.Service
{
    public class BookSetService(ApplicationDbContext context, IShelfService shelfService) : IBookSetService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IShelfService _shelfService = shelfService;

        public async Task<BookSetModel> CreateBookSetByBookSetVMAsync(BookSetVM bookSetVM)
        {
            var shelf = await _shelfService.FindShelfByIAsync(bookSetVM.ShelfId)
                ?? throw new Exception($"Shelf by the id {bookSetVM.ShelfId} does not exists");

            BookSetModel bookSet = new () 
            { 
                Name = bookSetVM.Name, 
                ShelfId = bookSetVM.ShelfId 
            };
            shelf.BookSets.Add(bookSet);
            await _context.SaveChangesAsync();
            return bookSet;
        }

        public async Task<List<BookSetModel>> GetBookSetsByShelfIdAsync(int shelfId) 
        {
            var shelf = await _context.Shelves
              .Include(shelf => shelf.BookSets)
              .ThenInclude(bookset => bookset.Books)
              .FirstOrDefaultAsync(x => x.Id == shelfId)
               ?? throw new Exception($"Bookset by the shelf id {shelfId} does not exists");
            
            return shelf.BookSets;
        }
           
    }
}

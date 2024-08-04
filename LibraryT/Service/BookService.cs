using LibraryT.Data;
using LibraryT.Models;
using LibraryT.ViewModel;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace LibraryT.Service
{
    public class BookService(ApplicationDbContext context) : IBookService
    {
        private readonly ApplicationDbContext _context = context;

        private bool IsHeightValid(BookVM book, ShelfModel shelf) => 
            book.Height <= shelf.Height;


        private bool IsWidthValid(BookVM book, ShelfModel shelf) =>
            book.Width < shelf.Width - shelf.BookSets
                .SelectMany(x => x.Books)
                .Sum(x => x.Width);

        private async Task<ShelfModel?> FindShelfByBookVM(BookVM bookVM)
        {
            var bookset = await _context.BookSets.FindAsync(bookVM.BookSetId);
            if (bookset == null) { return null; }
            return await _context.Shelves
                .Include(shelf => shelf.BookSets)
                .ThenInclude(bookset => bookset.Books)
                .FirstOrDefaultAsync(shelf => shelf.Id == bookset.ShelfId);
        }

        private bool IsLessThan10(BookVM book, ShelfModel shelf) =>
            shelf.Height - book.Height >= 10;

        public async Task<BookModel> CreateBookByBookVM(BookVM bookVM)
        {
            var shelf = await FindShelfByBookVM(bookVM)
                ?? throw new Exception($"Shelf by the book vm does not exists");
            
            if (!IsHeightValid(bookVM, shelf))
            {
                throw new Exception("Invalid book height");
            }

            if (!IsWidthValid(bookVM, shelf))
            {
                throw new Exception("Invalid book width");
            }

            var book = new BookModel () 
            {
                BookSetId = bookVM.BookSetId, 
                Genre = bookVM.Genre,
                Height = bookVM.Height,
                Width = bookVM.Width,
                Title = bookVM.Title
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<BookModel>> GetBooksByBookSetIdAsync(int bookSetId)
        {
            var bookset = await _context.BookSets
                .Include(bookset => bookset.Books)
                .FirstOrDefaultAsync(x => x.Id == bookSetId)
                ?? throw new Exception($"Books by the book set id {bookSetId} does not exists");

            return bookset.Books.ToList();
        }

        public async Task<bool> IsHeightLessThan10(BookVM bookVM)
        {
            var shelf = await FindShelfByBookVM(bookVM) 
                ?? throw new Exception("Shelf by book vm does not exists");
            return shelf.Height - bookVM.Height >= 10;
        }
    }
}

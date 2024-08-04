using LibraryT.Service;
using Microsoft.AspNetCore.Mvc;
using LibraryT.ViewModel;

namespace LibraryT.Controllers
{
    public class BooksController(IBookService bookService) : Controller
    {
        private readonly IBookService _bookService = bookService;
        public async Task<IActionResult> Index(int booksetId)
        {
            try
            {
                ViewBag.booksetId = booksetId;
                var books = await _bookService.GetBooksByBookSetIdAsync(booksetId);
                return View(books);
            } 
            catch
            {
                return RedirectToAction("Index", "Library");
            }
        }

        public IActionResult Create(int booksetId) =>
            View(new BookVM() { BookSetId = booksetId });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM bookVM)
        {
            try
            {
                var insertedBook = await _bookService.CreateBookByBookVM(bookVM);
                var isHighEnough = await _bookService.IsHeightLessThan10(bookVM);
                if (isHighEnough)
                {
                    ViewBag.highMessage = "The book has extra space";
                }   
                return View("Index", await _bookService.GetBooksByBookSetIdAsync(insertedBook.BookSetId));

            } catch(Exception ex) 
            {
                ModelState.AddModelError("createError", ex.Message);
                return View("Index", await _bookService.GetBooksByBookSetIdAsync(bookVM.BookSetId));
            }
        }
    }
}

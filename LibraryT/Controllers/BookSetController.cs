using LibraryT.Service;
using Microsoft.AspNetCore.Mvc;
using LibraryT.ViewModel;
using LibraryT.Models;

namespace LibraryT.Controllers
{
    public class BookSetController(IBookSetService bookSetService) : Controller
    {
        private readonly IBookSetService _bookSetService = bookSetService;
        public async Task<IActionResult> Index(int shelfId)
        {
            ViewBag.shelfId = shelfId;
            return View(await _bookSetService.GetBookSetsByShelfIdAsync(shelfId));
        }

        public IActionResult Create(int shelfId)
        {
            var bookSet = new BookSetVM() { ShelfId = shelfId };
            return View(bookSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookSetVM bookSetVM)
        {
            try
            {
                var inserted = await _bookSetService.CreateBookSetByBookSetVMAsync(bookSetVM);
                return RedirectToAction("Index", new { shelfId = bookSetVM.ShelfId });
            }
            catch
            {
                return RedirectToAction("Index", "Library");
            }
        }

    }
}

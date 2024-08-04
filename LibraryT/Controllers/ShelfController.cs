using LibraryT.Service;
using LibraryT.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryT.Controllers
{
    public class ShelfController(IShelfService shelfService) : Controller
    {
        private readonly IShelfService _shelfService = shelfService;


        public async Task<IActionResult> Index(int libraryId)
        {
            try
            {
                ViewBag.LibraryId = libraryId;
                var shelves = await _shelfService.GatAllShelfByLibraryIdAsync(libraryId);
                return View(shelves);
            }
            catch
            {
                return RedirectToAction("Index", "Library");
            }
        }

        public async Task<IActionResult> Details(int shelfId)
        {
            var shelf = await _shelfService.FindShelfByIAsync(shelfId);
            if (shelf == null) { return RedirectToAction("Index");  }
            return View(shelf);
        }

        public IActionResult Create(int libraryId) =>
            View(new ShelfVM() { LibraryId = libraryId });
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelfVM shelfVM)
        {
            try
            {
                var shelf = await _shelfService.CreateShelfByShelfVMAsync(shelfVM);
                return RedirectToAction("Index", new { libraryId = shelf.LibraryId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Library");
            }
        }

        public async Task<IActionResult> Delete(int shelfId)
        {
            try
            {
                var deleted = await _shelfService.DeleteByShelfIdAsync(shelfId);
                return View("Details", deleted);
            }
            catch
            {
                return RedirectToAction("Index", "Library"); 
            }
        }
    }
}

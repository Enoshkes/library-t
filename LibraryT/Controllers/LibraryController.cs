using LibraryT.Service;
using Microsoft.AspNetCore.Mvc;
using LibraryT.ViewModel;

namespace LibraryT.Controllers
{
    public class LibraryController(ILibraryService libraryService) : Controller
    {
        private readonly ILibraryService _libraryService = libraryService;
        public async Task<IActionResult> Index() =>
            View(await _libraryService.GetLibrariesAsync());

        public IActionResult Create() => View(new LibraryVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibraryVM libraryVM)
        {
            try
            {
                await _libraryService.CreateLibraryAsync(libraryVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("createError", ex.Message);
                return View(new LibraryVM());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var library = await _libraryService.FindLibraryByIdAsync(id);
            if (library == null) { return RedirectToAction("Index"); }
            return View(library);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _libraryService.DeleteByIdAsync(id);
                return View("Details", deleted);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("deleteError", ex.Message);
                return View("Index", await _libraryService.GetLibrariesAsync());
            }
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers;

public class BookController : Controller
{
    private static Books allBooks = new Books();
    private static FavoriteBooks allFavoriteBook = new FavoriteBooks();
    public ActionResult Index()
    {
        return View(allBooks);
    }

    public ActionResult FavoriteBooks()
    {
        ViewBag.allBooks = allBooks.Titles;
        return View(allFavoriteBook);
    }

    [HttpPost]
    public IActionResult AddToFavorites(string selectedBook)
    {
        allFavoriteBook.AddFavorite(selectedBook);
        return RedirectToAction("FavoriteBooks");
    }
}

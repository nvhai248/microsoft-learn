using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor_pages.Namespace
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ID { get; set; }
        public void OnGet()
        {
        }
    }
}

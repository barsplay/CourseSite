using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Data;
using Site.Models;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new GeneralForm());
        }

        [HttpPost]
        public async Task<IActionResult> Search(GeneralForm model)
        {
            if (ModelState.IsValid)
            {
                model.AlgOut = BoyerMoore.BoyerMooreAlg(model.SearchText, model.Pattern);
                model.Counts = model.AlgOut.Count;
                _context.SearchRequests.Add(model);
                await _context.SaveChangesAsync();

                return View("Search", model);
            }

            return View("Index", model);
        }

        public async Task<IActionResult> History()
        {
            var searches = await _context.SearchRequests
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return View(searches);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

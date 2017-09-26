using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using notizenapp.Models;

namespace notizenapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly notizenappContext _context;

		public HomeController(notizenappContext context)
		{
			_context = context;
		}

        public async Task<IActionResult> Index()
        {

            var notes = from n in _context.Note
                        select n;


            var notesVM = new NoteViewModel();
			notesVM.notes = await notes.ToListAsync();

			return View(notesVM);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

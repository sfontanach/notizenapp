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

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "finish_desc" : "";

            var notes = from n in _context.Note
                        select n;

            switch (sortOrder) 
            {
                case "finish_desc":
                    notes = notes.OrderByDescending(n => n.FinishDate);
                    break;
				case "created_desc":
					notes = notes.OrderByDescending(n => n.CreatedDate);
					break;
				case "importance_desc":
                    notes = notes.OrderByDescending(n => n.Importance);
					break;
            }

            var notesVM = new NoteViewModel();
			notesVM.notes = await notes.ToListAsync();

			return View(notesVM);
        }


		// GET: Notes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Notes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,CreatedDate,Title,Text,Importance,FinishDate")] Note note)
		{
			if (ModelState.IsValid)
			{
                note.CreatedDate = DateTime.Now;
				_context.Add(note);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(note);
		}


		// GET: Notes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

            var note = await _context.Note.SingleOrDefaultAsync(m => m.ID == id);
			if (note == null)
			{
				return NotFound();
			}
			return View(note);
		}

		// POST: Notes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedDate,Title,Text,Importance,FinishDate,Finished")] Note note)
		{
			if (id != note.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(note);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!NoteExists(note.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(note);
		}


        /**
         * Helper method to check whether a note exists.
         */
		private bool NoteExists(int id)
		{
			return _context.Note.Any(e => e.ID == id);
		}

    }
}

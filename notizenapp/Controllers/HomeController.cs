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

        public IActionResult Index(string sortOrder, bool hideFinished=false)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "finish_desc" : "";

            var notes = from n in _context.Note
                        select n;
            ViewBag.HideSetting = hideFinished;


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

            if (hideFinished) {
                notes = notes.Where(n => n.Finished == false);
            }

            var notesVM = new NoteViewModel();
            notesVM.notes = notes.ToList();

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
		public IActionResult Create(NewNoteViewModel newNote)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Note note = new Note
            {
                Title = newNote.Title,
                Text = newNote.Text,
                Importance = newNote.Importance,
                FinishDate = newNote.FinishDate
            };

            note.CreatedDate = DateTime.Now;
			_context.Add(note);
			_context.SaveChanges();
			
			return View(note);
		}


		// GET: Notes/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

            var note = _context.Note.SingleOrDefault(m => m.ID == id);
			if (note == null)
			{
				return NotFound();
			}
            ViewBag.Importance = note.Importance;
			return View(note);
		}

		// POST: Notes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,CreatedDate,Title,Text,Importance,FinishDate,Finished")] Note note)
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
                    _context.SaveChanges();
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

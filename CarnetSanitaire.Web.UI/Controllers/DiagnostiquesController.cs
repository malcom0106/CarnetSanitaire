using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarnetSanitaire.Web.UI.Data;
using CarnetSanitaire.Web.UI.Models;

namespace CarnetSanitaire.Web.UI.Controllers
{
    public class DiagnostiquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnostiquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diagnostiques
        public async Task<IActionResult> Index()
        {
            return View(await _context.Diagnostiques.ToListAsync());
        }

        // GET: Diagnostiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostique = await _context.Diagnostiques
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostique == null)
            {
                return NotFound();
            }

            return View(diagnostique);
        }

        // GET: Diagnostiques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnostiques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Diagnostique_Realise,Diagnostique_Date,Diagnostique_Intervenant,InstallationId")] Diagnostique diagnostique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnostique);
        }

        // GET: Diagnostiques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostique = await _context.Diagnostiques.FindAsync(id);
            if (diagnostique == null)
            {
                return NotFound();
            }
            return View(diagnostique);
        }

        // POST: Diagnostiques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Diagnostique_Realise,Diagnostique_Date,Diagnostique_Intervenant,InstallationId")] Diagnostique diagnostique)
        {
            if (id != diagnostique.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnostiqueExists(diagnostique.Id))
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
            return View(diagnostique);
        }

        // GET: Diagnostiques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostique = await _context.Diagnostiques
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostique == null)
            {
                return NotFound();
            }

            return View(diagnostique);
        }

        // POST: Diagnostiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnostique = await _context.Diagnostiques.FindAsync(id);
            _context.Diagnostiques.Remove(diagnostique);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnostiqueExists(int id)
        {
            return _context.Diagnostiques.Any(e => e.Id == id);
        }
    }
}

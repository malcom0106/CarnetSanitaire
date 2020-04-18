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
    public class SocietesController : Controller
    {
        private readonly DataSociete _dalSociete;

        public SocietesController(DataSociete dalSociete)
        {
            _dalSociete = dalSociete;
        }

        // GET: Societes
        public IActionResult Index()
        {
            return View(_dalSociete.GetSocietes());
        }

        // GET: Societes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var societe = _dalSociete.GetSocieteById((int)id);

            if (societe == null)
            {
                return NotFound();
            }

            return View(societe);
        }

        // GET: Societes/Create
        public IActionResult Create()
        {
            ViewData["CoordonneeId"] = new SelectList(_context.Coordonnees, "Id", "Adresse");
            return View();
        }

        // POST: Societes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,CoordonneeId")] Societe societe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(societe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordonneeId"] = new SelectList(_context.Coordonnees, "Id", "Adresse", societe.CoordonneeId);
            return View(societe);
        }

        // GET: Societes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var societe = await _context.Societes.FindAsync(id);
            if (societe == null)
            {
                return NotFound();
            }
            ViewData["CoordonneeId"] = new SelectList(_context.Coordonnees, "Id", "Adresse", societe.CoordonneeId);
            return View(societe);
        }

        // POST: Societes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,CoordonneeId")] Societe societe)
        {
            if (id != societe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(societe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocieteExists(societe.Id))
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
            ViewData["CoordonneeId"] = new SelectList(_context.Coordonnees, "Id", "Adresse", societe.CoordonneeId);
            return View(societe);
        }

        // GET: Societes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var societe = await _context.Societes
                .Include(s => s.Coordonnee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (societe == null)
            {
                return NotFound();
            }

            return View(societe);
        }

        // POST: Societes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var societe = await _context.Societes.FindAsync(id);
            _context.Societes.Remove(societe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocieteExists(int id)
        {
            return _context.Societes.Any(e => e.Id == id);
        }
    }
}

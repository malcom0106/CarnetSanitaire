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
    public class TemperaturesController : Controller
    {
        #region Constructeur + Global

        private readonly ApplicationDbContext _context;

        public TemperaturesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        // GET: Temperatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.PointReleveTemperatures.ToListAsync());
        }

        // GET: Temperatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointReleveTemperature = await _context.PointReleveTemperatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointReleveTemperature == null)
            {
                return NotFound();
            }

            return View(pointReleveTemperature);
        }

        // GET: Temperatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Temperatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Localisation,Statut")] PointReleveTemperature pointReleveTemperature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointReleveTemperature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pointReleveTemperature);
        }

        // GET: Temperatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointReleveTemperature = await _context.PointReleveTemperatures.FindAsync(id);
            if (pointReleveTemperature == null)
            {
                return NotFound();
            }
            return View(pointReleveTemperature);
        }

        // POST: Temperatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Localisation,Statut")] PointReleveTemperature pointReleveTemperature)
        {
            if (id != pointReleveTemperature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointReleveTemperature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointReleveTemperatureExists(pointReleveTemperature.Id))
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
            return View(pointReleveTemperature);
        }

        // GET: Temperatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointReleveTemperature = await _context.PointReleveTemperatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointReleveTemperature == null)
            {
                return NotFound();
            }

            return View(pointReleveTemperature);
        }

        // POST: Temperatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointReleveTemperature = await _context.PointReleveTemperatures.FindAsync(id);
            _context.PointReleveTemperatures.Remove(pointReleveTemperature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointReleveTemperatureExists(int id)
        {
            return _context.PointReleveTemperatures.Any(e => e.Id == id);
        }
    }
}

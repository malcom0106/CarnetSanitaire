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
    public class ProductionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DataProduction _dataProduction;

        public ProductionsController(ApplicationDbContext context, DataProduction dataProduction)
        {
            _context = context;
            _dataProduction = dataProduction;
        }



        // GET: Productions/Details/5
        public async Task<IActionResult> Details()
        {
            Production production = null;
            try
            {
                production = await _dataProduction.GetProduction();
            }
            catch (Exception ex)
            {
                await _dataProduction.AddLogErreur(ex);
            }
            if (production == null)
            {
                return RedirectToAction("Create", "Productions");
            }

            return View(production);
        }

        // GET: Productions/Create
        public async Task<IActionResult> Create()
        
        {
            if (await _dataProduction.VerifyProductionInstallation())
            {
                return RedirectToAction("Details", "Productions");
            }
            ViewBag.TypeReseau = new SelectList(_context.TypeReseaus, "Id", "Nom");
            ViewBag.TypeProduction = new SelectList(_context.TypeProductions, "Id", "Nom");
            return View();
        }

        // POST: Productions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Identification,NombreBallon,TemperatureDepartEcs,TemperatureBouclageEcs,TypeReseauId,TypeProductionId")] ModelViewProduction modelViewProduction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataProduction.AddProduction(modelViewProduction);
                    return RedirectToAction("Details", "Productions");
                }
                catch (Exception ex)
                {
                    await _dataProduction.AddLogErreur(ex);
                }

            }
            return View(modelViewProduction);
        }

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            return View(production);
        }

        // POST: Productions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Identification,NombreBallon,TemperatureDepartEcs,TemperatureBouclageEcs,TypeReseauId,TypeProductionId")] ModelViewProduction production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
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
            return View(production);
        }


        private bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }
    }
}

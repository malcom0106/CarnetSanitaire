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
    public class InstallationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DataInstallation _dataInstallation;

        public InstallationsController(ApplicationDbContext context, DataInstallation dataInstallation)
        {
            _context = context;
            _dataInstallation = dataInstallation;
        }

        // GET: Installations/Details/5
        public async Task<IActionResult> Details()
        {
            Installation installation;            
            try
            {
                installation = await _dataInstallation.GetInstallation();
            } 
            catch(Exception ex)
            {
                throw ex;                
            }
            
            if (installation == null)
            {
                return RedirectToAction("Create");
            }

            return View(installation);
        }

        // GET: Installations/Create
        public IActionResult Create()
        {
            ViewBag.CalorifugeageEf = new SelectList(_context.TypeCalorifugeages, "Id", "Nom");
            ViewBag.CalorifugeageEcs = new SelectList(_context.TypeCalorifugeages, "Id", "Nom");
            ViewBag.Materiaux = new SelectList(_context.Materiaus, "Id", "Nom");

            return View();
        }

        // POST: Installations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Materiaux,Interconnexion_Existance,InterconnexionType,CalorifugeageEfId,CalorifugeageEcsId,DispositifProtectionRetourEau")] ModelViewInstallation modelViewInstallation)
        {
            if (ModelState.IsValid)
            {
                await _dataInstallation.CreateInstallation(modelViewInstallation);
                return RedirectToAction("Details");
            }

            ViewBag.CalorifugeageEcs = new SelectList(_context.TypeCalorifugeages, "Id", "Id", modelViewInstallation.CalorifugeageEcsId);
            ViewBag.CalorifugeageEf = new SelectList(_context.TypeCalorifugeages, "Id", "Id", modelViewInstallation.CalorifugeageEfId);
            ViewBag.Materiaux = new SelectList(_context.Materiaus, "Id", "Nom");
            return View(modelViewInstallation);
        }

        // GET: Installations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installation = await _context.Installations.FindAsync(id);
            if (installation == null)
            {
                return NotFound();
            }
            ViewBag.CalorifugeageEf = new SelectList(_context.TypeCalorifugeages, "Id", "Nom");
            ViewBag.CalorifugeageEcs = new SelectList(_context.TypeCalorifugeages, "Id", "Nom");
            ViewBag.Materiaux = new SelectList(_context.Materiaus, "Id", "Nom");
            return View(installation);
        }

        // POST: Installations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Materiaux,Interconnexion_Existance,InterconnexionType,CalorifugeageEfId,CalorifugeageEcsId,DispositifProtectionRetourEau")] ModelViewInstallation modelViewInstallation)
        {
            if (id != modelViewInstallation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelViewInstallation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstallationExists(modelViewInstallation.Id))
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
            ViewBag.CalorifugeageEf = new SelectList(_context.TypeCalorifugeages, "Id", "Nom", modelViewInstallation.CalorifugeageEfId);
            ViewBag.CalorifugeageEcs = new SelectList(_context.TypeCalorifugeages, "Id", "Nom", modelViewInstallation.CalorifugeageEcsId);
            ViewBag.Materiaux = new SelectList(_context.Materiaus, "Id", "Nom",modelViewInstallation.Materiaux);
            return View(modelViewInstallation);
        }       

        private bool InstallationExists(int id)
        {
            return _context.Installations.Any(e => e.Id == id);
        }
    }
}

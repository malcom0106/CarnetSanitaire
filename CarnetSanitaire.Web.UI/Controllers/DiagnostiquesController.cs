using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarnetSanitaire.Web.UI.Data;
using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarnetSanitaire.Web.UI.Controllers
{
    [Authorize]
    public class DiagnostiquesController : Controller
    {
        #region Global et Constructeur
        private readonly ApplicationDbContext _context;
        private readonly DataDiagnostique _dataDiagnostique;

        public DiagnostiquesController(ApplicationDbContext context, DataDiagnostique dataDiagnostique)
        {
            _context = context;
            _dataDiagnostique = dataDiagnostique;
        }
        #endregion

        // GET: Diagnostiques
        public async Task<IActionResult> Index()
        {
            List<Diagnostique> diagnostiques = null;
            try
            {
                diagnostiques = await _dataDiagnostique.GetDiagnostiques();
            }
            catch(Exception ex)
            {
                await _dataDiagnostique.AddLogErreur(ex);
            }
            return View(diagnostiques);
        }

        // GET: Diagnostiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Diagnostique diagnostique = null;
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                diagnostique = await _dataDiagnostique.GetDiagnostiqueById((int)id);
            }
            catch (Exception ex)
            {
                await _dataDiagnostique.AddLogErreur(ex);
            }
            
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Diagnostique_Realise,Diagnostique_Date,Diagnostique_Intervenant")] Diagnostique diagnostique)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    diagnostique = await _dataDiagnostique.AddDiagnostique(diagnostique);
                    return RedirectToAction("Index", new { @installationId = diagnostique.InstallationId });
                }
            }
            catch (Exception ex)
            {
                await _dataDiagnostique.AddLogErreur(ex);
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
            var diagnostique = await _dataDiagnostique.GetDiagnostiqueById((int)id);
            if (diagnostique == null)
            {
                return NotFound();
            }
            return View(diagnostique);
        }

        // POST: Diagnostiques/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Diagnostique_Realise,Diagnostique_Date,Diagnostique_Intervenant,InstallationId")] Diagnostique diagnostique)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _dataDiagnostique.EditDiagnostique(diagnostique);
                }
                catch (Exception ex)
                {
                    if (!_dataDiagnostique.DiagnostiqueExists(diagnostique.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        await _dataDiagnostique.AddLogErreur(ex);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(diagnostique);
        }

        
    }
}

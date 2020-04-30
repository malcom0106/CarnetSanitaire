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
    public class ProductionsController : Controller
    {
        #region Constructeur et Global
        private readonly ApplicationDbContext _context;
        private readonly DataProduction _dataProduction;

        public ProductionsController(ApplicationDbContext context, DataProduction dataProduction)
        {
            _context = context;
            _dataProduction = dataProduction;
        }
        #endregion

        #region Détail
        // GET: Productions/Details/5
        public async Task<IActionResult> Details()
        {
            Production production;
            try
            {
                production = await _dataProduction.GetProduction();
            }
            catch (Exception ex)
            {
                await _dataProduction.AddLogErreur(ex);
                return NotFound();
            }
            if (production == null)
            {
                return RedirectToAction("Create", "Productions");
            }

            return View(production);
        }
        #endregion

        #region Creation
        // GET: Productions/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                if (await _dataProduction.VerifyProductionInstallation())
                {
                    return RedirectToAction("Details", "Productions");
                }
                ViewBag.TypeReseau = new SelectList(_context.TypeReseaus, "Id", "Nom");
                ViewBag.TypeProduction = new SelectList(_context.TypeProductions, "Id", "Nom");
            }
            catch (Exception ex)
            {
                await _dataProduction.AddLogErreur(ex);
                return NotFound();
            }

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
                    return NotFound();
                }

            }
            return View(modelViewProduction);
        }

        #endregion

        #region Edition

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit()
        {
            ModelViewProduction modelViewProduction = null;
            try
            {
                modelViewProduction = await _dataProduction.GetProductionModelView();
                ViewBag.TypeReseau = new SelectList(_context.TypeReseaus, "Id", "Nom");
                ViewBag.TypeProduction = new SelectList(_context.TypeProductions, "Id", "Nom");
            }
            catch (Exception ex)
            {
                await _dataProduction.AddLogErreur(ex);
            }
            if (modelViewProduction == null)
            {
                return NotFound();
            }
            return View(modelViewProduction);
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
                    if (!await _dataProduction.EditProduction(production))
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    if (!_dataProduction.ProductionExists(production.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        await _dataProduction.AddLogErreur(ex);
                        return NotFound();
                    }
                }
                return RedirectToAction("Details");
            }
            return View(production);
        }

        #endregion


    }
}

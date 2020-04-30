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
    public class TemperaturesController : Controller
    {
        #region Constructeur + Global

        private readonly ApplicationDbContext _context;
        private readonly DataTemperature _dataTemperature;

        public TemperaturesController(ApplicationDbContext context, DataTemperature dataTemperature)
        {
            _context = context;
            _dataTemperature = dataTemperature;
        }
        #endregion

        #region IndexPoint
        // GET: Temperatures
        public async Task<IActionResult> IndexPoints()
        {
            List<PointReleveTemperature> pointReleveTemperatures;
            try
            {
                pointReleveTemperatures = await _dataTemperature.GetPointReleveTemperatures();
            }
            catch(Exception ex)
            {
                await _dataTemperature.AddLogErreur(ex);
                return NotFound();
            }
            return View(pointReleveTemperatures);
        }

        #endregion

        #region DetailPoint

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

        #endregion

        #region CreatePoint

        // GET: Temperatures/Create
        public IActionResult CreatePoint()
        {
            return View();
        }


        // POST: Temperatures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePoint([Bind("Id,Nom,Localisation,TypePointId,Statut")] PointReleveTemperature pointReleveTemperature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointReleveTemperature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pointReleveTemperature);
        }

        #endregion

        #region EditPoint

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Localisation,TypePointId,Statut")] PointReleveTemperature pointReleveTemperature)
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
                    if (!_dataTemperature.PointReleveTemperatureExists(pointReleveTemperature.Id))
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
        #endregion

        
    }
}

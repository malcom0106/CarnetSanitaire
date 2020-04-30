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

        private readonly DataPoco _dataPoco;
        private readonly DataTemperature _dataTemperature;

        public TemperaturesController(DataTemperature dataTemperature, DataPoco dataPoco)
        {
            _dataPoco = dataPoco;
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
        public async Task<IActionResult> DetailsPoint(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointReleveTemperature = await _dataTemperature.GetPointReleveTemperatureById((int)id);

            if (pointReleveTemperature == null)
            {
                return NotFound();
            }

            return View(pointReleveTemperature);
        }

        #endregion

        #region CreatePoint

        // GET: Temperatures/Create
        public async Task<IActionResult> CreatePoint()
        {
            ViewBag.TypePoint = new SelectList(await _dataPoco.GetTypePoint(), "Id", "Nom");
            return View();
        }


        // POST: Temperatures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePoint([Bind("Id,Nom,Localisation,TypePointId,Statut")] PointReleveTemperature pointReleveTemperature)
        {
            if (ModelState.IsValid)
            {
                if (! await _dataTemperature.AddPointReleveTemperature(pointReleveTemperature))
                {
                    return NotFound();
                }
                return RedirectToAction("IndexPoints");
            }
            ViewBag.TypePoint = new SelectList(await _dataPoco.GetTypePoint(), "Id", "Nom");
            return View(pointReleveTemperature);
        }

        #endregion

        #region EditPoint

        // GET: Temperatures/Edit/5
        public async Task<IActionResult> EditPoint(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointReleveTemperature = await _dataTemperature.GetPointReleveTemperatureById((int)id);
            ViewBag.TypePoint = new SelectList(await _dataPoco.GetTypePoint(), "Id", "Nom");

            if (pointReleveTemperature == null)
            {
                return NotFound();
            }
            return View(pointReleveTemperature);
        }

        // POST: Temperatures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPoint(int id, [Bind("Id,Nom,Localisation,TypePointId, EtablissementId,Statut")] PointReleveTemperature pointReleveTemperature)
        {
            if (id != pointReleveTemperature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(! await _dataTemperature.EditPointReleveTemperature(pointReleveTemperature))
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    if (!_dataTemperature.PointReleveTemperatureExists(pointReleveTemperature.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        await _dataTemperature.AddLogErreur(ex);
                    }
                }
                return RedirectToAction("IndexPoints");
            }
            ViewBag.TypePoint = new SelectList(await _dataPoco.GetTypePoint(), "Id", "Nom");
            return View(pointReleveTemperature);
        }
        #endregion

    }
}

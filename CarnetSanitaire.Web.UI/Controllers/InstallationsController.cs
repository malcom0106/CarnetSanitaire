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
    public class InstallationsController : Controller
    {
        #region Global et Constructeur
        private readonly DataInstallation _dataInstallation;
        private readonly DataPoco _dataPoco;

        public InstallationsController(DataInstallation dataInstallation, DataPoco dataPoco)
        {
            _dataInstallation = dataInstallation;
            _dataPoco = dataPoco;
        }
        #endregion

        // GET: Installations/Details/5
        public async Task<IActionResult> Details()
        {
            Installation installation = null;            
            try
            {
                
                installation = await _dataInstallation.GetInstallation();
            } 
            catch(Exception ex)
            {
                await _dataInstallation.AddLogErreur(ex);                
            }
            
            if (installation == null)
            {
                return RedirectToAction("Create");
            }

            return View(installation);
        }

        // GET: Installations/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
                ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
                ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");
            }
            catch(Exception ex)
            {
                ViewBag.CalorifugeageEf = null;
                ViewBag.CalorifugeageEcs = null;
                ViewBag.Materiaux = null;

                await _dataInstallation.AddLogErreur(ex);
            }

            return View();
        }

        // POST: Installations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Materiaux,Interconnexion_Existance,InterconnexionType,CalorifugeageEfId,CalorifugeageEcsId,DispositifProtectionRetourEau")] ModelViewInstallation modelViewInstallation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _dataInstallation.CreateInstallation(modelViewInstallation);
                    return RedirectToAction("Details");
                }

                ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom", modelViewInstallation.CalorifugeageEcsId);
                ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom", modelViewInstallation.CalorifugeageEfId);
                ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");
            }
            catch (Exception ex)
            {
                ViewBag.CalorifugeageEf = null;
                ViewBag.CalorifugeageEcs = null;
                ViewBag.Materiaux = null;

                await _dataInstallation.AddLogErreur(ex);
            }

            return View(modelViewInstallation);
        }

        // GET: Installations/Edit/5
        public async Task<IActionResult> Edit()
        {
            ModelViewInstallation installation = null;
            try
            {
                installation = await _dataInstallation.GetInstallationByModelView();

                ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
                ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
                ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");
            }
            catch (Exception ex)
            {
                ViewBag.CalorifugeageEf = null;
                ViewBag.CalorifugeageEcs = null;
                ViewBag.Materiaux = null;

                await _dataInstallation.AddLogErreur(ex);
            }

            
            return View(installation);
        }

        // POST: Installations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Materiaux,Interconnexion_Existance,InterconnexionType,CalorifugeageEfId,CalorifugeageEcsId,DispositifProtectionRetourEau")] ModelViewInstallation modelViewInstallation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataInstallation.EditInstallation(modelViewInstallation);

                    ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
                    ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
                    ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");
                }
                catch (Exception ex)
                {
                    if (!_dataInstallation.InstallationExists(modelViewInstallation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.CalorifugeageEf = null;
                        ViewBag.CalorifugeageEcs = null;
                        ViewBag.Materiaux = null;

                        await _dataInstallation.AddLogErreur(ex);
                    }
                }
                return RedirectToAction("Details");
            }

            return View(modelViewInstallation);
        }       

        
    }
}

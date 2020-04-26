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
        private readonly DataPoco _dataPoco;

        public InstallationsController(ApplicationDbContext context, DataInstallation dataInstallation, DataPoco dataPoco)
        {
            _context = context;
            _dataInstallation = dataInstallation;
            _dataPoco = dataPoco;
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
        public async Task<IActionResult> Create()
        {
            ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
            ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
            ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");

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

            ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom", modelViewInstallation.CalorifugeageEcsId);
            ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom", modelViewInstallation.CalorifugeageEfId);
            ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");

            return View(modelViewInstallation);
        }

        // GET: Installations/Edit/5
        public async Task<IActionResult> Edit()
        {
            var installation = await _dataInstallation.GetInstallationByModelView();
            
            ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
            ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom");
            ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");
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
                return RedirectToAction("Details");
            }

            ViewBag.CalorifugeageEf = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom", modelViewInstallation.CalorifugeageEfId);
            ViewBag.CalorifugeageEcs = new SelectList(await _dataPoco.GetTypeCalorifugeage(), "Id", "Nom", modelViewInstallation.CalorifugeageEcsId);
            ViewBag.Materiaux = new SelectList(await _dataPoco.GetMateriaux(), "Id", "Nom");

            return View(modelViewInstallation);
        }       

        private bool InstallationExists(int id)
        {
            return _context.Installations.Any(e => e.Id == id);
        }
    }
}

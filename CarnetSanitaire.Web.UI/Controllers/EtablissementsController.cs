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
    public class EtablissementsController : Controller
    {
        
        private readonly DataEtablissement _dataEtablissement;

        public EtablissementsController(DataEtablissement dataEtablissement)
        {
            
            _dataEtablissement = dataEtablissement;
        }

        // GET: Etablissements
        public async Task<IActionResult> Index()
        {
            return View(await _dataEtablissement.GetEtablissements());
        }

        // GET: Etablissements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Etablissement etablissement;
            try
            {
                etablissement = await _dataEtablissement.GetEtablissementById((int)id);
            }
            catch (Exception ex)
            {
                await _dataEtablissement.AddLogErreur(ex);
                return NotFound();
            }
            
            if (etablissement == null)
            {
                return NotFound();
            }

            return View(etablissement);
        }

        // GET: Etablissements/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: Etablissements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Capacite,Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] ModelViewEtablissement modelViewEtablissement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataEtablissement.AddEtablissementByModelView(modelViewEtablissement);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    await _dataEtablissement.AddLogErreur(ex);
                    return NotFound();
                }
                
            }            
            return View(modelViewEtablissement);
        }

        // GET: Etablissements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelViewEtablissement modelViewEtablissement;
            try
            {
                modelViewEtablissement = await _dataEtablissement.GetModelViewEtablissementById((int)id);
            }
            catch (Exception ex)
            {
                await _dataEtablissement.AddLogErreur(ex);
                return NotFound();
            }

            if (modelViewEtablissement == null)
            {
                return NotFound();
            }            
            return View(modelViewEtablissement);
        }

        // POST: Etablissements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Capacite,Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] ModelViewEtablissement modelViewEtablissement)
        {
            if (id != modelViewEtablissement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dataEtablissement.EditEtablissementByModelViewEtablissment(modelViewEtablissement);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dataEtablissement.EtablissementExists(modelViewEtablissement.Id))
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
            return View(modelViewEtablissement);
        }
    }
}

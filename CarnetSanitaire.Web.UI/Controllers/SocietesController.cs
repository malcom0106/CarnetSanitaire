using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarnetSanitaire.Web.UI.Data;
using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CarnetSanitaire.Web.UI.Controllers
{
    [Authorize]
    public class SocietesController : Controller
    {
        #region Global et Constructeur
        private readonly DataSociete _dataSociete;

        public SocietesController(DataSociete dataSociete)
        {
            _dataSociete = dataSociete;
        }
        #endregion


        // GET: Societes
        public async Task<IActionResult> Index()
        {
            List<Societe> societes = null;
            try
            {
                societes = await _dataSociete.GetSocietes();
            }
            catch(Exception ex)
            {
                await _dataSociete.AddLogErreur(ex);
            }
            
            return View(societes);
        }

        // GET: Societes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if( await _dataSociete.VerifySocieteInEtablissement(id))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Societe societe = null;
            try
            {
                societe = await _dataSociete.GetSocieteById((int)id);
                if (societe == null)
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                await _dataSociete.AddLogErreur(ex);
            } 

            return View(societe);
        }

        // GET: Societes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Societes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom, Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] SocieteModelView societeModelView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataSociete.AddSocieteByModelView(societeModelView);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    await _dataSociete.AddLogErreur(ex);
                }
                
            }            
            return View(societeModelView);
        }

        // GET: Societes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SocieteModelView societe = null;
            try
            {
                if (await _dataSociete.VerifySocieteInEtablissement(id))
                {
                    return NotFound();
                }
                if (id == null)
                {
                    return NotFound();
                }

                societe = await _dataSociete.GetSocieteModelViewById(id);

                if (societe == null)
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                await _dataSociete.AddLogErreur(ex);
            }
            
            
            return View(societe);
        }

        // POST: Societes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nom, Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] SocieteModelView societe)
        {
            if (id != societe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dataSociete.EditSocieteByModel(societe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocieteExists(societe.Id))
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
            return View(societe);
        }

        private bool SocieteExists(int id)
        {
            //return _context.Societes.Any(e => e.Id == id);
            return true;
        }
    }
}

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
        private readonly DataVerification _dataVerification;

        public SocietesController(DataSociete dataSociete,DataVerification dataVerification)
        {
            _dataSociete = dataSociete;
            _dataVerification = dataVerification;
        }
        #endregion


        // GET: Societes
        public async Task<IActionResult> Index()
        {
            List<Societe> societes;
            try
            {
                societes = await _dataSociete.GetSocietes();
            }
            catch(Exception ex)
            {
                await _dataSociete.AddLogErreur(ex);
                return NotFound();
            }
            
            return View(societes);
        }

        // GET: Societes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if( !await _dataVerification.VerifySocieteInEtablissement(id))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Societe societe;
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
                return NotFound();
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
        public async Task<IActionResult> Create([Bind("Nom, Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] ModelViewSociete societeModelView)
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
            ModelViewSociete societe;
            try
            {
                if (!await _dataVerification.VerifySocieteInEtablissement(id))
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
                return NotFound();
            }
            
            
            return View(societe);
        }

        // POST: Societes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nom, Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] ModelViewSociete societe)
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
                    if (!_dataSociete.SocieteExists(societe.Id))
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

        
    }
}

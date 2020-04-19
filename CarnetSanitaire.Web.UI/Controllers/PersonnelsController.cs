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
    public class PersonnelsController : Controller
    {
        #region Contrcteur et global
        private readonly DataPersonnel _dataPersonnel;

        public PersonnelsController(DataPersonnel dataPersonnel)
        {
            _dataPersonnel = dataPersonnel;
        }
        #endregion

        // GET: Personnels/Index/5
        public async Task<IActionResult> Index(int? societeId)
        {
            List<Personnel> personnels = null;
            try
            {
                if (societeId == null)
                {
                    return NotFound();
                }
                ViewBag.SocieteId = societeId;
                personnels = await _dataPersonnel.GetPersonnelOfSociety(societeId);
            }
            catch(Exception ex)
            {
                await _dataPersonnel.AddLogErreur(ex);
            }
            
            return View(personnels);
        }

        // GET: Personnels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = _dataPersonnel.GetPersonnelById(id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: Personnels/Create
        public IActionResult Create(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            ViewBag.SocieteId = Id;
            return View();
        }

        // POST: Personnels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Telephone,Email,SocieteId")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                await _dataPersonnel.AddPersonnel(personnel);
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public async Task<IActionResult> Edit(int? personnelId)
        {
            if (personnelId == null)
            {
                return NotFound();
            }

            var personnel = await _dataPersonnel.GetPersonnelById(personnelId);

            if (personnel == null)
            {
                return NotFound();
            } 
            
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Telephone,Email,SocieteId")] Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dataPersonnel.EditPersonnel(personnel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.Id))
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
            return View(personnel);
        }
                

        private bool PersonnelExists(int id)
        {
            return _dataPersonnel.PersonnelExists(id);
        }
    }
}

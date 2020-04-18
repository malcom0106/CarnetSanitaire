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
        private readonly DataSociete _dalSociete;

        public SocietesController(DataSociete dalSociete)
        {
            _dalSociete = dalSociete;
        }

        // GET: Societes
        public IActionResult Index()
        {
            List<Societe> societes = null;
            try
            {
                societes = _dalSociete.GetSocietes().Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return View(societes);
        }

        // GET: Societes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var societe = _dalSociete.GetSocieteById((int)id);

            if (societe == null)
            {
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
        public async Task<IActionResult> Create([Bind("Nom, Adresse, SubAdresse, CodePostal, Ville, Fax, Telephone, Email")] SocieteModelView societeModelView)
        {
            if (ModelState.IsValid)
            {
                await _dalSociete.AddSocieteByModelView(societeModelView);
                return RedirectToAction(nameof(Index));
            }            
            return View(societeModelView);
        }

        // GET: Societes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SocieteModelView societe = await _dalSociete.GetSocieteModelViewById(id);

            if (societe == null)
            {
                return NotFound();
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
                    await _dalSociete.EditSocieteByModel(societe);
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

        // GET: Societes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var societe = await _context.Societes
            //    .Include(s => s.Coordonnee)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (societe == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: Societes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var societe = await _context.Societes.FindAsync(id);
            //_context.Societes.Remove(societe);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocieteExists(int id)
        {
            //return _context.Societes.Any(e => e.Id == id);
            return true;
        }
    }
}

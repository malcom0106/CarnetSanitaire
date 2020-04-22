using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataPersonnel : DataAccess
    {

        public DataPersonnel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {

        }

        public async Task<List<Personnel>> GetPersonnelOfSociety(int? societeId)
        {
            List<Personnel> personnels = null;
            if (societeId != null)
            {
                try
                {
                    personnels = await _context.Personnels.Include(p => p.Societe).Where(p => p.SocieteId == societeId).ToListAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                personnels = new List<Personnel>();
            }

            return personnels;
        }

        public async Task<Personnel> GetPersonnelById(int? personnelId)
        {
            Personnel personnel;
            try
            {
                personnel = await _context.Personnels
                   .Include(p => p.Societe)
                   .FirstOrDefaultAsync(m => m.Id == personnelId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return personnel;
        }

        public async Task AddPersonnel(Personnel personnel)
        {
            try
            {
                personnel.Nom = personnel.Nom.ToUpper();
                personnel.Statut = true;
                _context.Personnels.Add(personnel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditPersonnel(Personnel personnel)
        {
            try
            {
                personnel.Nom = personnel.Nom.ToUpper();
                _context.Personnels.Update(personnel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ChangeStatut(int personnelId)
        {
            try
            {
                Personnel personnel = _context.Personnels.Find(personnelId);
                personnel.Statut = !personnel.Statut;
                _context.Personnels.Update(personnel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PersonnelExists(int id)
        {
            return _context.Personnels.Any(e => e.Id == id);
        }
    }
}

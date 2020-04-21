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
                    personnels = await _context.Personnels.Include(p=>p.Societe).Where(p => p.SocieteId == societeId).ToListAsync();
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
            var personnel = await _context.Personnels
               .Include(p => p.Societe)
               .FirstOrDefaultAsync(m => m.Id == personnelId);

            return personnel;
        }

        public async Task AddPersonnel(Personnel personnel)
        {
            personnel.Nom = personnel.Nom.ToUpper();
            _context.Personnels.Add(personnel);
            await _context.SaveChangesAsync();
        }

        public async Task EditPersonnel(Personnel personnel)
        {
            personnel.Nom = personnel.Nom.ToUpper();
            _context.Personnels.Update(personnel);
            await _context.SaveChangesAsync();
        }

        public bool PersonnelExists(int id)
        {
            return _context.Personnels.Any(e => e.Id == id);
        } 
    }
}

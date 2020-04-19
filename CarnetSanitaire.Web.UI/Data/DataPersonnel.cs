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
                    personnels = await _context.Personnels.Where(p => p.SocieteId == societeId).ToListAsync();
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
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _context.Users.Where(u => u.Id == userId).Include(u => u.Etablissement).FirstOrDefaultAsync();
            int etablissementId = user.Etablissement.Id;
            var personnel = await _context.Personnels
               .Include(p => p.Societe)
               .FirstOrDefaultAsync(m => m.Id == personnelId);

            return null;
        }

        public async Task EditPersonnel(int? personnelId)
        {

        }
    }
}

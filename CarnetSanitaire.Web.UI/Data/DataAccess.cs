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
    public class DataAccess
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DataAccess(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Etablissement> GetEtablissementByUser()
        {
            Etablissement etablissement = null;
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Etablissement)
                    .FirstOrDefaultAsync();

                int etablissementId = user.Etablissement.Id;
                etablissement = await _context.Etablissements
                    .Include(e => e.Societes)
                    .Include(e => e.Coordonnee)
                    .Include(e => e.ReleveTemperatures)
                    .Include(e => e.CampagneAnalyses)
                    .Include(e => e.Installation).ThenInclude(i => i.Production)
                    .Include(e => e.Interventions)
                    .Where(e => e.Id == user.Etablissement.Id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return etablissement;
        }

        public async Task AddLogErreur(Exception ex)
        {
            string message = ex.Message;
            LogErreur logErreur = new LogErreur
            {
                MessageErreur = message,
                DateErreur = DateTime.Now
            };

            _context.LogErreurs.Add(logErreur);
            await _context.SaveChangesAsync();
        }
    }
}

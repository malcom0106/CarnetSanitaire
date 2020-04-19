using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddLogErreur(Exception ex)
        {
            string message = ex.Message;
            LogErreur logErreur = new LogErreur();
            logErreur.MessageErreur = message;
            logErreur.DateErreur = DateTime.Now;

            _context.LogErreurs.Add(logErreur);
            await _context.SaveChangesAsync();
        }
    }
}

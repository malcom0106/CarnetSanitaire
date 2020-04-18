using CarnetSanitaire.Web.UI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataSociete : DataAccess
    {
        public DataSociete(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Societe>> GetSocietes()
        {
            List<Societe> societes = null;
            try
            {
                var listesociete = _context.Societes.Include(s => s.Coordonnee);
                return await listesociete.ToListAsync();
            }
            catch (Exception ex)
            {
                return societes;
                throw ex;
            }
        }

        public async Task<Societe> GetSocieteById(int id)
        {
            Societe societe = null;
            try
            {
                societe = await _context.Societes
                .Include(s => s.Coordonnee)
                .FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return societe;
        }
    }
}

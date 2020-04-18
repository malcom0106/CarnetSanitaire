using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

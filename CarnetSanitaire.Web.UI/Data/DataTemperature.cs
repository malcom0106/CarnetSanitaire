using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataTemperature : DataAccess
    {
        public DataTemperature(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<List<PointReleveTemperature>> GetPointReleveTemperatures()
        {
            List<PointReleveTemperature> PointReleveTemperatures = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                PointReleveTemperatures = etablissement.PointReleveTemperatures.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PointReleveTemperatures;
        }



        public bool PointReleveTemperatureExists(int id)
        {
            return _context.PointReleveTemperatures.Any(e => e.Id == id);
        }
    }
}

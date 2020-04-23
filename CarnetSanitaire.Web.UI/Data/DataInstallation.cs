using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataInstallation : DataAccess
    {
        public DataInstallation(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<Installation> GetInstallation()
        {
            Installation installation = null;
            try
            {
                Etablissement etablissement = await this.GetEtablissementByUser();
                //installation = await _context.Installations
                   //.Include(i => i.Production);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return installation;
        }
    }
}

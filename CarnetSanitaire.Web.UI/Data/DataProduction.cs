using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataProduction : DataAccess
    {
        public DataProduction(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<bool> VerifyProductionInstallation()
        {
            bool isVerified = false;
            Production production = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();

                production = etablissement?.Installation?.Production;
                if(production != null)
                {
                    isVerified = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isVerified;
        }

        public async Task<Production> GetProduction()
        {
            Production production = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                production = await _context.Productions
                    .Include(p => p.TypeProduction)
                    .Include(p => p.TypeReseau)
                    .Where(p => p.InstallationId == etablissement.Installation.Id).FirstOrDefaultAsync();                
            }
            catch(Exception ex)
            {
                throw ex;
            } 
            return production;
        }

        public async Task<Production> AddProduction(ModelViewProduction modelViewProduction)
        {
            Production production = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                if(etablissement.Installation.Production == null)
                {
                    production = new Production()
                    {
                        Identification = modelViewProduction.Identification,
                        NombreBallon = modelViewProduction.NombreBallon,
                        TemperatureBouclageEcs = modelViewProduction.TemperatureBouclageEcs,
                        TemperatureDepartEcs = modelViewProduction.TemperatureDepartEcs,
                        InstallationId = etablissement.Installation.Id
                    };
                    production.TypeProduction = await _context.TypeProductions.FindAsync(modelViewProduction.TypeProductionId);
                    production.TypeReseau = await _context.TypeReseaus.FindAsync(modelViewProduction.TypeReseauId);

                    _context.Add(production);
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return production;
        }

    }
}

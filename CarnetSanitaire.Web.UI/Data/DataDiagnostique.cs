using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataDiagnostique : DataAccess
    {
        public DataDiagnostique(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<bool> VerifyInstallationUser(int installationId)
        {
            bool isVerified = false;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                if(etablissement.Installation.Id == installationId)
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

        public async Task<List<Diagnostique>> GetDiagnostiques(int installationId)
        {
            List<Diagnostique> diagnostiques = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                if(etablissement.Installation.Id == installationId)
                {
                    Installation installation = await
                        _context.Installations
                        .Include(d => d.Diagnostiques)
                        .Where(d => d.Id == installationId)
                        .FirstOrDefaultAsync();
                    if(installation.Diagnostiques.Count > 0)
                    {
                        diagnostiques = installation.Diagnostiques;
                    }
                    else
                    {
                        diagnostiques = new List<Diagnostique>();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return diagnostiques;
        }

        public async Task<Diagnostique> GetDiagnostiqueById(int diagnostiqueId)
        {
            Diagnostique diagnostique = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                Diagnostique diagnostiqueVerif = await _context.Diagnostiques.FindAsync(diagnostiqueId);
                if(diagnostiqueVerif.InstallationId == etablissement.Installation.Id)
                {
                    diagnostique = diagnostiqueVerif;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return diagnostique;
        }

        public async Task<Diagnostique> AddDiagnostique(Diagnostique diagnostique)
        {
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                diagnostique.InstallationId = etablissement.Installation.Id;
                _context.Diagnostiques.Add(diagnostique);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return diagnostique;
        }



        public async Task<Diagnostique> EditDiagnostique(Diagnostique diagnostique)
        {
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                if(diagnostique.InstallationId == etablissement.Installation.Id)
                {
                    _context.Diagnostiques.Update(diagnostique);
                    await _context.SaveChangesAsync();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return diagnostique;
        }

        public bool DiagnostiqueExists(int id)
        {
            return _context.Diagnostiques.Any(e => e.Id == id);
        }
    }
}

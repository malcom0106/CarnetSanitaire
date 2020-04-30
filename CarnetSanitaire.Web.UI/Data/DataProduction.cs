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
        private readonly DataPoco _dataPoco;
        public DataProduction(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, DataPoco dataPoco) : base(context, httpContextAccessor)
        {
            _dataPoco = dataPoco;
        }

        public async Task<bool> VerifyProductionInstallation()
        {
            bool isVerified = false;
            Production production;
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

        public async Task<ModelViewProduction> GetProductionModelView()
        {
            ModelViewProduction modelViewProduction = null;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                Production production = await _context.Productions
                    .Include(p => p.TypeProduction)
                    .Include(p => p.TypeReseau)
                    .Where(p => p.InstallationId == etablissement.Installation.Id).FirstOrDefaultAsync();

                modelViewProduction = new ModelViewProduction()
                {
                    Id = production.Id,
                    Identification = production.Identification,
                    InstallationId = production.InstallationId,
                    NombreBallon = production.NombreBallon,
                    TemperatureBouclageEcs = production.TemperatureBouclageEcs,
                    TemperatureDepartEcs = production.TemperatureDepartEcs,
                    TypeProductionId = production.TypeProduction.Id,
                    TypeReseauId = production.TypeReseau.Id
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelViewProduction;
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

        public async Task<bool> EditProduction(ModelViewProduction modelViewProduction)
        {
            bool IsEdited = false;

            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                if(etablissement.Installation.Production.Id == modelViewProduction.Id)
                {
                    Production production = await _context.Productions
                        .Include(p => p.TypeProduction)
                        .Include(p => p.TypeReseau)
                        .Where(p => p.Id == modelViewProduction.Id)
                        .FirstOrDefaultAsync();

                    List<TypeProduction> typeProductions = await _dataPoco.GetTypeProduction();
                    List<TypeReseau> typeReseaus = await _dataPoco.GetTypeReseau();

                    production.Identification = modelViewProduction.Identification;
                    production.InstallationId = etablissement.Installation.Id;
                    production.NombreBallon = modelViewProduction.NombreBallon;
                    production.TemperatureBouclageEcs = modelViewProduction.TemperatureBouclageEcs;
                    production.TemperatureDepartEcs = modelViewProduction.TemperatureDepartEcs;
                    
                    production.TypeProduction = typeProductions.Where(p => p.Id == modelViewProduction.TypeProductionId).FirstOrDefault();                    
                    production.TypeReseau = typeReseaus.Where(p => p.Id == modelViewProduction.TypeReseauId).FirstOrDefault();

                    _context.Update(production);
                    await _context.SaveChangesAsync();
                    IsEdited = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return IsEdited;
        }

        public bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }
    }
}

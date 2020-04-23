using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataPoco : DataAccess
    {
        public DataPoco(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<List<Materiau>> GetMateriaux()
        {
            List<Materiau> maliste = null;
            try
            {
                maliste = await _context.Materiaus.Where(m => m.Statut == true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<TypeCalorifugeage>> GetTypeCalorifugeage()
        {
            List<TypeCalorifugeage> maliste = null;
            try
            {
                maliste = await _context.TypeCalorifugeages.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }


        public async Task<List<TypeIntervention>> GetTypeIntervention()
        {
            List<TypeIntervention> maliste = null;
            try
            {
                maliste = await _context.TypeInterventions.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<TypePoint>> GetTypePoint()
        {
            List<TypePoint> maliste = null;
            try
            {
                maliste = await _context.TypePoints.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<TypeProduction>> GetTypeProduction()
        {
            List<TypeProduction> maliste = null;
            try
            {
                maliste = await _context.TypeProductions.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<TypeReseau>> GetTypeReseau()
        {
            List<TypeReseau> maliste = null;
            try
            {
                maliste = await _context.TypeReseaus.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<TypeTraitement>> GetTypeTraitement()
        {
            List<TypeTraitement> maliste = null;
            try
            {
                maliste = await _context.TypeTraitements.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<ProduitTraitement>> GetProduitTraitement()
        {
            List<ProduitTraitement> maliste = null;
            try
            {
                maliste = await _context.ProduitTraitements.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }

        public async Task<List<Domaine>> GetDomaine()
        {
            List<Domaine> maliste = null;
            try
            {
                maliste = await _context.Domaines.Where(m => m.Statut == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maliste;
        }
    }
}

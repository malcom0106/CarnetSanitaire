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
            List<PointReleveTemperature> PointReleveTemperatures;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                PointReleveTemperatures = etablissement.PointReleveTemperatures.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PointReleveTemperatures;
        }

        public async Task<bool> AddPointReleveTemperature(PointReleveTemperature pointReleveTemperature)
        {
            bool isCreated;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                pointReleveTemperature.Etablissement = etablissement;
                _context.Add(pointReleveTemperature);
                await _context.SaveChangesAsync();
                isCreated = true;
            }
            catch (Exception ex)
            {
                isCreated = false
                throw ex;

            }
            return isCreated;
        }

        public async Task<bool> EditPointReleveTemperature(PointReleveTemperature pointReleveTemperature)
        {
            bool IsEdited = false;
            try
            {
                Etablissement etablissement = await GetEtablissementByUser();
                if(pointReleveTemperature.Etablissement.Id == etablissement.Id)
                {

                    await _context.SaveChangesAsync();
                    IsEdited = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsEdited;
        }

        public bool PointReleveTemperatureExists(int id)
        {
            return _context.PointReleveTemperatures.Any(e => e.Id == id);
        }
    }
}

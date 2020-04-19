using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataVerification : DataAccess
    {
        #region Contrcteur et global
        private readonly DataPersonnel _dataPersonnel;
        private readonly DataEtablissement _dataEtablissement;
        public DataVerification(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, DataPersonnel dataPersonnel, DataEtablissement dataEtablissement) : base(context, httpContextAccessor)
        {
            _dataEtablissement = dataEtablissement;
            _dataPersonnel = dataPersonnel;
        }
        #endregion

        public async Task<bool> VerifySocieteInEtablissement(int? societeId)
        {
            bool IsVerified = false;
            Etablissement etablissement = await _dataEtablissement.GetEtablissementByUser();
            Societe societeEtablissement = etablissement.Societes.FirstOrDefault(s => s.Id == societeId);
            if (societeEtablissement != null)
            {
                IsVerified = true;
            }
            return IsVerified;
        }

        public async Task<bool> VerifyPersonnelSocieteInEtablissement(int? personnelId)
        {
            bool IsVerified = false;
            Etablissement etablissement = await _dataEtablissement.GetEtablissementByUser();
            Personnel personnel = await _context.Personnels.Include(p => p.Societe).FirstOrDefaultAsync(p => p.Id == personnelId);
            Societe societeEtablissement = etablissement.Societes.FirstOrDefault(s => s.Id == personnel.Societe.Id);
            if (societeEtablissement != null)
            {
                IsVerified = true;
            }

            return IsVerified;
        }
    }
}

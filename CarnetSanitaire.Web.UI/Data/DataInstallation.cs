using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataInstallation : DataAccess
    {
        private readonly DataPoco _dataPoco;
        public DataInstallation(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, DataPoco dataPoco) : base(context, httpContextAccessor)
        {
            _dataPoco = dataPoco;
        }

        public async Task<Installation> GetInstallation()
        {
            Installation installation = null;
            try
            {
                Etablissement etablissement = await this.GetEtablissementByUser();
                if(etablissement.Installation != null)
                {
                    installation = await _context.Installations
                   .Include(i => i.Production)
                   .FirstOrDefaultAsync(i => i.Id == etablissement.Installation.Id);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return installation;
        }


        public async Task CreateInstallation(ModelViewInstallation modelView)
        {
            Installation installation = null;
            try
            {
                Etablissement etablissement = await this.GetEtablissementByUser();
                if (etablissement.Installation == null)
                {
                    installation = new Installation() {
                        Interconnexion_Existance = modelView.Interconnexion_Existance,
                        InterconnexionType = modelView.InterconnexionType,
                        DispositifProtectionRetourEau = modelView.DispositifProtectionRetourEau
                    };
                    List<TypeCalorifugeage> typeCalorifugeages = await _dataPoco.GetTypeCalorifugeage();
                    TypeCalorifugeage typeCalorifugeageEfs = typeCalorifugeages.Where(c=>c.Id == modelView.CalorifugeageEfId).FirstOrDefault();
                    TypeCalorifugeage typeCalorifugeageEcs = typeCalorifugeages.Where(c => c.Id == modelView.CalorifugeageEcsId).FirstOrDefault();

                    installation.CalorifugeageEcs = typeCalorifugeageEcs;
                    installation.CalorifugeageEf = typeCalorifugeageEfs;

                    List<Materiau> materiaux = await _dataPoco.GetMateriaux();
                    List<Materiau> mesMateriaux = new List<Materiau>();
                    foreach(int materiauId in modelView.Materiaux)
                    {
                        Materiau materiau = materiaux.Where(c => c.Id == materiauId).FirstOrDefault();
                        mesMateriaux.Add(materiau);
                    }
                    installation.Materiaux = mesMateriaux;
                    etablissement.Installation = installation;
                    _context.Update(etablissement);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


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
                if (etablissement.Installation != null)
                {
                    installation = await _context.Installations
                   .Include(i => i.Production)
                   .Include(i => i.Diagnostiques)
                   .Include(i => i.CalorifugeageEcs)
                   .Include(i => i.CalorifugeageEf)
                   .Include(i => i.InstallationMateriaus).ThenInclude(im => im.Materiau)
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
                    installation = new Installation()
                    {
                        Interconnexion_Existance = modelView.Interconnexion_Existance,
                        InterconnexionType = modelView.InterconnexionType,
                        DispositifProtectionRetourEau = modelView.DispositifProtectionRetourEau
                    };
                    List<TypeCalorifugeage> typeCalorifugeages = await _dataPoco.GetTypeCalorifugeage();
                    TypeCalorifugeage typeCalorifugeageEfs = typeCalorifugeages.Where(c => c.Id == modelView.CalorifugeageEfId).FirstOrDefault();
                    TypeCalorifugeage typeCalorifugeageEcs = typeCalorifugeages.Where(c => c.Id == modelView.CalorifugeageEcsId).FirstOrDefault();

                    installation.CalorifugeageEcs = typeCalorifugeageEcs;
                    installation.CalorifugeageEf = typeCalorifugeageEfs;
                    _context.Add(installation);
                    await _context.SaveChangesAsync();

                    List<InstallationMateriau> installationMateriaux = new List<InstallationMateriau>();
                    InstallationMateriau installationMateriau;
                    
                    foreach (int materiauId in modelView.Materiaux)
                    {
                        installationMateriau = new InstallationMateriau
                        {
                            MateriauId = materiauId,
                            InstallationId = installation.Id
                        };

                        installationMateriaux.Add(installationMateriau);
                    }
                    installation.InstallationMateriaus = installationMateriaux;
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


        public async Task EditInstallation(ModelViewInstallation modelView)
        {
            Installation installation = null;
            try
            {
                Etablissement etablissement = await this.GetEtablissementByUser();
                if (etablissement.Installation != null && etablissement.Installation.Id == modelView.Id)
                {
                    installation = await _context.Installations
                   .Include(i => i.Production)
                   .Include(i => i.Diagnostiques)
                   .Include(i => i.CalorifugeageEcs)
                   .Include(i => i.CalorifugeageEf)
                   .Include(i => i.InstallationMateriaus)
                   .FirstOrDefaultAsync(i => i.Id == etablissement.Installation.Id);

                    installation.Interconnexion_Existance = modelView.Interconnexion_Existance;
                    installation.InterconnexionType = modelView.InterconnexionType;
                    installation.DispositifProtectionRetourEau = modelView.DispositifProtectionRetourEau;

                    List<TypeCalorifugeage> typeCalorifugeages = await _dataPoco.GetTypeCalorifugeage();
                    TypeCalorifugeage typeCalorifugeageEfs = typeCalorifugeages.Where(c => c.Id == modelView.CalorifugeageEfId).FirstOrDefault();
                    TypeCalorifugeage typeCalorifugeageEcs = typeCalorifugeages.Where(c => c.Id == modelView.CalorifugeageEcsId).FirstOrDefault();

                    installation.CalorifugeageEcs = typeCalorifugeageEcs;
                    installation.CalorifugeageEf = typeCalorifugeageEfs;

                    InstallationMateriau installationMateiau = null; 
                    List<InstallationMateriau> mesMateriaux = new List<InstallationMateriau>();

                    foreach (int materiauId in modelView.Materiaux)
                    {
                        installationMateiau = new InstallationMateriau
                        {
                            MateriauId = materiauId,
                            InstallationId = installation.Id
                        };

                        mesMateriaux.Add(installationMateiau);
                    }

                    installation.InstallationMateriaus = mesMateriaux;


                    _context.Update(installation);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ModelViewInstallation> GetInstallationByModelView()
        {
            ModelViewInstallation modelViewInstallation = null;
            try
            {
                Etablissement etablissement = await this.GetEtablissementByUser();
                if (etablissement.Installation != null)
                {
                    Installation installation = await _context.Installations
                   .Include(i => i.Production)
                   .Include(i => i.Diagnostiques)
                   .Include(i => i.CalorifugeageEcs)
                   .Include(i => i.CalorifugeageEf)
                   .Include(i => i.InstallationMateriaus)
                   .FirstOrDefaultAsync(i => i.Id == etablissement.Installation.Id);

                    modelViewInstallation = new ModelViewInstallation()
                    {
                        Id = installation.Id,
                        Diagnostiques = installation.Diagnostiques,
                        Interconnexion_Existance = installation.Interconnexion_Existance,
                        InterconnexionType = installation.InterconnexionType,
                        CalorifugeageEcsId = installation.CalorifugeageEcs.Id,
                        CalorifugeageEfId = installation.CalorifugeageEf.Id,
                        DispositifProtectionRetourEau = installation.DispositifProtectionRetourEau,
                        Traitements = installation.Traitements
                    };

                    List<int> materiauxId = new List<int>();
                    foreach (InstallationMateriau materiau in installation.InstallationMateriaus)
                    {
                        materiauxId.Add(materiau.MateriauId);
                    }
                    modelViewInstallation.Materiaux = materiauxId;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelViewInstallation;
        }

        public bool InstallationExists(int id)
        {
            return _context.Installations.Any(e => e.Id == id);
        }
    }
}


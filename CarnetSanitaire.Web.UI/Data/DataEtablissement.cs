using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataEtablissement : DataAccess
    {
        public DataEtablissement(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<List<Etablissement>> GetEtablissements()
        {
            List<Etablissement> etablissements = null;
            try
            {
                etablissements = await (_context.Etablissements
                    .Include(e => e.Societes)
                    .Include(e => e.Coordonnee))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return etablissements;
        }

        public async Task<Etablissement> GetEtablissementByUser()
        {
            Etablissement etablissement = null;
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Etablissement)
                    .FirstOrDefaultAsync();

                int etablissementId = user.Etablissement.Id;
                etablissement = await _context.Etablissements
                    .Include(e => e.Societes)
                    .Include(e => e.Coordonnee)
                    .Where(e => e.Id == user.Etablissement.Id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return etablissement;
        }

        public async Task<Etablissement> GetEtablissementById(int etablissementId)
        {
            Etablissement etablissement = null;
            try
            {
                etablissement = await _context.Etablissements
                    .Include(e => e.Societes)
                    .Include(e => e.Coordonnee)
                    .Where(e => e.Id == etablissementId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return etablissement;
        }

        public async Task AddEtablissementByModelView(ModelViewEtablissement modelViewEtablissement)
        {
            try
            {
                Coordonnee coordonnee = new Coordonnee
                {
                    Adresse = modelViewEtablissement.Adresse,
                    SubAdresse = modelViewEtablissement.SubAdresse,
                    CodePostal = modelViewEtablissement.CodePostal,
                    Ville = modelViewEtablissement.Ville,
                    Fax = modelViewEtablissement.Fax,
                    Telephone = modelViewEtablissement.Telephone,
                    Email = modelViewEtablissement.Email
                };

                Etablissement etablissement = new Etablissement
                {
                    Nom = modelViewEtablissement.Nom,
                    Capacite = modelViewEtablissement.Capacite,
                    Coordonnee = coordonnee
                };

                _context.Add(etablissement);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<ModelViewEtablissement> GetModelViewEtablissementById(int id)
        {
            ModelViewEtablissement modelViewEtablissement = null;
            try
            {
                Etablissement etablissement = await this.GetEtablissementById(id);

                modelViewEtablissement.Nom = etablissement.Nom;
                modelViewEtablissement.Capacite = etablissement.Capacite;
                modelViewEtablissement.Adresse = etablissement.Coordonnee.Adresse;
                modelViewEtablissement.SubAdresse = etablissement.Coordonnee.SubAdresse;
                modelViewEtablissement.CodePostal = etablissement.Coordonnee.CodePostal;
                modelViewEtablissement.Ville = etablissement.Coordonnee.Ville;
                modelViewEtablissement.Fax = etablissement.Coordonnee.Fax;
                modelViewEtablissement.Telephone = etablissement.Coordonnee.Telephone;
                modelViewEtablissement.Email = etablissement.Coordonnee.Email;
            }
            catch(Exception ex)
            {
                throw ex;
            }


            return modelViewEtablissement;
        }

        public async Task EditEtablissementByModelViewEtablissment(ModelViewEtablissement modelViewEtablissement)
        {
            try
            {
                Etablissement etablissement = await _context.Etablissements
                   .Include(s => s.Coordonnee)
                   .FirstOrDefaultAsync(s => s.Id == modelViewEtablissement.Id);

                etablissement.Nom = modelViewEtablissement.Nom;
                etablissement.Capacite = modelViewEtablissement.Capacite;

                etablissement.Coordonnee.Adresse = modelViewEtablissement.Adresse;
                etablissement.Coordonnee.SubAdresse = modelViewEtablissement.SubAdresse;
                etablissement.Coordonnee.CodePostal = modelViewEtablissement.CodePostal;
                etablissement.Coordonnee.Ville = modelViewEtablissement.Ville;
                etablissement.Coordonnee.Fax = modelViewEtablissement.Fax;
                etablissement.Coordonnee.Telephone = modelViewEtablissement.Telephone;
                etablissement.Coordonnee.Email = modelViewEtablissement.Email;
                _context.Update(etablissement);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool EtablissementExists(int id)
        {
            return _context.Etablissements.Any(e => e.Id == id);
        }
    }
}

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
    public class DataSociete : DataAccess
    {
        private readonly DataVerification _dataVerification;
        private readonly DataEtablissement _dataEtablissement;
        public DataSociete(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, DataVerification dataVerification, DataEtablissement dataEtablissement) : base(context, httpContextAccessor)
        {
            _dataVerification = dataVerification;
            _dataEtablissement = dataEtablissement;
        }

        /// <summary>
        /// Retourne l'ensemble des societe pour l'etablissement courant 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Societe>> GetSocietes()
        {
            List<Societe> societes = null;
            try
            {
                Etablissement etablissement = await _dataEtablissement.GetEtablissementByUser();

                societes = await _context.Societes
                    .Include(s => s.Coordonnee)
                    .Include(s => s.Personnels)
                    .Where(s=>s.EtablissementId == etablissement.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                societes = new List<Societe>();
                throw ex;
            }
            return societes;
        }

        /// <summary>
        /// Retourne une societe grace à son id
        /// </summary>
        /// <param name="id">Id de Societe</param>
        /// <returns>Une societe</returns>
        public async Task<Societe> GetSocieteById(int id)
        {
            Societe societe = null;
            try
            {
                societe = await _context.Societes
                    .Include(s => s.Coordonnee)
                    .Include(s => s.Personnels)
                    .FirstOrDefaultAsync(m => m.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return societe;
        }

        /// <summary>
        /// Ajout une societe ainsi que ses coordonnées
        /// </summary>
        /// <param name="societeModelView">Model de vue compose de societe et de coordonnées</param>
        /// <returns>Rien</returns>
        public async Task AddSocieteByModelView(SocieteModelView societeModelView)
        {
            Etablissement etablissement = await _dataEtablissement.GetEtablissementByUser();
            Coordonnee coordonnee = new Coordonnee();
            coordonnee.Adresse = societeModelView.Adresse;
            coordonnee.SubAdresse = societeModelView.SubAdresse;
            coordonnee.CodePostal = societeModelView.CodePostal;
            coordonnee.Ville = societeModelView.Ville;
            coordonnee.Fax = societeModelView.Fax;
            coordonnee.Telephone = societeModelView.Telephone;
            coordonnee.Email = societeModelView.Email;

            Societe societe = new Societe();
            societe.Nom = societeModelView.Nom;
            societe.EtablissementId = etablissement.Id;
            societe.Coordonnee = coordonnee;

            _context.Add(societe);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retourne un Model de vue incluant la societe et ses coordonnées rencherche par Id de la societe
        /// </summary>
        /// <param name="id">Id de la societe</param>
        /// <returns>Un model de vue SocieteModelView</returns>
        public async Task<SocieteModelView> GetSocieteModelViewById(int? id)
        {
            SocieteModelView societeModelView = null;
            try
            {
                Societe societe = await _context.Societes
                .Include(s => s.Coordonnee)
                .FirstOrDefaultAsync(s => s.Id == id);

                societeModelView = new SocieteModelView();
                societeModelView.Id = societe.Id;
                societeModelView.Nom = societe.Nom;
                societeModelView.Adresse = societe.Coordonnee.Adresse;
                societeModelView.SubAdresse = societe.Coordonnee.SubAdresse;
                societeModelView.CodePostal = societe.Coordonnee.CodePostal;
                societeModelView.Ville = societe.Coordonnee.Ville;
                societeModelView.Fax = societe.Coordonnee.Fax;
                societeModelView.Telephone = societe.Coordonnee.Telephone;
                societeModelView.Email = societe.Coordonnee.Email;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return societeModelView;
        }

        /// <summary>
        /// Editer un Modele de Vue de Type SocieteModelView
        /// </summary>
        /// <param name="societeModelView">Model de vue compose de societe et de coordonnées</param>
        /// <returns>Rien</returns>
        public async Task EditSocieteByModel(SocieteModelView societeModelView)
        {
            try
            {
                Societe societe = await _context.Societes
                    .Include(s => s.Coordonnee)
                    .FirstOrDefaultAsync(s => s.Id == societeModelView.Id);

                societe.Nom = societeModelView.Nom;

                societe.Coordonnee.Adresse = societeModelView.Adresse;
                societe.Coordonnee.SubAdresse = societeModelView.SubAdresse;
                societe.Coordonnee.CodePostal = societeModelView.CodePostal;
                societe.Coordonnee.Ville = societeModelView.Ville;
                societe.Coordonnee.Fax = societeModelView.Fax;
                societe.Coordonnee.Telephone = societeModelView.Telephone;
                societe.Coordonnee.Email = societeModelView.Email;
                _context.Update(societe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

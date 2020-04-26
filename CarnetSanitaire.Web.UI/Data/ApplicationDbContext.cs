using System;
using System.Collections.Generic;
using System.Text;
using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarnetSanitaire.Web.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //DbSet des tables Principales
        public DbSet<Coordonnee> Coordonnees { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Domaine> Domaines { get; set; }
        public DbSet<Societe> Societes { get; set; }
        public DbSet<LogErreur> LogErreurs { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Materiau> Materiaus { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProduitTraitement> ProduitTraitements { get; set; }
        public DbSet<Traitement> Traitements { get; set; }
        public DbSet<Travail> Travails { get; set; }
        public DbSet<TypeIntervention> TypeInterventions { get; set; }
        public DbSet<TypeProduction> TypeProductions { get; set; }
        public DbSet<TypeTraitement> TypeTraitements { get; set; }
        public DbSet<TypeReseau> TypeReseaus { get; set; }
        public DbSet<TypePoint> TypePoints { get; set; }
        public DbSet<ReleveTemperature> ReleveTemperatures { get; set; }
        public DbSet<PointReleveTemperature> PointReleveTemperatures { get; set; }
        public DbSet<CampagneAnalyse> CampagneAnalyses { get; set; }
        public DbSet<Prelevement> Prelevements { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<ParametreAnalyse> ParametreAnalyses { get; set; }
        public DbSet<TypeCalorifugeage> TypeCalorifugeages { get; set; }
        public DbSet<Diagnostique> Diagnostiques { get; set; }

        
        //DbSet Table Intermediare
        public DbSet<SocieteDomaine> SocieteDomaines { get; set; }
        public DbSet<InstallationMateriau> InstallationMateiaus { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Relation N-N avec tables intermediare Installation et Materiaux

            builder.Entity<InstallationMateriau>()
                .HasKey(im => new { im.MateriauId, im.InstallationId });

            builder.Entity<InstallationMateriau>()
                .HasOne(im => im.Materiau)
                .WithMany(im => im.InstallationMateiaus)
                .HasForeignKey(im=>im.MateriauId);

            builder.Entity<InstallationMateriau>()
                .HasOne(im => im.Installation)
                .WithMany(im => im.InstallationMateiaus)
                .HasForeignKey(im=>im.InstallationId);


            //Relation N-N avec tables intermediare Societes et Domaines
            builder.Entity<SocieteDomaine>()
                .HasKey(sd => new { sd.DomaineId, sd.SocieteId });

            builder.Entity<SocieteDomaine>()
                .HasOne(bc => bc.Societe)
                .WithMany(b => b.SocieteDomaines)
                .HasForeignKey(bc => bc.SocieteId);

            builder.Entity<SocieteDomaine>()
                .HasOne(bc => bc.Domaine)
                .WithMany(c => c.SocieteDomaines)
                .HasForeignKey(bc => bc.DomaineId);


            //Ajouter toutes les propriétés de navigation de l’utilisateur
            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}

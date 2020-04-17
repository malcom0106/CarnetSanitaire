using System;
using System.Collections.Generic;
using System.Text;
using CarnetSanitaire.Web.UI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarnetSanitaire.Web.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Coordonnee> Coordonnees { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }
    }
}

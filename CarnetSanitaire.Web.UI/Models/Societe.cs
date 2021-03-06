﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Societe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la société est requis")]
        public string Nom { get; set; }
        
        public Coordonnee Coordonnee { get; set; }
        public ICollection<SocieteDomaine> SocieteDomaines { get; set; }
        public ICollection<Personnel> Personnels { get; set; }
        public int EtablissementId { get; set; }
        [ForeignKey("EtablissementId")]
        public Etablissement Etablissement { get; set; }

        
    }
}

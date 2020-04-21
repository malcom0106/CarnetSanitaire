using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Etablissement
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de l'établissement est requis")]
        [StringLength(50, MinimumLength =3)]
        public string Nom { get; set; }

        [Range(1,1000)]
        public int Capacite { get; set; }

        public int CoordonneeId { get; set; }

        [ForeignKey("CoordonneeId")]
        public Coordonnee Coordonnee { get; set;  }

        public ICollection<Societe> Societes { get; set; }

        public ICollection<Intervention> Interventions { get; set; }
        public ICollection<ReleveTemperature> ReleveTemperatures { get; set; }

        public Installation Installation { get; set; }
    }
}

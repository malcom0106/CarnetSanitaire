using System;
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

        public int CoordonneeId { get; set; }
        [ForeignKey("CoordonneeId")]
        public Coordonnee Coordonnee { get; set; }
        public ICollection<Domaine> Domaines { get; set; }
        public ICollection<Personnel> Personnels { get; set; }

        
    }
}

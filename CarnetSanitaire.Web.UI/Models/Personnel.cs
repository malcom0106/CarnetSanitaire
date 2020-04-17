using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Personnel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le Nom est requis")]
        [StringLength(50, MinimumLength =2)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le Prénom est requis")]
        [StringLength(50, MinimumLength = 2)]
        public string Prenom { get; set; }
        public virtual ICollection<Etablissement> Etablissements { get; set; }
    }
}

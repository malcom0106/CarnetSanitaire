using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Domaine
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Le nom du domaine est requis")]
        public string Nom { get; set; }
        public ICollection<SocieteDomaine> SocieteDomaines { get; set; }
        public bool Statut { get; set; }
    }
}

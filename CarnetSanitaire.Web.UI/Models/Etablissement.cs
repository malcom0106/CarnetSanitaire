using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Etablissement
    {
        public int Id { get; set; }
        public int Nom { get; set; }
        public int Capacite { get; set; }
        public int IdCoordonnee { get; set; }
        [ForeignKey("IdCoordonnee")]
        public Coordonnee Coordonnee { get; set;  }

    }
}

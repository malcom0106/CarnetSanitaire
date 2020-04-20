using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class ProduitTraitement
    {
        public int Id { get; set; }
        public string nom { get; set; }

        public bool Statut { get; set; }
    }
}

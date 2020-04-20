using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class SocieteDomaine
    {
        public int SocieteId { get; set; }
        public Societe Societe { get; set; }
        public int DomaineId { get; set; }
        public Domaine Domaine { get; set; }
    }
}

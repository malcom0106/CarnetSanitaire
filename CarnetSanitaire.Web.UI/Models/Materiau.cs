using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Materiau
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<InstallationMateriau> InstallationMateriaus { get; set; }
        public bool Statut { get; set; }
    }
}

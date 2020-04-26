using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class InstallationMateriau
    {
        public int InstallationId { get; set; }
        public int MateriauId { get; set; }
        public Installation Installation { get; set; }
        public Materiau Materiau { get; set; }
    }
}

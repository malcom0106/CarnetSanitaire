using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class ParametreAnalyse
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Diminutif { get; set; }
        public string Description { get; set; }
        public bool Statut { get; set; }
    }
}

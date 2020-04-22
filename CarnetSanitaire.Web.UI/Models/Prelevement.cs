using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Prelevement
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public bool PrelevementCofrac {get; set;}
        public List<Analyse> Analyses { get; set; }
        public CampagneAnalyse CampagneAnalyse { get; set; }
    }
}

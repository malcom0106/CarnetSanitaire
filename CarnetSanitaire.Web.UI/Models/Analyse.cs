using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Analyse
    {
        public int Id { get; set; }
        public ParametreAnalyse ParametreAnalyse { get; set; }
        public bool AnalyseCofrac { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Resultat { get; set; }
        public string Unite { get; set; }
        public string Seuil { get; set; }
        public Prelevement Prelevement { get; set; }

   }
}

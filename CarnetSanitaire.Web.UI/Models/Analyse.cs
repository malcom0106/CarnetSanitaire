using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public abstract class Analyse
    {
        public virtual int Id { get; set; }
        public ParametreAnalyse ParametreAnalyse { get; set; }
        public virtual bool AnalyseCofrac { get; set; }
        public virtual decimal Resultat { get; set; }
        public virtual string Unite { get; set; }
        public virtual string Seuil { get; set; }
        public Prelevement Prelevement { get; set; }

    }
}

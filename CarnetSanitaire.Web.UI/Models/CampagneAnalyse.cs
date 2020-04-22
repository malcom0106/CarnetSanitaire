using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class CampagneAnalyse
    {
        public int Id { get; set; }

        public DateTime DateCampagne { get; set; }

        public List<Prelevement> Prelevements { get; set; }
        public Etablissement Etablissement { get; set; }
    }
}

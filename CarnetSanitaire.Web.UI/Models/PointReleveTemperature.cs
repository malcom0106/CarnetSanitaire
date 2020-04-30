using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class PointReleveTemperature
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Localisation { get; set; }
        public int TypePointId { get; set; }
        [ForeignKey("TypePointId")]
        public TypePoint TypePoint { get; set; }
        public List<ReleveTemperature> ReleveTemperatures { get; set; }
        public bool Statut { get; set; }
        public Etablissement Etablissement { get; set; }
    }
}

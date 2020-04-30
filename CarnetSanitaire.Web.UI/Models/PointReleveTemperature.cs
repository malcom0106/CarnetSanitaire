using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class PointReleveTemperature
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Localisation { get; set; }
        [Required]
        public int TypePointId { get; set; }
        [ForeignKey("TypePointId")]
        public TypePoint TypePoint { get; set; }
        public List<ReleveTemperature> ReleveTemperatures { get; set; }
        [Required]
        public bool Statut { get; set; }
        [Required]
        public Etablissement Etablissement { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class ReleveTemperature
    {
        public int Id { get; set; }
        public DateTime DateReleve { get; set; }
        public PointReleveTemperature PointReleveTemperature { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal Temperature { get; set; }
        public Etablissement Etablissement { get; set; }
    }
}

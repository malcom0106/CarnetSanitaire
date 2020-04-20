using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public DateTime DateIntervention { get; set; }
        public string Description { get; set; }
        public int Intervenant { get; set; }
        public string Lieu { get; set; }
        public TypeIntervention TypeIntervention { get; set; }
        public Etablissement Etablissement { get; set; }
    }
}

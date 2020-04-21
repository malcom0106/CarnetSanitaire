using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class PointReleveTemperature
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Localisation { get; set; }
        public TypePoint TypePoint { get; set; }
        public bool Statut { get; set; }
    }
}

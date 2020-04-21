using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Travail
    {
        public int Id { get; set; }
        public DateTime DateTravaux { get; set; }
        public int InstallationId { get; set; }
        [ForeignKey("InstallationId")]
        public Installation Installation { get; set; }

        public int PersonnelId { get; set; }
        [ForeignKey("PersonnelId")]
        public Personnel Personnel { get; set; }
    }
}

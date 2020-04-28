using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    [NotMapped]
    public class ModelViewProduction
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public int TypeProductionId { get; set; }
        public int NombreBallon { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal TemperatureDepartEcs { get; set; }

        [Display(Name = "Type de Reseau")]
        public int TypeReseauId { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal TemperatureBouclageEcs { get; set; }
        public int InstallationId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Production
    {
        public int Id { get; set; }
        public TypeProduction TypeProduction { get; set; }
        public int NombreBallon { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal TemperatureDepartEcs { get; set; }

        [Display(Name = "Type de Reseau")]
        public TypeReseau TypeReseau { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal TemperatureBouclageEcs { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Traitement
    {
        public int Id { get; set; }
        public TypeTraitement TypeTraitement { get; set; }
        public ProduitTraitement ProduitTraitement { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Dosage { get; set; }
    }
}

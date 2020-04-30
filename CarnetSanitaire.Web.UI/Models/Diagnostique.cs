using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Diagnostique
    {
        #region Propriete Diag
        public int Id { get; set; }
        
        [Display(Name = "Diagnostique Réalisé")]
        public bool Diagnostique_Realise { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date du Diagnostique")]
        public DateTime? Diagnostique_Date { get; set; }

        [Required]
        [Display(Name = "Intervenant")]
        public int? Diagnostique_Intervenant { get; set; }

        public int InstallationId { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    [NotMapped]
    public class ModelViewInstallation
    {
        public int Id { get; set; }
        #region Diag
        public List<Diagnostique> Diagnostiques { get; set; }

        #endregion
        #region Reseau
        [Display(Name = "Materiau(x)")]
        public List<int> Materiaux { get; set; }

        [Display(Name = "Existe-t-il une interconnexion ?")]
        public bool Interconnexion_Existance { get; set; }

        [Display(Name = "Type d'interconnexion ")]
        public string InterconnexionType { get; set; }

        [Display(Name = "Calorifugeage EFS")]
        public int? CalorifugeageEfId { get; set; }

        [Display(Name = "Calorifugeage ECS")]
        public int? CalorifugeageEcsId { get; set; }

        #endregion

        #region Traitement
        [Display(Name = "Traitement mise en place")]
        public List<Traitement> Traitements { get; set; }

        [Display(Name = "Dispositif de Protection contre les retours d'eau ")]
        public bool DispositifProtectionRetourEau { get; set; }
        #endregion
    }
}

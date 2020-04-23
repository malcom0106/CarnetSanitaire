using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class ModelViewInstallation
    {
        public int Id { get; set; }
        #region Diag
        [Display(Name = "Diagnostique Réalisé")]
        public bool Diagnostique_Realise { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date du Diagnostique")]
        public DateTime? Diagnostique_Date { get; set; }

        [Display(Name = "Intervenant")]
        public int? Diagnostique_Intervenant { get; set; }

        #endregion
        #region Reseau
        [Display(Name = "Materiau(x)")]
        public List<int> Materiaux { get; set; }

        [Display(Name = "Existe-t-il une interconnexion ?")]
        public bool Interconnexion_Existance { get; set; }

        [Display(Name = "Type d'interconnexion ")]
        public string? InterconnexionType { get; set; }

        [Display(Name = "Calorifugeage EFS")]
        public int? CalorifugeageEfId { get; set; }

        [Display(Name = "Calorifugeage ECS")]
        public int? CalorifugeageEcsId { get; set; }

        [Display(Name = "Type de Reseau")]
        public TypeReseau TypeReseau { get; set; }
        #endregion

        #region Production
        [Display(Name = "Production")]
        public int? ProductionId { get; set; }
        #endregion

        #region Traitement
        [Display(Name = "Traitement mise en place")]
        public List<Traitement> Traitements { get; set; }

        [Display(Name = "Dispositif de Protection contre les retours d'eau ")]
        #endregion
        public bool DispositifProtectionRetourEau { get; set; }
    }
}

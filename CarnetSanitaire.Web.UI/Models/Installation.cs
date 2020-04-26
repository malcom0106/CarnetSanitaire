using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Installation
    {
        public int Id { get; set; }
        
        #region Reseau
        [Display(Name = "Materiau(x)")]
        public List<InstallationMateriau> InstallationMateiaus { get; set; }

        [Display(Name = "Existe-t-il une interconnexion ?")]
        public bool Interconnexion_Existance { get; set; }

        [Display(Name = "Type d'interconnexion ")]
        public string InterconnexionType { get; set; }

        [Display(Name = "Calorifugeage EFS")]
        public TypeCalorifugeage CalorifugeageEf { get; set; }

        [Display(Name = "Calorifugeage ECS")]
        public TypeCalorifugeage CalorifugeageEcs { get; set; }
        #endregion

        #region Production
        [Display(Name = "Production")]
        public int? ProductionId { get; set; }
        [ForeignKey("ProductionId")]
        public Production Production { get; set; }
        #endregion

        #region Traitement
        [Display(Name = "Traitement mise en place")]
        public List<Traitement> Traitements { get; set; }

        [Display(Name = "Dispositif de Protection contre les retours d'eau ")]        
        public bool DispositifProtectionRetourEau { get; set; }
        #endregion
        #region Diagnostique
        public List<Diagnostique> Diagnostique { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Installation
    {
        public int Id { get; set; }
        #region Diag
        public bool Diagnostique_Realise { get; set; }
        public DateTime Diagnostique_Date { get; set; }
        public int Diagnostique_Intervenant { get; set; }
        #endregion
        #region Reseau
        public ICollection<InstallationMateriau> InstallationMateriaus { get; set; }
        public bool Interconnexion_Existance { get; set; }
        public string InterconnexionType { get; set; }
        public int CalorifugeageEf { get; set; }
        public int CalorifugeageEcs { get; set; }
        public TypeReseau TypeReseau { get; set; }        
        #endregion

        #region Production
        public int ProductionId { get; set; }
        [ForeignKey("ProductionId")]
        public Production Production { get; set; }
        #endregion

        #region Traitement
        public List<Traitement> Traitements { get; set; }
        #endregion
        public bool DispositifProtectionRetourEau { get; set; }
        public Etablissement Etablissement { get; set; }

    }
}

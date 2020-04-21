using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Personnel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le Nom est requis")]
        [StringLength(50, MinimumLength =2)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le Prénom est requis")]
        [StringLength(50, MinimumLength = 2)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le téléphone est requis")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "L'Email est requis")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }        
        
        public int SocieteId { get; set; }
        [ForeignKey("SocieteId")]
        public Societe Societe { get; set; }
        public bool Statut { get; set; }
    }
}

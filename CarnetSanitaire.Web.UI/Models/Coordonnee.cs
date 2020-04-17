using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Coordonnee
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="L'adresse est requise")]
        public string Adresse { get; set; }
        public string SubAdresse { get; set; }
        
        [Required(ErrorMessage ="Le code postal est requis")]
        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; }
        
        [Required(ErrorMessage = "La ville est requise")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "Le Telephone est requis")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [Required(ErrorMessage = "L'Email est requis")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }    

    }
}

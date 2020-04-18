using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    [NotMapped]
    public class SocieteModelView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom de la société est requis")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "L'adresse est requise")]
        public string Adresse { get; set; }
        [Display(Name="Complement d'adresse")]
        public string SubAdresse { get; set; }

        [Required(ErrorMessage = "Le code postal est requis")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Code Postal")]
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

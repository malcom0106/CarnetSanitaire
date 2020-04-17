using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class Coordonnee
    {
        public int Id { get; set; }
        public string Adresse { get; set; }
        public string SubAdresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Models
{
    public class LogErreur
    {
        public int Id { get; set; }
        public string MessageErreur { get; set; }
        public DateTime DateErreur { get; set; }
    }
}

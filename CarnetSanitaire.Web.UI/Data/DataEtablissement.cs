using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataEtablissement : DataAccess
    {
        public DataEtablissement(ApplicationDbContext context) : base(context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataCoordonnee : DataAccess
    {
        public DataCoordonnee(ApplicationDbContext context) : base(context)
        {
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataDomaine : DataAccess
    {
        public DataDomaine(ApplicationDbContext context) : base(context)
        {
        }
    }
}

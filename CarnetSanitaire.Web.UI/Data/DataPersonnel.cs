using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataPersonnel : DataAccess
    {
        public DataPersonnel(ApplicationDbContext context) : base(context)
        {
        }
    }
}

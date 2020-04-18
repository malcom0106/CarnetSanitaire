using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Data
{
    public class DataAccess
    {
        protected readonly ApplicationDbContext _context;

        public DataAccess(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}

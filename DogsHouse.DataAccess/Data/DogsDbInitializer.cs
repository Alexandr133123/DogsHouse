using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.DataAccess.Data
{
    public class DogsDbInitializer
    {
        public static void Initialize(DogsDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.Application.Common.Exceptions
{
    public class SortArgumentNotFoundException : Exception
    {
        public SortArgumentNotFoundException(string attribute) 
            : base($"Sorting attribute ({attribute}) not found.") { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Security
{
    public class JwtClaims
    {
        public string username { get; set; }
        public List<string> permissions { get; set; }

    }
}

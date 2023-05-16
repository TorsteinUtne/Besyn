using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasuraActionExt.Types
{
    public class Auth0Response
    {
            public string access_token { get; set; }
            public string id_token { get; set; }

            public string scope { get; set; }   
            public string expires_in { get;set; }

            public string token_type { get; set; }


    }
}

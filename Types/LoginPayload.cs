using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasuraActionExt.Types
{

    public class LoginPayload
    {
        public Action Action { get; set; }
        public Input Input { get; set; }
        public SessionVariables SessionVariables { get; set; }
        public string RequestQuery { get; set; }
    }

    public class Action
    {
        public string Name { get; set; }
    }

    public class Input
    {
        public Arg1 Arg1;
    }

    public class Arg1
    {
        public string Username;
        public string Password;
    }

    public class SessionVariables
    {
        public string XHasuraUserId { get; set; }
        public string XHasuraRole { get; set; }
    }
}

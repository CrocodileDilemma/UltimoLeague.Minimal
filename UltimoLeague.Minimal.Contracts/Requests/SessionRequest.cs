using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class SessionRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }

    public class SessionRegisterRequest : SessionRequest
    {
        public string ConfirmPassword { get; set; }
    }

}

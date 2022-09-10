using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Models
{
    [Serializable]
    public class RegisterCreds
    {
        public string email;
        public string username;
        public string password;
        public string confirmPassword;
    }
    [Serializable]
    public class LoginCreds
    {
        public string username;
        public string password;
        public string notifytext;
    }
}

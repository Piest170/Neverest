using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Models
{
    [Serializable]
    public class ServiceResponse<T>
    {
        public T? data;
        public bool success;
        public string message;
    }

    [Serializable]
    public class ModelInvalidResponse<T>
    {
        public string type;
        public string title;
        public int status;
        public string traceId;
        public T errors;
    }

    [Serializable]
    public class ModelRegisterInvalidError 
    {
        public string[] Email;
        public string[] Password;
        public string[] Username;
        public string[] ConfirmPassword;

    }
}


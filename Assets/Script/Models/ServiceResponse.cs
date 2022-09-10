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
}

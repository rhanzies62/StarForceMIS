using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.Web.Models
{
    public class Callback<T>
    {
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public T Data { get; set; }
        public void SuccessResult(string message)
        {
            this.Message = message;
            this.Success = true;
        }

        public void FailResult(string message)
        {
            this.Message = message;
            this.Success = false;
        }
    }
}

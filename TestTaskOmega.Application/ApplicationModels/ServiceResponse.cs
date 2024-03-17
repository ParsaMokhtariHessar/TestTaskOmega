using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Application.ApplicationModels
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public ServiceResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}

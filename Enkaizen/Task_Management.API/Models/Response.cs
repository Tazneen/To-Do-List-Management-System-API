using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Management.API.Models
{
    public class Response<T> where T : class
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Arepa.Sample.DataAccess
{
    public class DataResponse<T>
    {
        public T Results { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

    }
}

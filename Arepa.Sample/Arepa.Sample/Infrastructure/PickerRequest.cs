using System;
using System.Collections.Generic;
using System.Text;

namespace Arepa.Sample.Infrastructure
{


    public class PickerRequest : IPickerRequest
    {
        public string Search { get; set; }
        public int Offset { get; set; }
        public int ChunkSize { get; set; }
    }
}

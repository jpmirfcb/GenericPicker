using System;
using System.Collections.Generic;
using System.Text;

namespace Arepa.Sample.Infrastructure
{
    public interface IPickerRequest
    {
        string Search { get; set; }
        int Offset { get; set; }
        int ChunkSize { get; set; }
    }
}

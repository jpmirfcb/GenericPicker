using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Arepa.Sample.Infrastructure
{
    public class PickerResponse<T>
    {
        public bool Success { get; set; }
        public bool HasMore { get; set; }
        public int AffectedRecords { get; set; }
        public IEnumerable<T> Results { get; set; }
        public string Message { get; set; }


        public static PickerResponse<T> Ok(IEnumerable<T> data, bool hasMore = true) =>
            new PickerResponse<T>()
            {
                HasMore = hasMore,
                Results = data,
                Success = true,
                Message = string.Empty
            };

        public static PickerResponse<T> Error(Exception ex)
            =>
                new PickerResponse<T>()
                {
                    HasMore = false,
                    Results = null,
                    Success = false,
                    Message = ex.Message
                };
    }
}

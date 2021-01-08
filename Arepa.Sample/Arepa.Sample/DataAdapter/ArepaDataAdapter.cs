using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Arepa.Sample.DataAccess;
using Arepa.Sample.Infrastructure;
using Arepa.Sample.Infrastructure.POCO;

namespace Arepa.Sample.DataAdapter
{
    public class ArepaDataAdapter : IPickerDataAdapter
    {
        public async Task<DataResponse<IEnumerable>> LoadDataAsync(int offset, string searchText)
        {
            //TODO: Implement your own API client here

            var dataAccess = new ArepaDataAccess();
            var response = await dataAccess.GetList(new PickerRequest()
            {
                Search = searchText,
                Offset = offset,
                ChunkSize = 10
            });
            return response;
        }
    }
}

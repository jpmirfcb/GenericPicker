using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arepa.Sample.DataAccess;
using Arepa.Sample.Infrastructure;

namespace Arepa.Sample.DataAdapter
{
    public interface IPickerDataAdapter
    {
        Task<DataResponse<IEnumerable>> LoadDataAsync(int offset, string searchText);
    }
}

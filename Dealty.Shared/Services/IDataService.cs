using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealty.Shared.Services
{
    public interface IDataService
    {
        Task<List<string>> GetData();
    }
}

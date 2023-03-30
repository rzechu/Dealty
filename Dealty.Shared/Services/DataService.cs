using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealty.Shared.Services
{
    public class DataService : IDataService
    {
        public DataService()
        {
            
        }

        public Task<List<string>> GetData()
        {
            return Task.FromResult(new List<string>()
            {
                "abc", "cde", "efg"
            });
        }
    }
}

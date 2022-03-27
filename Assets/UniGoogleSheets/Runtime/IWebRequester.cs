using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniGS.Runtime
{

    public interface IWebRequester
    {
        Task<string> Get(string uri, string queryParams);
        Task<string> Post(string uri, string queryParams, string body);
    }
}
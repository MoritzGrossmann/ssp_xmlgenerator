using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ssp_xmlgenerator
{
    public class PersonProvider
    {

        private const string Path = "https://randomuser.me/api/";

        private const string nationalities = "us,dk,fr,gb,de";
        public async Task<RootObject> GetRandomUsers(int count) 
        {
            var json = await new RestClient().LoadJson($"{Path}?results={count}&net={nationalities}");
            return JsonConvert.DeserializeObject<RootObject>(json);
        }
    }
}
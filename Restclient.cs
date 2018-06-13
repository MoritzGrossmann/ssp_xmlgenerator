
using System.Net.Http;
using System.Threading.Tasks;

namespace ssp_xmlgenerator 
{
    public class RestClient
    {
        private static readonly HttpClient Client = new HttpClient();
        
        public async Task<string> LoadJson(string path) 
        {
            var result = await Client.GetAsync(path);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
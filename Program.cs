using System;
using System.Threading.Tasks;

namespace ssp_xmlgenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var json = await new PersonProvider().GetRandomUsers(10);
            var xmlWriter = new XmlGenerator("customer.xml", json);
            await xmlWriter.WriteFile();
        }
    }
}

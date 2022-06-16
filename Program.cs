using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.NETFramworkHttp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync("users").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                dynamic data = JsonConvert.DeserializeObject<List<ExpandoObject>>(results);

                
                Console.WriteLine(data[0].name);

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Console.ReadLine();
            }
        }
    }
}

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
            client.BaseAddress = new Uri("https://api.eia.gov/v2/electricity/retail-sales/data/?api_key=Db2HV0bhkALMfJIdjHZ3rArIwIAhzvVUEzvFg6sI");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync("?api_key=Db2HV0bhkALMfJIdjHZ3rArIwIAhzvVUEzvFg6sI&facets[stateid][]=PA&facets[sectorid][]=RES&data[]=price").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                dynamic data = JsonConvert.DeserializeObject<List<ExpandoObject>>("["+results+"]");

                
                 foreach(dynamic var in data[0].response.data)
                {
                    Console.WriteLine(String.Format("{0},{1}", var.period, var.price));
                };
             
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Console.ReadLine();
            }
        }
    }
}

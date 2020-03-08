using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var newPerson = new Person { FirstName = "Marcin" };

            if (args.Length == 0) throw new ArgumentNullException();
            var pattern = new Regex("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$");
            if (!pattern.IsMatch(args[0])) throw new ArgumentException();
            
            
            
            var url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
            
            Console.WriteLine("");
            
           

            using (var Client = new HttpClient())
            {
                try
                {
                    using (var response = await Client.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var htmlContent = await response.Content.ReadAsStringAsync();
                            var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                            var uniqueMatches =regex.Matches(htmlContent)
                                .OfType<Match>()
                                .Select(m => m.Value)
                                .Distinct();
                            if (uniqueMatches.Count() == 0) Console.WriteLine("Nie znaleziono adresow email");

                            foreach (var match in uniqueMatches)
                            {    
                                Console.WriteLine(match);
                            }
                        }
                    }
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Blad w czasie pobierania strony");
                }
            }

          

        }
    }
}

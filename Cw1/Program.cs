﻿using System;
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

            var url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
            
            Console.WriteLine("");
            
           

            using (var Client = new HttpClient())
            {
                using (var response = await Client.GetAsync(url)){ 
                if (response.IsSuccessStatusCode)
                {
                    var htmlContent = await response.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                    var matches = regex.Matches(htmlContent);

                    foreach (var match in matches)
                    {
                        Console.WriteLine(match);
                    }
                }
            }
            }

          

        }
    }
}

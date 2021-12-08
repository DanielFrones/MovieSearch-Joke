using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;


namespace Filmsökning
{
    class Program
    {

        public static HttpClient client = new HttpClient();


        static async Task Main(string[] args)
        {

            DotNetEnv.Env.TraversePath().Load();
            string key = Environment.GetEnvironmentVariable("API_KEY");


            //Console.WriteLine("Skriv in ett ID:");
            //string insert = Console.ReadLine();
            Console.WriteLine("Skriv in en Titel:");
            string insert = Console.ReadLine();

            Console.WriteLine(key);
            //string uri = @$"https://api.themoviedb.org/3/movie/{insert}?api_key={key}";

            string uri2 = @$"https://api.themoviedb.org/3/search/movie?api_key={key}&query={insert}";

            //var response = await client.GetAsync(uri);
            var response2 = await client.GetAsync(uri2);

            //Console.WriteLine(response);
            //response.EnsureSuccessStatusCode();
            response2.EnsureSuccessStatusCode();


            //string responseContent = await response.Content.ReadAsStringAsync();
            string responseContent2 = await response2.Content.ReadAsStringAsync();
            //Console.WriteLine(responseContent);


           

            // IdSearch ID = JsonConvert.DeserializeObject<IdSearch>(responseContent);
            TitleSearch Results = JsonConvert.DeserializeObject<TitleSearch>(responseContent2);

            TitleSearch.Results.Add(Results);
            




            //Console.WriteLine(ID.Original_Title);
            TitleSearch.ShowTitle();


        }

    }
}
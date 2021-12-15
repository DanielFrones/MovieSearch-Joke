using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Filmsökning
{
    class Program
    {

        public static HttpClient client = new HttpClient();

        static async Task Mainmenu()
        {
            int userInput = 0;
            do
            {

                Console.WriteLine("Choose way of search, 1 for movie ID, 2 for movie title:");


                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }

            }
            while (userInput == 0 || userInput > 2);

            switch (userInput)
            {

                case 1:
                    MovieSearchID().Wait();
                    break;
                case 2:
                    MovieSearchTitle().Wait();
                    break;


            }

        }
        static void Main(string[] args)
        {

            Mainmenu();

 







        }
        public static async Task MovieSearchID()
        {
            string path = $"https://image.tmdb.org/t/p/w500/";
            DotNetEnv.Env.TraversePath().Load();
            string key = Environment.GetEnvironmentVariable("API_KEY");
            int movieId = 0;
            Console.WriteLine("Pick an ID:");
            try
            {
                movieId = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                MovieSearchID();

            }

            try
            {
                string uri = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={key}";

                Console.WriteLine(uri);
                var response = await client.GetAsync(uri);


                //Console.WriteLine(response);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseContent);
                Movie ID = JsonConvert.DeserializeObject<Movie>(responseContent);
                Console.WriteLine("Movie ID: " + ID.Id);
                Console.WriteLine("\nOriginal title: " + ID.Original_Title);
                Console.WriteLine("\nOverview: " + ID.Overview);
                Console.WriteLine("\nRuntime: " + ID.Runtime);
                Console.WriteLine("\nRelease date: " + ID.Release_Date);
                Console.WriteLine("\nHomepage: " + ID.Homepage);
                Console.WriteLine("\nVote ,avarage: " + ID.Vote_Avarage);
                Console.WriteLine("\nOriginal language: " + ID.Original_Language);
                Console.WriteLine("\nPoster path: " + $"https://image.tmdb.org/t/p/w500/{ID.Poster_Path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MovieSearchID();
            }


            int userInput = 0;
            do
            {

                Console.WriteLine("To do another search, press 1. \nTo terminate program, press 2.");


                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }

            }
            while (userInput == 0 || userInput > 2);

            switch (userInput)
            {

                case 1:
                    Mainmenu();
                    break;
                case 2:
                    Console.WriteLine("Bye bye..");
                    break;


            }




        }

        public static async Task MovieSearchTitle()
        {
            string path = $"https://image.tmdb.org/t/p/w500/";
            DotNetEnv.Env.TraversePath().Load();
            string key = Environment.GetEnvironmentVariable("API_KEY");

            string movieTitle = "";
            Console.WriteLine("Write a title:");
            try
            {
                movieTitle = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                MovieSearchTitle();
            }





            string uri2 = $"https://api.themoviedb.org/3/search/movie?api_key={key}&query={movieTitle}";

            var response2 = await client.GetAsync(uri2);

            response2.EnsureSuccessStatusCode();

            string responseContent2 = await response2.Content.ReadAsStringAsync();

            TitleSearch SearchRespons = JsonConvert.DeserializeObject<TitleSearch>(responseContent2);
            //Skapar ett objekt som heter searchrespons, som innehåller en lista av filmobjekt.
            // istället för att köra titlesearch searchrespons = new titlesearch(); behöver inte bygga objekten.
            // Blir som en konstruktor som skapar dom här objekten, och mappar upp dom. 

            foreach (var item in SearchRespons.Results)
            {
                Console.Write("Index: " + SearchRespons.Results.IndexOf(item));
                Console.WriteLine(" , Title: " + item.Original_Title + ", MovieID: " + item.Id);
                //Fungerar inte att skriva med index i text...

            }
            int ID = 0;
            bool correct = true;
            while (correct)
            {
                try
                {
                    Console.WriteLine("Write the indexvalue of which movie you want to choose");
                    ID = Convert.ToInt32(Console.ReadLine());
                    correct = false;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }


            Console.Clear();
            Console.WriteLine("You've chosen:");
            Console.WriteLine(SearchRespons.Results[ID].Original_Title);
            Console.WriteLine("For aditional information regarding the movie, press any key.");
            Console.ReadKey();
            Console.WriteLine("\nOriginal title: " + SearchRespons.Results[ID].Original_Title);
            Console.WriteLine("\nOverview: " + SearchRespons.Results[ID].Overview);
            Console.WriteLine("\nRuntime: " + SearchRespons.Results[ID].Runtime);
            Console.WriteLine("\nRelease date: " + SearchRespons.Results[ID].Release_Date);
            Console.WriteLine("\nHomepage: " + SearchRespons.Results[ID].Homepage);
            Console.WriteLine("\nVote ,avarage: " + SearchRespons.Results[ID].Vote_Avarage);
            Console.WriteLine("\nOriginal language: " + SearchRespons.Results[ID].Original_Language);
            Console.WriteLine("\nPoster path: " + $"https://image.tmdb.org/t/p/w500/{SearchRespons.Results[ID].Poster_Path}");


            
            int userInput = 0;
            do
            {

                Console.WriteLine("To do another search, press 1. \nTo terminate program, press 2.");


                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }

            }
            while (userInput == 0 || userInput > 2);

            switch (userInput)
            {

                case 1:
                    Mainmenu();
                    break;
                case 2:
                    Console.WriteLine("Bye bye..");
                    break;


            }

            //TitleSearch.Results.Add(Results);

            //TitleSearch.ShowTitle();

        }


    }
}
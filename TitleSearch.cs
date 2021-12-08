using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmsökning
{
    class TitleSearch
    {
        public static List<TitleSearch> Results { get; set; }

        //testa med api stränge och ändra i program.cs

        public static void ShowTitle()
        {
            foreach (var item in Results)
            {
                Console.WriteLine(item);
            }
        }

    }
}

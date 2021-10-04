using System;

namespace WineCellar
{
    /*
     * Grupp Mjölby
     * Frans Nilsson Lidström, Sahar Ali Abdou
     * Fredrik Viman, Jennifer Björklund
     */

    public enum GrapeVariants
    {
        CabernetSauvignon= 1, PinotNoir, Corvina, Shiraz, Merlot, Chablis,
        Riesling, Tempranillo
    }
    public enum GrapeRegions
    {
        Bordeaux = 1, Burgundy, Veneto, Piedmonte, RiberaDelDuero,
        NapaValley, Puglia, Pfalz
    }
    public struct Wine
    {
        public int? Year;                   // null = undefined
        public string Name;
        public GrapeVariants Grape;
        public GrapeRegions Region;

    }

    class Program
    {
        const int maxNrBottles = 4;

        static Wine[] myCellar = new Wine[maxNrBottles];
        static int nrArticles = 0;
        static int articlesLeft = 0;

//Main
        static void Main(string[] args)
        {
            InsertWine();
            Console.Clear();
            PrintWineList();
            RemoveAnArticle();
        }
//Adding wine to winecellar method
        private static void InsertWine()
        {
            articlesLeft = 0;

            //check how many articles can be added
            for (int i = 0; i < myCellar.Length; i++)
            {
                if (myCellar[i].Name == null)
                {
                    articlesLeft += 1;
                }
            }

            bool isTrue = false;

            //choose how many to add
            do
            {

                Console.WriteLine($"nr of articles 1-{articlesLeft}?");
                try
                {
                    int option = int.Parse(Console.ReadLine());
                    if ((option >= 1) && (option <= articlesLeft))
                    {
                        nrArticles = option;
                        isTrue = true;
                    }
                    else
                    {
                        Console.WriteLine($"not a valid option, you can only choose a number between 1-{articlesLeft}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("not an integer number, try again");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (!isTrue);

            Console.Clear();

            //Displaying the grape and region options to the user
            Console.Write("Regions: ");
            for (int i = 1; i < typeof(GrapeRegions).GetEnumValues().Length; i++)
            {
                Console.Write($"{i}. {(GrapeRegions)Enum.ToObject(typeof(GrapeRegions), i)} ");
            }
            Console.WriteLine();
            Console.Write("Grapes: ");
            for (int i = 1; i < typeof(GrapeVariants).GetEnumValues().Length; i++)
            {
                Console.Write($"{i}. {(GrapeVariants)Enum.ToObject(typeof(GrapeVariants), i)} ");
            }
            Console.WriteLine("\n\nUse format Name Year Grape Region\n");


            /*
             * Running method that adds the wines to the winecellar
             * as many times as the user specified
             */
            for (int i = (maxNrBottles - articlesLeft); i < nrArticles + (maxNrBottles - articlesLeft); i++)
            {
                articleInput(i);
            }
        }

        
//User input method to add myCellar array w max 4 items
         
        private static void articleInput(int articleNr)
        {

            Console.WriteLine($"enter article {articleNr + 1}");
            try
            {
                var articleInput = Console.ReadLine().Split(' ');

                myCellar[articleNr].Name = articleInput[0];
                myCellar[articleNr].Year = int.Parse(articleInput[1]);
                myCellar[articleNr].Grape = (GrapeVariants)int.Parse(articleInput[2]);
                myCellar[articleNr].Region = (GrapeRegions)int.Parse(articleInput[3]);
            }
            catch
            {
                Console.WriteLine("try again");
                articleInput(articleNr);
            }
        }
        
        
//Prints entire wine list
        private static void PrintWineList()
        {
            Console.WriteLine("\n# Name Year Grape Region\n");
            int items = 0;
            foreach (var item in myCellar)
            {
                if (myCellar[items].Name != null)
                {
                    Console.WriteLine($"{items + 1}. {myCellar[items].Name} {myCellar[items].Year} {myCellar[items].Grape} {myCellar[items].Region}");
                    items++;
                }
                else
                {
                    Console.WriteLine($"{items+1}. EMPTY");
                    items++;
                }
            }
        }
//method for removing wines int the myCellar array      
        static int option;
        private static void RemoveAnArticle()
        {
            int count = 0;

            Console.WriteLine("Select to remove ");

            bool isTrue = false;
            do
            {
                try
                {
                
                    int selection = int.Parse(Console.ReadLine());
                    if (selection > 0 && selection < nrArticles + 1)
                    {
                        option = selection;
                        isTrue = true;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid option, try again");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Not an integer number, try again");
                }
            } while (!isTrue);

            
            for (int i = 0; i < nrArticles; i++)
            {
                if (option == i + 1)
                {
                    myCellar[i].Name = null;
                }
            }

            //sorting leaving no emty cells between wines in myCellar array
            count = 0;
            foreach (var item in myCellar)
            {
                if (myCellar[count].Name != null)
                {
                    if (myCellar[count - 1].Name == null && count != 0)
                    {
                        myCellar[count - 1].Name = myCellar[count].Name;
                        myCellar[count].Name = null;
                    }
                }
                count++;
            }
            
            
            Console.Clear();

            PrintWineList();

            Console.ReadKey();
        }
    }
}

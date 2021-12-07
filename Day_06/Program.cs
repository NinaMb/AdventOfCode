using System;
using System.Linq;

using System.Collections.Generic;

namespace Day_06
{
    class Program
    {
        static int[] input_test = { 3, 4, 3, 1, 2 }; 
        static int[] input = { 1, 1, 3, 5, 1, 3, 2, 1, 5, 3, 1, 4, 4, 4, 1, 1, 1, 3, 1, 4, 3, 1, 2, 2, 2, 4, 1, 1, 5, 5, 4, 3, 1, 1, 1, 1, 1, 1, 3, 4, 1, 2, 2, 5, 1, 3, 5, 1, 3, 2, 5, 2, 2, 4, 1, 1, 1, 4, 3, 3, 3, 1, 1, 1, 1, 3, 1, 3, 3, 4, 4, 1, 1, 5, 4, 2, 2, 5, 4, 5, 2, 5, 1, 4, 2, 1, 5, 5, 5, 4, 3, 1, 1, 4, 1, 1, 3, 1, 3, 4, 1, 1, 2, 4, 2, 1, 1, 2, 3, 1, 1, 1, 4, 1, 3, 5, 5, 5, 5, 1, 2, 2, 1, 3, 1, 2, 5, 1, 4, 4, 5, 5, 4, 1, 1, 3, 3, 1, 5, 1, 1, 4, 1, 3, 3, 2, 4, 2, 4, 1, 5, 5, 1, 2, 5, 1, 5, 4, 3, 1, 1, 1, 5, 4, 1, 1, 4, 1, 2, 3, 1, 3, 5, 1, 1, 1, 2, 4, 5, 5, 5, 4, 1, 4, 1, 4, 1, 1, 1, 1, 1, 5, 2, 1, 1, 1, 1, 2, 3, 1, 4, 5, 5, 2, 4, 1, 5, 1, 3, 1, 4, 1, 1, 1, 4, 2, 3, 2, 3, 1, 5, 2, 1, 1, 4, 2, 1, 1, 5, 1, 4, 1, 1, 5, 5, 4, 3, 5, 1, 4, 3, 4, 4, 5, 1, 1, 1, 2, 1, 1, 2, 1, 1, 3, 2, 4, 5, 3, 5, 1, 2, 2, 2, 5, 1, 2, 5, 3, 5, 1, 1, 4, 5, 2, 1, 4, 1, 5, 2, 1, 1, 2, 5, 4, 1, 3, 5, 3, 1, 1, 3, 1, 4, 4, 2, 2, 4, 3, 1, 1 };
        static int DAYS_TOTAL = 256;
        
        static void Main(string[] args)
        {

            Dictionary<int, long> FishKidMap = new Dictionary<int, long>();
            
            for (int i = input.Min(); i <= input.Max(); i++)
            {
                FishKidMap.Add(i, CalcAmountOfFishKids(0, i));
            }


            long sum_fish = 0;
            foreach (var fish in input)
            {
                sum_fish += FishKidMap[fish];
            }

            Console.WriteLine("Fish: " + sum_fish);
        }

        private static long CalcAmountOfFishKids(int day, int counter_adult)
        {
            if (day == DAYS_TOTAL)
            {
                return 1;
            }
            else if (day < DAYS_TOTAL)
            {

                long amount_kids = 0;
                for (int i = day + counter_adult; i <= DAYS_TOTAL; i = i + 7)
                {
                    //Make new fish kid
                    amount_kids += CalcAmountOfFishKids(i + 1, 8);
                }

                return amount_kids + 1;
            }
            else
            {
                return 0;
            }

        }
    }
}

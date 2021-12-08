using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
namespace Day_08
{
    class Program
    {
        const string FILE = "../../../Resources/Input.txt";
        static readonly char[] allChars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

        static void Main(string[] args)
        {

            Console.WriteLine("Hello to Day 08!");

            // PART 1
            string[,] outputValues = GetOutputValues(FILE);
            int count = Count1478(outputValues);

            Console.WriteLine("1,4,7,8 appears " + count + " times.");

            // PART 2

            string[] entries = GetEntries(FILE);
            int[] result = new int[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                Dictionary<char,char> segments = DecodeEntry(entries[i]);

                int resultValue = DecodeOutput(segments, outputValues[i,0], outputValues[i, 1], outputValues[i, 2], outputValues[i, 3]);
                result[i] = resultValue;
            }

            Console.WriteLine("All encoded values summed up gives: " + result.Sum().ToString());
        }

        private static int DecodeOutput(Dictionary<char, char> segment, string output0,string output1, string output2, string output3)
        {
            string resultValue = "";

            foreach (var item in new []{ output0, output1, output2, output3})
            {
                switch (item.Length)
                {
                    case 2:
                        resultValue += "1";
                        break;
                    case 3:
                        resultValue += "7";
                        break;
                    case 4:
                        resultValue += "4";
                        break;
                    case 5:
                        if (!item.Contains(segment['c']))
                        {
                            resultValue += "5";
                        }
                        else if (!item.Contains(segment['b']) && !item.Contains(segment['e']))
                        {
                            resultValue += "3";
                        }
                        else
                        {
                            resultValue += "2";
                        }
                        break;
                    case 6:
                        if (!item.Contains(segment['e']))
                        {
                            resultValue += "9";
                        } else if (!item.Contains(segment['c']))
                        {
                            resultValue += "6";
                        }
                        else
                        {
                            resultValue += "0";
                        }
                        break;
                    case 7:
                        resultValue += "8";
                        break;
                    default:
                        break;
                }

            }

            return Convert.ToInt32(resultValue);
        }

        private static Dictionary<char, char> DecodeEntry(string entry)
        {
            string[] entryItems = GetEntryItems(entry);

            Dictionary<int, char[]> numbers = new Dictionary<int, char[]>();
            Dictionary<char, char> segments = new Dictionary<char, char>();

            var numbers1478 = Get1478(entryItems);
            numbers.Add(1, numbers1478[0].ToCharArray());
            numbers.Add(4, numbers1478[1].ToCharArray());
            numbers.Add(7, numbers1478[2].ToCharArray());
            numbers.Add(8, numbers1478[3].ToCharArray());

            numbers.Add(0, get0Number(numbers[1], numbers[4], entryItems));
            numbers.Add(9, get9Number(numbers[1], numbers[0], entryItems));
            numbers.Add(6, get6Number(numbers[0], numbers[9], entryItems));

            segments.Add('a', getASegment(numbers[1], numbers[7]));
            segments.Add('d', getDSegment(numbers[0], numbers[4]));
            segments.Add('b', getBSegment(numbers[1], numbers[4], segments['d']));

            segments.Add('c', getCSegment(numbers[6]));
            segments.Add('e', getESegment(numbers[9]));
            segments.Add('f', getFSegment(numbers[1], segments['c']));

            return segments;
        }

        private static string[] GetEntries(string file)
        {
            string[] lines = File.ReadAllLines(file);
            return lines;
        }
        private static string[] GetEntryItems(string entry)
        {
            string[] entryItems = new string[10];
            var temp = entry.Split("|");
            var tempArray = temp[0].Trim().Split(' ');

            for (int n = 0; n < tempArray.Length; n++)
            {
                    entryItems[n]= tempArray[n];
            }

            return entryItems;
            
        }

        private static string[,] GetOutputValues(string file)
        {
            string[] lines = File.ReadAllLines(file);
            string[,] outputValues = new string[lines.Length,4];
            for (int i = 0; i < lines.Length; i++)
            {
                var temp = lines[i].Split("|");
                var tempArray= temp[1].Trim().Split(' ');

                for (int n = 0; n < tempArray.Length; n++)
                {
                    outputValues[i, n] = tempArray[n];
                }
            }

            return outputValues;
        }

        private static int Count1478(string[,] ouputValues) {

            int[] lengthOf1478 = {2,4,3,7};
            int count = 0;
            foreach (var value in ouputValues)
            {
                if (lengthOf1478.Contains(value.Length))
                {
                    count++;
                }
            }

            return count;
        }

        private static string[] Get1478(string[] inputValues)
        {
            string[] decoding = new string[4];
            foreach (var value in inputValues)
            {
                switch (value.Length)
                {
                    case 2:
                        decoding[0] = value;
                        break;
                    case 4:
                        decoding[1] = value;
                        break;
                    case 3:
                        decoding[2] = value;
                        break;
                    case 7:
                        decoding[3] = value;
                        break;
                    default:
                        break;
                }
            }
            return decoding;            

        }


        private static char getASegment(char[] one, char[] seven)
        {
            foreach (var item in seven)
            {
                if (!one.Contains(item))
                {
                    return item;
                }
            }

            throw new Exception("Segment A not found.");
        }
        private static char[] get0Number(char[] one, char[] four, string[] entryItems)
        {
            List<char> bdsegment = new List<char>();
            foreach (var item in four)
            {
                if (!one.Contains(item))
                {
                    bdsegment.Add(item);
                }
            }

            foreach (var item in entryItems)
            {
                var itemChar = item.ToCharArray();
                if(itemChar.Length==6 && (!itemChar.Contains(bdsegment[0]) ^ !itemChar.Contains(bdsegment[1])))
                {
                    return itemChar;
                }
            }

            throw new Exception("Number 0 not found.");
        }
        private static char[] get9Number(char[] one, char[] zero, string[] entryItems)
        {
            foreach (var item in entryItems)
            {
                var itemChar = item.ToCharArray();
                if (itemChar.Length == 6 && !itemChar.SequenceEqual(zero) && itemChar.Contains(one[0]) && itemChar.Contains(one[1]))
                {
                    return itemChar;
                }
            }

            throw new Exception("Number 9 not found.");
        }

        private static char[] get6Number(char[] zero, char[] nine, string[] entryItems)
        {
            foreach (var item in entryItems)
            {
                var itemChar = item.ToCharArray();
                if (itemChar.Length == 6 && !itemChar.SequenceEqual(zero) && !itemChar.SequenceEqual(nine))
                {
                    return itemChar;
                }
            }

            throw new Exception("Number 6 not found.");
        }

        private static char getDSegment(char[] zero, char[] four)
        {
            foreach (var item in four)
            {
                if (!zero.Contains(item))
                {
                    return item;
                }
            }

            throw new Exception("Segment D not found.");
        }

        private static char getBSegment(char[] one, char[] four, char DSegment)
        {
            char[] temp = new char[] { one[0], one[1], DSegment };
            foreach (var item in four)
            {
                if (!temp.Contains(item))
                {
                    return item;
                }
            }

            throw new Exception("Segment B not found.");
        }

        private static char getESegment(char[] nine)
        {
            foreach (var c in allChars)
            {
                if (!nine.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Segment E not found.");
        }
        private static char getCSegment(char[] six)
        {
            foreach (var c in allChars)
            {
                if (!six.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Segment C not found.");
        }

        private static char getFSegment(char[] one, char Csegment)
        {
            foreach (var c in one)
            {
                if (c != Csegment)
                {
                    return c;
                }
            }

            throw new Exception("Segment F not found.");
        }

    }

}

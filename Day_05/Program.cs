using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_05
{
    class Program
    {

        const string INPUT_TEST_FILE = "../../../Resources/InputTest.txt";
        const string INPUT_FILE = "../../../Resources/Input.txt";

        static void Main(string[] args)
        {

            var coordmanager = new CoordManager();

            coordmanager.Parse(INPUT_FILE);

            coordmanager.DrawLines();

            var count = coordmanager.CountFieldsGreater2();

            Console.WriteLine("Number of fields greater 2: " + count.ToString());
        }
    }

    class CoordManager
    {
        public int[,] CoordField { get; set; }

        public int[,] X1Y1 { get; set; }
        public int[,] X2Y2 { get; set; }

        public List<(int, int)> X1Y1_sorted { get; set; }
        public List<(int, int)> X2Y2_sorted { get; set; }

        public void Parse(string file)
        {
            string[] text = File.ReadAllLines(file);
            X1Y1 = new int[text.Length, 2];
            X2Y2 = new int[text.Length, 2];
            for (int i = 0; i < text.Length; i++)
            {
                string[] values = text[i].Split(new String[] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries);
                X1Y1[i, 0] = Convert.ToInt32(values[0]);
                X1Y1[i, 1] = Convert.ToInt32(values[1]);
                X2Y2[i, 0] = Convert.ToInt32(values[2]);
                X2Y2[i, 1] = Convert.ToInt32(values[3]);
            }
        }

        public void DrawLines()
        {
            CoordField = new int[1000, 1000];

            for (int i = 0; i < X1Y1.Length / 2; i++)
            {
                int x1 = X1Y1[i, 0];
                int y1 = X1Y1[i, 1];
                int x2 = X2Y2[i, 0];
                int y2 = X2Y2[i, 1];

                // vertical
                if (x1 == x2)
                {

                    if (y1 < y2)
                    {
                        for (int n = y1; n <= y2; n++)
                        {
                            CoordField[x1, n] += 1;
                        }
                    }

                    if (y1 > y2)
                    {
                        for (int n = y2; n <= y1; n++)
                        {
                            CoordField[x1, n] += 1;
                        }
                    }
                }
                // horizontal
                if (y1 == y2)
                {

                    if (x1 < x2)
                    {
                        for (int n = x1; n <= x2; n++)
                        {
                            CoordField[n, y1] += 1;
                        }
                    }

                    if (x1 > x2)
                    {
                        for (int n = x2; n <= x1; n++)
                        {
                            CoordField[n, y1] += 1;
                        }
                    }
                }
                // diagonal
                if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2))
                {
                    if (x1 < x2 && y1 < y2)
                    {
                        var x = Enumerable.Range(x1, x2 - x1 + 1).ToArray();
                        var y = Enumerable.Range(y1, y2 - y1 + 1).ToArray();
                        for (int n = 0; n < x.Length; n++)
                        {
                            CoordField[x[n], y[n]] += 1;
                        }
                    }
                    else if (x1 > x2 && y1 > y2)
                    {
                        var x = Enumerable.Range(x2, x1 - x2 + 1).ToArray();
                        var y = Enumerable.Range(y2, y1 - y2 + 1).ToArray();
                        for (int n = 0; n < x.Length; n++)
                        {
                            CoordField[x[x.Length - n - 1], y[y.Length - n - 1]] += 1;
                        }
                    }
                    else if (x1 > x2 && y1 < y2)
                    {
                        var x = Enumerable.Range(x2, x1 - x2 + 1).ToArray();
                        var y = Enumerable.Range(y1, y2 - y1 + 1).ToArray();
                        for (int n = 0; n < x.Length; n++)
                        {
                            CoordField[x[x.Length - n - 1], y[n]] += 1;
                        }
                    }
                    else if (x1 < x2 && y1 > y2)
                    {
                        var x = Enumerable.Range(x1, x2 - x1 + 1).ToArray();
                        var y = Enumerable.Range(y2, y1 - y2 + 1).ToArray();
                        for (int n = 0; n < x.Length; n++)
                        {
                            CoordField[x[n], y[y.Length - n - 1]] += 1;
                        }
                    }

                }
            }
        }

        internal int CountFieldsGreater2()
        {
            int count = 0;
            foreach (var field in CoordField)
            {
                count += field >= 2 ? 1 : 0;
            }

            return count;
        }

    }
}

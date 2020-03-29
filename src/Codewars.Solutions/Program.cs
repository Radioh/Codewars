﻿using System;
using Codewars.Solutions.Tasks;

namespace Codewars.Solutions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Codewars... \n\n");

            Run();
        }

        static void Run()
        {
            var task = new StopgninnipSMysdroW();
            var result = task.Run();

            Console.WriteLine(task.Rank);
            Console.WriteLine(task.Name);
            Console.WriteLine(task.Description);
            Console.WriteLine(task.Link);
            Console.WriteLine();
            Console.WriteLine("Result: \n" + result);
        }
    }
}

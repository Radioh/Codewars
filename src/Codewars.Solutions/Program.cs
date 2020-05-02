using System;
using System.Linq;
using Codewars.Solutions.Core;
using Codewars.Solutions.Tasks;

namespace Codewars.Solutions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CodeWars...");

            var solved = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(ITask).IsAssignableFrom(x) && !x.IsInterface);

            Console.WriteLine($"Solved {solved.Count()} tasks\n");

            RunWorkspace();
        }

        static void RunWorkspace()
        {
            var task = new RailFenceCipher();

            Console.WriteLine($"Workspace --> {task.Name} : {task.Rank}\n");
            Console.WriteLine("Result: \n" + task.Run());
        }
    }
}

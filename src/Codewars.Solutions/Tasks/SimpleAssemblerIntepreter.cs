using System.Linq;
using System.Collections.Generic;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Simple assembler interpreter
    /// We want to create a simple interpreter of assembler which will support the following instructions:
    /// mov x y - copies y (either a constant value or the content of a register) into register x
    /// inc x - increases the content of the register x by one 
    /// dec x - decreases the content of the register x by one
    /// jnz x y - jumps to an instruction y steps away (positive means forward, negative means backward),
    /// but only if x (a constant or a register) is not zero
    /// </summary>
    public class SimpleAssemblerIntepreter : ITask
    {
        public string Name => "Simple assembler interpreter";
        public string Rank => "5 Kuy";
        public string Link => "https://www.codewars.com/kata/58e24788e24ddee28e000053";

        public string Run()
        {
            var cases = new string[][]
            {
                new[] { "mov a 5", "inc a", "dec a", "dec a", "jnz a -1", "inc a" },
                new[] { "mov a -10", "mov b a", "inc a", "dec b", "jnz a -2" }                
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Interpret(testCase);
                
                var caseString = string.Join(" ", testCase);
                var resultstring = string.Join(" ", result.Select(x => $"<{x.Key}:{x.Value}>"));

                results.Append($"{caseString} -> {resultstring} \n");
            }

            return results.ToString();
        }

        private Dictionary<string, int> Interpret(string[] program)
        {
            var vars = new Dictionary<string, int>();

            for (int i = 0; i < program.Length; i++)
            {
                var instructionSet = program[i].Split(" ");
                
                var cmd = instructionSet[0];
                var x = instructionSet[1];

                switch (cmd)
                {
                    case "mov":
                    var hasMov = vars.TryGetValue(instructionSet[2], out var mov);
                    var value = hasMov ? mov : int.Parse(instructionSet[2]); 
                    if (vars.ContainsKey(x))
                        vars[x] = value;
                    else 
                        vars.Add(x, value);
                    break;
                    
                    case "inc":
                    vars[x] += 1;
                    break;
                    
                    case "dec":
                    vars[x] -= 1;
                    break;
                    
                    case "jnz":
                    var hasJnz = vars.TryGetValue(x, out var jnz);
                    var compare = hasJnz ? jnz : int.Parse(x);
                    if (compare != 0) 
                        i += int.Parse(instructionSet[2]) - 1;
                    break;
                }
            }

            return vars;
        }
    }
}

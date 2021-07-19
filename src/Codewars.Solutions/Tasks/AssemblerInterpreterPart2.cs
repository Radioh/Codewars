using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Assembler interpreter (part II)
    /// https://www.codewars.com/kata/58e61f3d8ff24f774400002c
    /// </summary>
    public class AssemblerInterpreterPart2 : ITask
    {
        public string Name => "Assembler interpreter (part II)";
        public string Rank => "2 Kuy";

        public string Run()
        {
            var cases = new string[]
            {
                "\n; My first program\nmov  a, 5\ninc  a\ncall function\nmsg  '(5+1)/2 = ', a    ; output message\nend\n\nfunction:\n    div  a, 2\n    ret\n",
                "\nmov   a, 5\nmov   b, a\nmov   c, a\ncall  proc_fact\ncall  print\nend\n\nproc_fact:\n    dec   b\n    mul   c, b\n    cmp   b, 1\n    jne   proc_fact\n    ret\n\nprint:\n    msg   a, '! = ', c ; output text\n    ret\n",
                "\nmov   a, 8            ; value\nmov   b, 0            ; next\nmov   c, 0            ; counter\nmov   d, 0            ; first\nmov   e, 1            ; second\ncall  proc_fib\ncall  print\nend\n\nproc_fib:\n    cmp   c, 2\n    jl    func_0\n    mov   b, d\n    add   b, e\n    mov   d, e\n    mov   e, b\n    inc   c\n    cmp   c, a\n    jle   proc_fib\n    ret\n\nfunc_0:\n    mov   b, c\n    inc   c\n    jmp   proc_fib\n\nprint:\n    msg   'Term ', a, ' of Fibonacci series is: ', b        ; output text\n    ret\n",
                "\nmov   a, 11           ; value1\nmov   b, 3            ; value2\ncall  mod_func\nmsg   'mod(', a, ', ', b, ') = ', d        ; output\nend\n\n; Mod function\nmod_func:\n    mov   c, a        ; temp1\n    div   c, b\n    mul   c, b\n    mov   d, a        ; temp2\n    sub   d, c\n    ret\n",
                "\nmov   a, 81         ; value1\nmov   b, 153        ; value2\ncall  init\ncall  proc_gcd\ncall  print\nend\n\nproc_gcd:\n    cmp   c, d\n    jne   loop\n    ret\n\nloop:\n    cmp   c, d\n    jg    a_bigger\n    jmp   b_bigger\n\na_bigger:\n    sub   c, d\n    jmp   proc_gcd\n\nb_bigger:\n    sub   d, c\n    jmp   proc_gcd\n\ninit:\n    cmp   a, 0\n    jl    a_abs\n    cmp   b, 0\n    jl    b_abs\n    mov   c, a            ; temp1\n    mov   d, b            ; temp2\n    ret\n\na_abs:\n    mul   a, -1\n    jmp   init\n\nb_abs:\n    mul   b, -1\n    jmp   init\n\nprint:\n    msg   'gcd(', a, ', ', b, ') = ', c\n    ret\n",
                "\ncall  func1\ncall  print\nend\n\nfunc1:\n    call  func2\n    ret\n\nfunc2:\n    ret\n\nprint:\n    msg 'This program should return null'\n",
                "\nmov   a, 2            ; value1\nmov   b, 10           ; value2\nmov   c, a            ; temp1\nmov   d, b            ; temp2\ncall  proc_func\ncall  print\nend\n\nproc_func:\n    cmp   d, 1\n    je    continue\n    mul   c, a\n    dec   d\n    call  proc_func\n\ncontinue:\n    ret\n\nprint:\n    msg a, '^', b, ' = ', c\n    ret\n"
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var result = Interpret(testCase);

                results.Append($"{testCase} -> {result} \n");
            }

            return results.ToString();
        }

        private static string Interpret(string input)
        {
            var program = ParseProgram(input);
            var subRutinePointers = ParseSubRutinesPointers(program);

            // Debug(program); // Remove this

            var output = new StringBuilder();
            var registers = new Dictionary<string, int>();
            var retPointer = new Stack<int>();
            var pointer = 0;
            var cmp = 0;
            var running = true;

            while (running)
            {
                if (pointer == program.Length)
                    return null;

                var (cmd, arguments) = ParseInstructionSet(program[pointer]);

                switch (cmd)
                {
                    case "mov":
                        Mov(arguments, registers);
                        break;
                    case "inc":
                        Inc(arguments, registers);
                        break;
                    case "dec":
                        Dec(arguments, registers);
                        break;
                    case "add":
                        Add(arguments, registers);
                        break;
                    case "sub":
                        Sub(arguments, registers);
                        break;
                    case "mul":
                        Mul(arguments, registers);
                        break;
                    case "div":
                        Div(arguments, registers);
                        break;
                    case "jmp":
                        pointer = Jmp(arguments, subRutinePointers);
                        break;
                    case "cmp":
                        cmp = Cmp(arguments, registers);
                        break;
                    case "jne":
                        pointer = Jne(arguments, pointer, cmp, subRutinePointers);
                        break;
                    case "je":
                        pointer = Je(arguments, pointer, cmp, subRutinePointers);
                        break;
                    case "jge":
                        pointer = Jge(arguments, pointer, cmp, subRutinePointers);
                        break;
                    case "jg":
                        pointer = Jg(arguments, pointer, cmp, subRutinePointers);
                        break;
                    case "jle":
                        pointer = Jle(arguments, pointer, cmp, subRutinePointers);
                        break;
                    case "jl":
                        pointer = Jl(arguments, pointer, cmp, subRutinePointers);
                        break;
                    case "call":
                        pointer = Call(arguments, subRutinePointers, retPointer, pointer);
                        break;
                    case "ret":
                        pointer = Ret(retPointer);
                        break;
                    case "msg":
                        output.Append(Msg(arguments, registers));
                        break;
                    case "end":
                        running = false;
                        break;
                }

                pointer++;
            }
            return output.ToString();
        }

        private static int Jl(string[] arguments, int pointer, int cmp, Dictionary<string, int> subRutinePointers)
        {
            if (cmp < 0)
                return subRutinePointers[arguments[0]];

            return pointer;
        }

        private static int Jle(string[] arguments, int pointer, int cmp, Dictionary<string, int> subRutinePointers)
        {
            if (cmp <= 0)
                return subRutinePointers[arguments[0]];

            return pointer;
        }

        private static int Jg(string[] arguments, int pointer, int cmp, Dictionary<string, int> subRutinePointers)
        {
            if (cmp > 0)
                return subRutinePointers[arguments[0]];

            return pointer;
        }

        private static int Jge(string[] arguments, int pointer, int cmp, Dictionary<string, int> subRutinePointers)
        {
            if (cmp >= 0)
                return subRutinePointers[arguments[0]];

            return pointer;
        }

        private static int Je(string[] arguments, int pointer, int cmp, Dictionary<string, int> subRutinePointers)
        {
            if (cmp == 0)
                return subRutinePointers[arguments[0]];

            return pointer;
        }

        private static int Jne(string[] arguments, int pointer, int cmp, Dictionary<string, int> subRutinePointers)
        {
            if (cmp != 0)
                return subRutinePointers[arguments[0]];

            return pointer;
        }

        private static int Cmp(string[] arguments, Dictionary<string, int> registers)
        {
            return CheckRegisterGetValue(arguments[0], registers).CompareTo(CheckRegisterGetValue(arguments[1], registers));
        }

        private static string Msg(string[] instructionSet, Dictionary<string, int> registers)
        {
            var line = new StringBuilder();

            foreach (var argument in instructionSet)
            {
                if (argument.StartsWith("'"))
                    line.Append(argument.Replace("'", string.Empty));
                else
                    line.Append(registers[argument]);
            }

            return line.ToString();
        }

        private static int Ret(Stack<int> retPointer)
        {
            return retPointer.Pop();
        }

        private static int Call(string[] arguments, Dictionary<string, int> subRutinePointers, Stack<int> retPointer, int pointer)
        {
            retPointer.Push(pointer);
            return subRutinePointers[arguments[0]];
        }

        private static int Jmp(string[] arguments, Dictionary<string, int> subRutinePointers)
        {
            return subRutinePointers[arguments[0]];
        }

        private static void Div(string[] arguments, Dictionary<string, int> registers)
        {
            registers[arguments[0]] /= CheckRegisterGetValue(arguments[1], registers);
        }

        private static void Mul(string[] arguments, Dictionary<string, int> registers)
        {
            registers[arguments[0]] *= CheckRegisterGetValue(arguments[1], registers);
        }

        private static void Sub(string[] arguments, Dictionary<string, int> registers)
        {
            registers[arguments[0]] -= CheckRegisterGetValue(arguments[1], registers);
        }

        private static void Add(string[] arguments, Dictionary<string, int> registers)
        {
            registers[arguments[0]] += CheckRegisterGetValue(arguments[1], registers);
        }

        private static void Dec(string[] arguments, Dictionary<string, int> registers)
        {
            registers[arguments[0]] -= 1;
        }

        private static void Inc(string[] arguments, Dictionary<string, int> registers)
        {
            registers[arguments[0]] += 1;
        }

        private static void Mov(string[] arguments, Dictionary<string, int> registers)
        {
            var x = arguments[0];
            var y = arguments[1];

            var value = CheckRegisterGetValue(y, registers);

            if (registers.ContainsKey(x))
                registers[x] = value;
            else
                registers.Add(x, value);
        }

        private static int CheckRegisterGetValue(string y, Dictionary<string, int> registers)
        {
            var isRegister = registers.TryGetValue(y, out var registerValue);
            return isRegister ? registerValue : int.Parse(y);
        }

        private static string[] ParseProgram(string input)
        {
            return input
                .Split("\n")
                .Where(x => !x.StartsWith(";"))
                .Select(x => x.Split(";")[0])
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();
        }

        private static Dictionary<string, int> ParseSubRutinesPointers(string[] program)
        {
            var result = new Dictionary<string, int>();

            for (int i = 0; i < program.Length; i++)
                if (program[i].Contains(":"))
                    result.Add(program[i].Remove(program[i].Length - 1), i);

            return result;
        }

        private static (string cmd, string[] arguments) ParseInstructionSet(string input)
        {
            var cmdSplit = input.Split(" ", count: 2);

            if (cmdSplit.Length == 1)
                return (cmdSplit[0], Array.Empty<string>());

            string cur = "";
            var msg = false;
            var arguments = new List<string>();

            for (int i = 0; i < cmdSplit[1].Length; i++)
            {
                if (cmdSplit[1][i] == '\'')
                {
                    msg = !msg;
                }

                if (cmdSplit[1][i] == ',' && !msg)
                {
                    arguments.Add(cur);
                    cur = "";
                    continue;
                }

                cur += cmdSplit[1][i];
            }

            arguments.Add(cur);

            var parsedArguments = arguments
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();

            return (cmdSplit[0], parsedArguments);
        }

        private static void Debug(string[] input)
        {
            foreach (var i in input)
                System.Console.WriteLine(i);
        }
    }
}

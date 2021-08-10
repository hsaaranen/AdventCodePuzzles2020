using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;

namespace AdventCode8
{
    class Program
    {
        public enum Type
        {
            nop,
            acc,
            jmp
        }

        public class Instruction
        {
            public Instruction(Type command, int arg)
            {
                Command = command;
                Arg = arg;
                CallOrder = -1;

            }

            public Type Command { get; }
            public int Arg { get; }
            public int CallOrder { get; set; }
        }


        static void Main()
        {
            var input = File.ReadAllLines("..\\..\\..\\input.txt");
            var fixedInput = File.ReadAllLines("..\\..\\..\\fixed_input.txt");
            var bootCode = ParseInstructions(fixedInput);
            var executionCompleted = ExecuteBootCode(bootCode, out var accValue);

            var lineNumber = 0;
            foreach (var codeLine in bootCode)
            {
                lineNumber++;
                Console.WriteLine($"Line {lineNumber}: {codeLine.Command} {codeLine.Arg} {codeLine.CallOrder}");
            }

            if (executionCompleted)
            {
                Console.WriteLine($"Acc value was {accValue} when code completed");
            }
            else
            {
                Console.WriteLine($"Acc value was {accValue} when instruction ws called second time");
            }
        }

        private static bool ExecuteBootCode(Instruction[] bootCode, out int accValue)
        {
            accValue = 0;
            var callOrder = 0;
            var index = 0;
            while (true)
            {
                if (index == bootCode.Length)
                {
                    return true;
                }

                var instruction = bootCode[index];
                if (instruction.CallOrder > 0)
                {
                    return false;
                }

                instruction.CallOrder = callOrder++;
                switch (instruction.Command)
                {
                    case Type.nop:
                        index++;
                        break;
                    case Type.acc:
                        index++;
                        accValue += instruction.Arg;
                        break;
                    case Type.jmp:
                        index += instruction.Arg;
                        break;
                }
            }
        }

        private static Instruction[] ParseInstructions(string[] input)
        {
            var bootCode = new List<Instruction>();
            foreach (var line in input)
            {
                var spaceIndex = line.IndexOf(' ');
                var command = line.Substring(0, spaceIndex);
                var arg = int.Parse(line.Substring(spaceIndex, line.Length - spaceIndex), NumberStyles.Any);
                bootCode.Add(new Instruction(Enum.Parse<Type>(command), arg));
            }

            return bootCode.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        protected Dictionary<char, Action<IVirtualMachine>> CommandSet { get; set; }

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            InstructionPointer = 0;
            MemoryPointer = 0;
            CommandSet = new Dictionary<char, Action<IVirtualMachine>>();
        }
        
        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            if (!CommandSet.ContainsKey(symbol)) CommandSet.Add(symbol, execute);
        }

        public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                if (CommandSet.TryGetValue(Instructions[InstructionPointer], out var action))
                    action(this);
                InstructionPointer++;
            }
        }
    }
}
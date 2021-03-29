using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
            List<int> startLoop = new List<int>();
            List<int> endLoop = new List<int>();
            Dictionary<int, int> startProgLp = new Dictionary<int, int>();
            Dictionary<int, int> endProgLp = new Dictionary<int, int>();

            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[') startLoop.Add(i);
                if (vm.Instructions[i] == ']') endLoop.Add(i);
            }
            endLoop.Reverse();

            for (int i = 0; i < startLoop.Count; i++)
            {
                startProgLp.Add(startLoop[i], endLoop[i]);
            }

            foreach (var item in startProgLp)
            {
                endProgLp.Add(item.Value, item.Key);
            }

			vm.RegisterCommand('[', b => {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = startProgLp[b.InstructionPointer];
            });
			vm.RegisterCommand(']', b => {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = endProgLp[b.InstructionPointer];
            });
		}
	}
}
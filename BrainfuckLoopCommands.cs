using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
            Dictionary<int, int> startProgLp = new Dictionary<int, int>();
            Dictionary<int, int> endProgLp = new Dictionary<int, int>();
            Stack<int> list = new Stack<int>();

            LoopProcces(vm, startProgLp, list);

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

        public static void LoopProcces(IVirtualMachine vm, Dictionary<int, int> startProgLp, Stack<int> list)
        {
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[')
                {
                    for (int j = 1 + i; j < vm.Instructions.Length; j++)
                    {
                        if (vm.Instructions[j] == ']')
                        {
                            startProgLp.Add(i, j);
                            i = 1 + j;
                            break;
                        }
                        if (vm.Instructions[j] == '[')
                        {
                            list.Push(i);
                            break;
                        }
                    }
                }

                if (vm.Instructions[i] == ']')
                {
                    startProgLp.Add(list.Pop(), i);
                }
            }
        }
	}
}
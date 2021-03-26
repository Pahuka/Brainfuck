using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            foreach (var item in vm.Instructions)
            {
                if (char.IsLetterOrDigit(item))
                    vm.RegisterCommand(item, b => b.Memory[b.MemoryPointer] = (byte)item);
            }

            vm.RegisterCommand('.', b =>
            {
               write.Invoke((char)b.Memory[b.MemoryPointer]);
            });
            vm.RegisterCommand('+', b => 
            {
                b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + 1) % 256);
            });
            vm.RegisterCommand('-', b => 
            {
                var temValue = b.Memory[b.MemoryPointer];
                b.Memory[b.MemoryPointer] = temValue <= 0 ? (byte)255 : temValue -= 1;
            });
            vm.RegisterCommand('>', b => 
            {
                b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length;
            });
            vm.RegisterCommand('<', b => 
            {
                b.MemoryPointer = b.MemoryPointer <= 0 ? b.Memory.Length - 1 : b.MemoryPointer -= 1 ;
            });
            vm.RegisterCommand(',', b =>
                {
                    var x = read.Invoke();
                    if (x != -1) b.Memory[b.MemoryPointer] = (byte)x;
                });

        }
    }
}
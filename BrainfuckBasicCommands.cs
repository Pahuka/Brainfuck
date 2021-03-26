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
            Encoding encoding = Encoding.ASCII;
            var c = encoding.GetBytes(vm.Instructions);
            var a = (char)c[0];

            foreach (var item in vm.Instructions)
            {
                if (char.IsLetterOrDigit(item))
                    vm.RegisterCommand(item, b => b.Memory[b.MemoryPointer] = (byte)item);
            }

            vm.RegisterCommand('.', b =>
            {
               write.Invoke((char)b.Memory[b.MemoryPointer]);
            });
            vm.RegisterCommand('+', b => { });
            vm.RegisterCommand('-', b => { });
            vm.RegisterCommand('>', b => { });
            vm.RegisterCommand('<', b => { });
            vm.RegisterCommand(',', b =>
                {
                    var x = read.Invoke();
                    if (x != -1) b.Memory[b.MemoryPointer] = (byte)x;
                });

        }
    }
}
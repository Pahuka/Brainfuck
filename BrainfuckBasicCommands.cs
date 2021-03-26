using System;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            MemoryIncDec(vm, read, write);
            MemoryReadWrite(vm, read, write);
            MemoryMove(vm, read, write);

            foreach (var item in vm.Instructions)
            {
                if (char.IsLetterOrDigit(item))
                    try
                    {
                        vm.RegisterCommand(item, b => b.Memory[b.MemoryPointer] = (byte)item);
                    }
                    catch (Exception) { }
            }
        }

        public static void MemoryReadWrite(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b =>
            {
                write.Invoke((char)b.Memory[b.MemoryPointer]);
            });
            vm.RegisterCommand(',', b =>
            {
                var x = read.Invoke();
                if (x != -1) b.Memory[b.MemoryPointer] = (byte)x;
            });
        }

        public static void MemoryIncDec(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('+', b =>
            {
                b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + 1) % 256);
            });
            vm.RegisterCommand('-', b =>
            {
                var temValue = b.Memory[b.MemoryPointer];
                b.Memory[b.MemoryPointer] = temValue <= 0 ? (byte)255 : temValue -= 1;
            });
        }

        public static void MemoryMove(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('>', b =>
            {
                b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length;
            });
            vm.RegisterCommand('<', b =>
            {
                b.MemoryPointer = b.MemoryPointer <= 0 ? b.Memory.Length - 1 : b.MemoryPointer -= 1;
            });
        }
    }
}
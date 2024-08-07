using MagickaForge.Forges;
using System.Diagnostics;

namespace MagickaForge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("= Magicka Forge by Rylei. C =");
            Console.ForegroundColor = ConsoleColor.Green;

            string instructionPath, outputPath;

            if (args.Length == 0)
            {
                Console.WriteLine("Input the path to a JSON instruction file:");
                instructionPath = Console.ReadLine();

                if (instructionPath == null)
                {
                    throw new ArgumentNullException("Path may not be null!");
                }
            }
            else
            {
                instructionPath = args[0];
            }
            Console.WriteLine();
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("= Process Starting... =\n");

            //ItemForge item = new ItemForge();
            //item.InstructionsToXNB(instructionPath);
            CharacterForge characterForge = new CharacterForge();
            characterForge.InstructionsToXNB(instructionPath, false);

            sw.Stop();
            Console.WriteLine($"= XNB Created in {sw.ElapsedMilliseconds} ms =");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}

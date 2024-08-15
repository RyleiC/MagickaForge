using MagickaForge.Forges;
using MagickaForge.Forges.Character;
using MagickaForge.Forges.Item;
using System.Diagnostics;

namespace MagickaForge
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("= Magicka Forge by Rylei. C =");

            string instructionPath;

            if (args.Length < 1)
            {
                Console.WriteLine(@"Input the path to a JSON instruction file\directory:");
                instructionPath = Console.ReadLine();
            }
            else
            {
                instructionPath = args[0];
            }


            Console.WriteLine("= Process Starting... =\n");
            Console.ForegroundColor = ConsoleColor.White;

            Stopwatch stopWatch = Stopwatch.StartNew();

            Forge.GenerateXNBs(instructionPath);

            stopWatch.Stop();

            Console.WriteLine($"= XNB Created in {stopWatch.ElapsedMilliseconds} ms =");
            Console.ReadKey();
        }
    }
}

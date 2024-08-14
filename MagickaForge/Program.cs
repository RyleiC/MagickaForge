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
            Console.ForegroundColor = ConsoleColor.Yellow;

            string instructionPath;

            if (args.Length == 0)
            {
                Console.WriteLine(@"Input the path to a JSON instruction file\directory:");
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


            Console.WriteLine("= Process Starting... =\n");
            Console.ForegroundColor = ConsoleColor.White;

            Stopwatch sw = Stopwatch.StartNew();

            Forge.CreateForges(instructionPath);

            sw.Stop();

            Console.WriteLine($"= XNB Created in {sw.ElapsedMilliseconds} ms =");
            Console.ReadKey();
        }
    }
}

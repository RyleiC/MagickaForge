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
            Console.ForegroundColor = ConsoleColor.Green;

            string instructionPath, forgeType;

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

            Console.WriteLine("Input the type of forge (Item/Character):");
            forgeType = Console.ReadLine().ToLower();
            if (forgeType == null)
            {
                throw new ArgumentNullException("Forge type may not be null!");
            }

            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("= Process Starting... =\n");

            if (forgeType == "item")
            {
                ItemForge item = new ItemForge();
                item.InstructionsToXNB(instructionPath);
            }
            else
            {
                CharacterForge characterForge = new CharacterForge();
                characterForge.InstructionsToXNB(instructionPath, false);
            }

            sw.Stop();
            Console.WriteLine($"= XNB Created in {sw.ElapsedMilliseconds} ms =");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}

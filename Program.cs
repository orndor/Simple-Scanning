using System;
using System.IO;

namespace Simple_Scanning
{
    class Program
    {
        static void Main(string[] args)
        {
            char menuInput = ' ';
            char[] delimiterChars = { '¿' };
            string fileName = @"Task-List.txt";
            int counter = 0;
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            string[] lines = File.ReadAllLines(fileName);
            for (int i = counter; i < 20; i++)
            {
                string line = lines[i];
                string[] statusSeparator = line.Split(delimiterChars);
                Console.WriteLine($"{statusSeparator[1]}");
            }
            counter += 20;
            Console.WriteLine("Options: N for next 20 tasks.  P for previous 20 tasks.  I to insert a new task.  X to Exit.  E to edit an item.");
            menuInput = Console.ReadKey().KeyChar;
            do
            {
                if (menuInput == 'N' || menuInput == 'n')
                {
                    for (int i = counter; i <= counter + 19; i++)
                    {
                        string line = lines[i];
                        string[] statusSeparator = line.Split(delimiterChars);
                        Console.WriteLine($"{statusSeparator[1]}");
                    }
                    counter += 20;
                    Console.WriteLine("Options: N for next 20 tasks.  P for previous 20 tasks.  I to insert a new task.  X to Exit.  E to edit an item.");
                    menuInput = Console.ReadKey().KeyChar;
                }
                else if (menuInput == 'p' || menuInput == 'p')
                {
                    for (int i = counter - 40; i <= counter - 21; i++)
                    {
                        string line = lines[i];
                        string[] statusSeparator = line.Split(delimiterChars);
                        Console.WriteLine($"{statusSeparator[1]}");
                    }
                    counter -= 20;
                    Console.WriteLine("Options: N for next 20 tasks.  P for previous 20 tasks.  I to insert a new task.  X to Exit.  E to edit an item.");
                    menuInput = Console.ReadKey().KeyChar;
                }
                else if (menuInput == 'I' || menuInput == 'i')
                {
                    Console.WriteLine("Enter a new task on a single line.  Hit enter when complete.");
                    string newTask = Console.ReadLine();
                    using (StreamWriter stream = File.AppendText(@"Task-List.txt"))
                    {
                        stream.WriteLine($"O¿{lines.Length + 1}.{newTask}");
                    }
                    for (int i = 0; i <= 19; i++)
                    {
                        string line = lines[i];
                        string[] statusSeparator = line.Split(delimiterChars);
                        Console.WriteLine($"{statusSeparator[1]}");
                    }
                    Console.WriteLine("Options: N for next 20 tasks.  P for previous 20 tasks.  I to insert a new task.  X to Exit.  E to edit an item.");
                    menuInput = Console.ReadKey().KeyChar;
                }
                else if (menuInput == 'X' || menuInput == 'x')
                {
                    System.Environment.Exit(0);
                }
                else if (menuInput == 'E' || menuInput == 'e')
                {
                    Console.WriteLine("Implment task for editing an item");
                    Console.WriteLine("Options: N for next 20 tasks.  P for previous 20 tasks.  I to insert a new task.  X to Exit.  E to edit an item.");
                    menuInput = Console.ReadKey().KeyChar;
                }
                else
                {
                    Console.WriteLine("\nPlease enter a valid choice.");
                    Console.WriteLine("Options: N for next 20 tasks.  P for previous 20 tasks.  I to insert a new task.  X to Exit.  E to edit an item.");
                    menuInput = Console.ReadKey().KeyChar;
                }
            } while (menuInput != 'N' || menuInput == 'n' || menuInput == 'P' || menuInput == 'p' || menuInput == 'I' || menuInput == 'i' || menuInput == 'X' || menuInput == 'x' || menuInput == 'E' || menuInput == 'e');
        }
    }
}

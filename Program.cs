using System;
using System.IO;
using System.Collections.Generic;

namespace Simple_Scanning
{
    class Program
    {
        static void Main(string[] args)
        {
            char menuInput = ' ';
            char delimiterChars = '␟';
            int currentPlaceOnList = 0;
            string fileName = @"Task-List.txt";

            List<string> taskLists = FileReader(delimiterChars, fileName);
            currentPlaceOnList = OutputWriter(delimiterChars, currentPlaceOnList, taskLists);
            menuInput = MenuPrinter();

            do
            {
                if (menuInput == 'N' || menuInput == 'n')
                {
                    Console.WriteLine("\n");
                    currentPlaceOnList = OutputWriter(delimiterChars, currentPlaceOnList, taskLists);
                    menuInput = MenuPrinter();
                }
                else if (menuInput == 'I' || menuInput == 'i')
                {
                    Console.WriteLine("\nEnter a new task on a single line.  Hit enter when complete.");
                    string newTask = Console.ReadLine();
                    using (StreamWriter streamLine = File.AppendText(@"Task-List.txt"))
                    {
                        streamLine.WriteLine($"O{delimiterChars}{taskLists.Count + 1}.{delimiterChars}{newTask}");
                    }
                    taskLists = FileReader(delimiterChars, fileName);
                    currentPlaceOnList = 0;
                    menuInput = MenuPrinter();
                }
                else if (menuInput == 'E' || menuInput == 'e')
                {
                    Console.WriteLine("\nEnter the task number you wish to edit, and then press Enter:");
                    int taskNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nWould you like to [D]ismiss, [R]enter the task, or [S]tart working a task? Or go [B]ack?");
                    char editOption = Console.ReadKey().KeyChar;
                    if(editOption == 'D' || editOption == 'd')
                    {
                        string line = taskLists[taskNumber - 1];
                        string[] statusSeparator = line.Split(delimiterChars);
                        taskLists[taskNumber - 1] = $"D{delimiterChars}{statusSeparator[1]}{delimiterChars}{statusSeparator[2]}";
                        using (StreamWriter stream = File.CreateText(@"Task-List.txt"))
                        {
                            foreach (string l in taskLists)
                            {
                                stream.WriteLine(l);
                            }
                            stream.Close();
                            taskLists = FileReader(delimiterChars, fileName);

                        }
                        currentPlaceOnList = 0;
                    }
                    else if (editOption == 'R' || editOption == 'r')
                    {
                        string line = taskLists[taskNumber - 1];
                        string[] statusSeparator = line.Split(delimiterChars);
                        using (StreamWriter streamLine = File.AppendText(@"Task-List.txt"))
                        {
                            statusSeparator = line.Split(delimiterChars);
                            streamLine.WriteLine($"O{delimiterChars}{taskLists.Count + 1}.{delimiterChars}{statusSeparator[2]}");
                        }
                        taskLists = FileReader(delimiterChars, fileName);
                        taskLists[taskNumber - 1] = $"D{delimiterChars}{statusSeparator[1]}{delimiterChars}{statusSeparator[2]}";
                        using (StreamWriter stream = File.CreateText(@"Task-List.txt"))
                        {
                            foreach (string l in taskLists)
                            {
                                stream.WriteLine(l);
                            }
                            stream.Close();
                        }
                        currentPlaceOnList = 0;
                    }
                    else if (editOption == 'S' || editOption == 's')
                    {
                        string line = taskLists[taskNumber - 1];
                        string[] statusSeparator = line.Split(delimiterChars);
                        taskLists[taskNumber - 1] = $"S{delimiterChars}{statusSeparator[1]}{delimiterChars}{statusSeparator[2]}";
                        using (StreamWriter stream = File.CreateText(@"Task-List.txt"))
                        {
                            foreach (string l in taskLists)
                            {
                                stream.WriteLine(l);
                            }
                            stream.Close();
                            taskLists = FileReader(delimiterChars, fileName);
                        }
                        currentPlaceOnList = 0;
                    }
                    else if (editOption == 'B' || editOption == 'b')
                    {
                        menuInput = MenuPrinter();
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter a valid choice.");
                        menuInput = MenuPrinter();
                    }
                    menuInput = MenuPrinter();
                }
                else if (menuInput == 'X' || menuInput == 'x')
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nPlease enter a valid choice.");
                    menuInput = MenuPrinter();
                }
            } while (menuInput != 'N' || menuInput == 'n' || menuInput == 'I' || menuInput == 'i' || menuInput == 'X' || menuInput == 'x' || menuInput == 'E' || menuInput == 'e');
        }

        static char MenuPrinter()
        {
            Console.WriteLine("\nOptions: N for next page.  I to insert a new task.  E to edit an item.  X to Exit. ");
            char menuInput = Console.ReadKey().KeyChar;

            return menuInput;
        }

        static List<string> FileReader(char delimiterChars, string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            Console.WriteLine("\n");
            var tasksFile = File.ReadAllLines(fileName);
            var taskLists = new List<string>(tasksFile);
            return taskLists;
        }

        static int OutputWriter(char delimiterChars, int currentPlaceOnList, List<string> taskLists)
        {
            if(currentPlaceOnList + 20 > taskLists.Count)
            {
                for (int i = currentPlaceOnList; i < taskLists.Count; i++)
                {
                    string line = taskLists[i];
                    string[] statusSeparator = line.Split(delimiterChars);
                    if (statusSeparator[0] == "D" || statusSeparator[0] == "d")
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    }
                    else if (statusSeparator[0] == "S" || statusSeparator[0] == "s")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    }
                }

                currentPlaceOnList = 0;
            }
            else
            {
                for (int i = currentPlaceOnList; i <= (currentPlaceOnList + 19); i++)
                {
                    string line = taskLists[i];
                    string[] statusSeparator = line.Split(delimiterChars);
                    if (statusSeparator[0] == "D" || statusSeparator[0] == "d")
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    }
                    else if (statusSeparator[0] == "S" || statusSeparator[0] == "s")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    }
                }
                currentPlaceOnList += 20;
            }

            return currentPlaceOnList;
        }
    }
}

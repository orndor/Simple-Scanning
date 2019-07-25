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
            currentPlaceOnList = ConsoleWriter(delimiterChars, currentPlaceOnList, taskLists);
            menuInput = MenuPrinter();

            do
            {
                if (menuInput == 'N' || menuInput == 'n')
                {
                    currentPlaceOnList = ConsoleWriter(delimiterChars, currentPlaceOnList, taskLists);
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
                    ConsoleWriter(delimiterChars, 0, taskLists);
                    menuInput = MenuPrinter();
                }
                else if (menuInput == 'E' || menuInput == 'e')
                {
                    Console.WriteLine("\nEnter the task number you wish to edit, and then press Enter:");
                    int taskNumber = int.Parse(Console.ReadLine());
                    if(taskNumber > taskLists.Count)
                    {
                        Console.WriteLine("\nThat task number dosen't exist.  Please try again.\n");
                    }
                    else
                    {
                        Console.WriteLine("\nWould you like to [D]ismiss, [R]eenter the task, or [S]tart working a task? Or go [B]ack?");
                        char editOption = Console.ReadKey().KeyChar;
                        if (editOption == 'D' || editOption == 'd')
                        {
                            string line = taskLists[taskNumber - 1];
                            string[] statusSeparator = line.Split(delimiterChars);
                            taskLists[taskNumber - 1] = $"D{delimiterChars}{statusSeparator[1]}{delimiterChars}{statusSeparator[2]}";
                            FileWriter(delimiterChars, taskLists, fileName);
                            ConsoleWriter(delimiterChars, 0, taskLists);
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
                            FileWriter(delimiterChars, taskLists, fileName);
                            ConsoleWriter(delimiterChars, 0, taskLists);
                        }
                        else if (editOption == 'S' || editOption == 's')
                        {
                            bool taskAlreadyStarted = false;
                            string line = taskLists[0];
                            string[] statusSeparator = line.Split(delimiterChars);
                            for (int i = 0; i < taskLists.Count; i++)
                            {
                                line = taskLists[i];
                                statusSeparator = line.Split(delimiterChars);
                                if (statusSeparator[0] == "S")
                                {
                                    taskAlreadyStarted = true;
                                }
                            }
                            if (taskAlreadyStarted)
                            {
                                Console.WriteLine("\nYou're already working on a task.  You'll need to dismiss or reenter the task to start a new one.\n");
                            }
                            else
                            {
                                line = taskLists[taskNumber - 1];
                                statusSeparator = line.Split(delimiterChars);
                                taskLists[taskNumber - 1] = $"S{delimiterChars}{statusSeparator[1]}{delimiterChars}{statusSeparator[2]}";
                                FileWriter(delimiterChars, taskLists, fileName);
                                ConsoleWriter(delimiterChars, 0, taskLists);
                            }
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
            } while (menuInput == 'N' || menuInput == 'n' || menuInput == 'I' || menuInput == 'i' || menuInput == 'X' || menuInput == 'x' || menuInput == 'E' || menuInput == 'e');
        }


        static char MenuPrinter()
        {
            Console.WriteLine("\nOptions: [N]ext page.  [I]nsert a new task.  [E]dit a task.  E[X]it. ");
            char menuInput = Console.ReadKey().KeyChar;

            return menuInput;
        }


        static List<string> FileReader(char delimiterChars, string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            var tasksFile = File.ReadAllLines(fileName);
            var taskLists = new List<string>(tasksFile);

            return taskLists;
        }


        static void FileWriter(char delimiterChars, List<string> taskLists, string fileName)
        {
            taskLists = ListReorganizer(delimiterChars, taskLists);
            using (StreamWriter stream = File.CreateText(fileName))
            {
                foreach (string l in taskLists)
                {
                    stream.WriteLine(l);
                }
                stream.Close();
                taskLists = FileReader(delimiterChars, fileName);
            }
        }


        static List<string> ListReorganizer(char delimiterChars, List<string> taskLists)
        {
            try
            {
                string line = taskLists[0];
                string[] statusSeparator = line.Split(delimiterChars);
                while (statusSeparator[0] == "D")
                {
                    taskLists.RemoveAt(0);
                    line = taskLists[0];
                    statusSeparator = line.Split(delimiterChars);
                }
                for (int i = 0; i < taskLists.Count; i++)
                {
                    line = taskLists[i];
                    statusSeparator = line.Split(delimiterChars);
                    taskLists[i] = $"{statusSeparator[0]}{delimiterChars}{i + 1}.{delimiterChars}{statusSeparator[2]}";
                }
            }
            catch
            {
                Console.WriteLine("You have no tasks on your task list.  Joyous day!");
            }

            return taskLists;
        }


        static int ConsoleWriter(char delimiterChars, int currentPlaceOnList, List<string> taskLists)
        {
            Console.Clear();
            if (currentPlaceOnList + 20 > taskLists.Count)
            {
                try
                {
                    for (int i = currentPlaceOnList; i < taskLists.Count; i++)
                    {
                        WriterLoopTask(taskLists, i);
                    }
                }
                catch
                {
                    Console.WriteLine("You have no tasks on your task list.  Joyous day!");
                }


                currentPlaceOnList = 0;
            }
            else
            {
                for (int i = currentPlaceOnList; i <= (currentPlaceOnList + 19); i++)
                {
                    WriterLoopTask(taskLists, i);
                }
                currentPlaceOnList += 20;
            }


            void WriterLoopTask(List<string> taskListsCopy, int incrementer)
            {
                string line = taskListsCopy[incrementer];
                string[] statusSeparator = line.Split(delimiterChars);
                if (statusSeparator[0] == "D" || statusSeparator[0] == "d")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (statusSeparator[0] == "S" || statusSeparator[0] == "s")
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"{statusSeparator[1]} {statusSeparator[2]}");
                }
            }

            return currentPlaceOnList;
        }
    }
}

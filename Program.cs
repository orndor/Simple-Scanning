// File name: Program.cs
// Project Name: Simply_Scanning
// Author: Orndoff, Robert K.
// Date created: 07/22/2019
// Date last modified: 07/26/2019
//
// Purpose: MSSA Homework assignment to create a simple task tracking application based on 
// Mark Forster Simple Scanning technique: http://markforster.squarespace.com/blog/2017/12/2/simple-scanning-the-rules.html
//
// C#
using System;
using System.IO;
using System.Collections.Generic;

namespace Simple_Scanning
{
    class Program
    {
        static void Main()
        {
            char menuInput;
            char delimiterChars = '␟';
            int currentPlaceOnList = 0;
            string fileName = @"Task-List.txt";

            List<string> taskLists = FileReader(fileName);
            currentPlaceOnList = ConsoleWriter(delimiterChars, currentPlaceOnList, taskLists);

            do
            {
                MenuPrinter("Options: [N]ext page.  [I]nsert a new task.  [E]dit a task.  E[X]it.");
                menuInput = Console.ReadKey().KeyChar;

                if (menuInput == 'N' || menuInput == 'n')
                {
                    currentPlaceOnList = ConsoleWriter(delimiterChars, currentPlaceOnList, taskLists);
                }
                else if (menuInput == 'I' || menuInput == 'i')
                {
                    MenuPrinter("Enter a new task on a single line.  Hit enter when complete.");
                    string newTask = Console.ReadLine();

                    using (StreamWriter streamLine = File.AppendText(@"Task-List.txt"))
                    {
                        streamLine.WriteLine($"O{delimiterChars}{taskLists.Count + 1}.{delimiterChars}{newTask}");
                    }

                    taskLists = FileReader(fileName);
                    currentPlaceOnList = ConsoleWriter(delimiterChars, 0, taskLists);
                }
                else if (menuInput == 'E' || menuInput == 'e')
                {
                    try
                    {
                        MenuPrinter("Enter the task number you wish to edit, and then press Enter:");
                        int taskNumber = int.Parse(Console.ReadLine());

                        if (taskNumber > taskLists.Count)
                        {
                            MenuPrinter("That task number dosen't exist.  Please try again.");
                        }
                        else
                        {
                            MenuPrinter("Would you like to [D]ismiss, [R]eenter, or [S]tart working the selected task? Or you can go [B]ack.");
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

                                taskLists = FileReader(fileName);
                                taskLists[taskNumber - 1] = $"D{delimiterChars}{statusSeparator[1]}{delimiterChars}{statusSeparator[2]}";
                                FileWriter(delimiterChars, taskLists, fileName);
                                currentPlaceOnList = ConsoleWriter(delimiterChars, 0, taskLists);
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
                                    MenuPrinter("You're already working on a task.  You'll need to dismiss or reenter the task to start a new one.");
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
                                continue;
                            }
                            else
                            {
                                MenuPrinter("Not a valid choice.");
                            }
                        }
                        
                    }
                    catch
                    {
                        MenuPrinter("Not a valid choice.");
                    }

                }
                else if (menuInput == 'X' || menuInput == 'x')
                {
                    MenuPrinter("\n\nGoodbye!\n");
                    Environment.Exit(0);
                }
                else
                {
                    MenuPrinter("Please enter a valid choice."); ;
                }
                menuInput = ' ';
            } while (menuInput == ' ');
        }


        static void MenuPrinter(string menuText)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{menuText}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }


        static List<string> FileReader(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

            var taskList = new List<string>(File.ReadAllLines(fileName));

            return taskList;
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
                taskLists = FileReader(fileName);
            }
        }


        static List<string> ListReorganizer(char delimiterChars, List<string> taskLists)
        {
            string line = taskLists[0];
            string[] statusSeparator = line.Split(delimiterChars);

            try
            {
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
            { ; }

            return taskLists;
        }


        static int ConsoleWriter(char delimiterChars, int currentPlaceOnList, List<string> taskLists)
        {
            Console.Clear();

            if (currentPlaceOnList + 20 > taskLists.Count)
            {
                for (int i = currentPlaceOnList; i < taskLists.Count; i++)
                {
                    WriterLoopTask(taskLists, i);
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

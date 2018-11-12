using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace Messing_Around
{
    internal class Program
    {
        static string getName()
        {
            // Ask for name, get name and return it. Is there a point in modularizing this? maybe not.
            Console.WriteLine("Please enter your name...");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            return name;
        }

        static void setting(string text, string[] commands)
        {
            // Use a generic setting with customized menu.
            Console.WriteLine(text);
            actions(commands);
        }

        static void actions(string[] commands)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[{0}]   ", commands[i].ToUpper());
                Console.ForegroundColor = ConsoleColor.White;
            }
            
            // Start a new line for the response.
            Console.WriteLine();
        }

        static string getAction(string[] commands)
        {
            string text = Console.ReadLine();
            
            while (text != "")
            {
                for (int i = 0; i < commands.Length; i++)
                {
                    if (text.ToLower() == commands[i])
                    {
                        return commands[i];
                    }
                }
                Console.WriteLine("Sorry, that is not a valid option.");
                actions(commands);
                text = Console.ReadLine();
            }

            return "Something went wrong!";
        }

        static bool respond(string command, string[] commands, string[] responses, bool[] cont)
        {
            // Checking entered command against the given commands and giving respective response.
            for (int i = 0; i < commands.Length; i++)
            {
                if (command == commands[i])
                {
                    if (command == "quit")
                    {
                        Environment.Exit(1);
                    }

                    if (!cont[i])
                    {
                        Console.WriteLine(responses[i]);
                        actions(commands);
                        
                        return false;
                    }
                    else // If it's true
                    {
                        Console.WriteLine(responses[i]);
                        
                        return true;
                    }
                }
            }

            return false;
        }

        static void replaceAndSleep(int seconds)
        {
            // Move console write cursor to the beginning of the string to overwrite it.
            Console.CursorTop -= 1;
            Console.CursorLeft = 0;
            
            // Convert to milliseconds and sleep.
            Thread.Sleep(seconds*100);
        }

        static void clearAndSleep(int seconds)
        {
            // Convert to milliseconds and sleep.
            Thread.Sleep(seconds*100);
            
            // Clear the entire console.
            Console.Clear();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Version 0.001");

            string[] commands;
            string[] responses;
            bool[] cont;
            bool next = false;

            Name:
            string name = getName();

            Thread.Sleep(100);
            Console.WriteLine("Your name is {0}? [enter Y or N to proceed]", name);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string choice = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            if (choice == "Y")
            {
                Console.WriteLine("Initializing game.exe");
                replaceAndSleep(5);
                Console.WriteLine("Initializing game.exe.");
                replaceAndSleep(5);
                Console.WriteLine("Initializing game.exe..");
                replaceAndSleep(5);
                Console.WriteLine("Initializing game.exe...");
            }
            else
            {
                goto Name;
            }

            clearAndSleep(3);

            Console.WriteLine(".");
            replaceAndSleep(5);
            Console.WriteLine("..");
            replaceAndSleep(5);
            Console.WriteLine("...");

            clearAndSleep(3);
            
            sceneOne:
            commands = new string[] {"look", "feel", "quit"};
            responses = new string[]
                {"You can't see anything.", "You feel a smooth surface below your feet."};
            cont = new bool[] {false, true, false};
            setting("You appear to be in a dark room, with nothing but your sense of touch to guide you.", commands);

            while (!next)
            {
                next = respond(getAction(commands), commands, responses, cont);
            }
            
            sceneTwo:
            commands = new string[] {"look", "feel more", "quit"};
            responses = new string[]
                {"Surprisingly, you still can't see anything.", "As your reach to feel the ground again, a light above comes to life."};
            cont = new bool[] {false, true, false};
            setting("The room is still dark, but your surroundings almost feel... easier to see.", commands);

            next = false;
            
            while (!next)
            {
                next = respond(getAction(commands), commands, responses, cont);
            }
            
            sceneThree:
            commands = new string[] {"look", "talk", "quit"};
            responses = new string[]
                {"The room is plain white, with a single dark brown wooden door in the center. A burly man stands in front, staring you down.", "You try speaking, but you seem to be muted."};
            cont = new bool[] {false, true, false};
            setting("A light now hands from the ceiling of the short white room, illuminating a single door and a man at its center. He looks intimidating.", commands);

            next = false;
            
            while (!next)
            {
                next = respond(getAction(commands), commands, responses, cont);
            }
        }
    }
}
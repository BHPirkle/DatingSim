using System;
using System.Net;
using System.Net.Mime;
using System.Threading;

namespace MessingAroundMobile
{
    internal class Program
    {
        static void displaySetting(string settingText) // Working on making this print out words character-by-character
        {
            char[] setting = settingText.ToCharArray();
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            
            for (int i = 0; i < setting.Length; i++)
            {
                switch (setting[i])
                {
                    case '\\':
                        // Yellow
                        if (setting[i + 1] == 'Y')
                        {
                            setting[i] = '\0';
                            setting[i + 1] = '\0';
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }

                        // Dark Yellow
                        if (setting[i + 1] == 'D' && setting[i + 2] == 'Y')
                        {
                            setting[i] = '\0';
                            setting[i + 1] = '\0';
                            setting[i + 2] = '\0';
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        
                        // White
                        if (setting[i + 1] == 'W')
                        {
                            setting[i] = '\0';
                            setting[i + 1] = '\0';
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        
                        // Longer pause, seconds based on number after \L (example: \L1 will extend the wait by 1 second)
                        if (setting[i + 1] == 'L' && int.Parse(setting[i + 2].ToString()) > 0)
                        {
                            int customSleep = int.Parse(setting[i + 2].ToString());
                            setting[i] = '\0';
                            setting[i + 1] = '\0';
                            setting[i + 2] = '\0';
                            Thread.Sleep(customSleep * 100);
                        }

                        break;
                    case '.':
                        Console.Write(setting[i]);
                        Thread.Sleep(150);

                        break;
                    case ',':
                        Console.Write(setting[i]);
                        Thread.Sleep(150);

                        break;
                    default:
                        if (setting[i] != '\0')
                        {
                            Console.Write(setting[i]);
                            Thread.Sleep(35);
                        }
                        
                        break;
                }
            }
        }

        static string[] displayActions(string[] actions) // Work on this later
        {
            // Creating actions method, should allow for more... user-friendly input?
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            for (int i = 0; i < actions.Length; i++)
            {
                Console.Write("[{0}]     ", actions[i].ToUpper());
            }
            
            Console.Write("[QUIT]");

            Console.ForegroundColor = ConsoleColor.White;

            return actions;
        }

        static string getAction(string[] actions) // Focus on after text manipulation
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            string action = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            while (action != "")
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    if (action.ToLower() == actions[i])
                    {
                        return actions[i];
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That is not a valid action.");

                Console.ForegroundColor = ConsoleColor.Yellow;
                action = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }

            return "Error";
        }

        static bool getResponse(string action, string[] actions, string[] responses, bool[] next)
        {
            for (int i = 0; i < next.Length; i++)
            {
                if (action.ToLower() == actions[i])
                {
                    if (next[i] == true)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(responses[i]);
                        return false;
                    }
                }
            }

            return false;
        }
        
        static void replaceAndWait(int seconds)
        {
            // Adjusts console cursor to the beginning of the last message to replace it, and then waits for the given time.
            Console.CursorTop -= 1;
            Console.CursorLeft = 0;
            Thread.Sleep(seconds * 100);
        }
        
        public static void Main(string[] args)
        {
            string name = "Dummy";
            string[] actions = { };
            string[] responses = { };
            bool cont = false;
            bool[] next = { };
            
            // Ask for name, use system name as default.
            Console.WriteLine("Is your name {0}?", Environment.UserName);
            Console.WriteLine("ENTER \"Y\" OR \"N\" TO ANSWER]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string answer = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            // If the system name is not preferred, then get the real name.
            if (answer.ToLower() == "n")
            {
                Console.WriteLine("Please enter your preferred name:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Okay, {0}. We will start the program.", name);
            }

            // Lets the user know the program is starting.
            Console.WriteLine("Starting");
            replaceAndWait(3);
            Console.WriteLine("Starting.");
            replaceAndWait(3);
            Console.WriteLine("Starting..");
            replaceAndWait(3);
            Console.WriteLine("Starting...");
            Thread.Sleep(2 * 100);
            Console.Clear();
            
            tutorial:
            actions = new string[] {"continue"};
            next = new bool[] {true};
            responses = new string[] {"Error"};
            
            displaySetting("\\DYWelcome to Seth's Dating Sim, where you can live out your desires of dating.\\L1.\\L1.\\L2 \\YBartosz Burda\\DY!!!!!!!!!!!!");

            while (cont == false)
            {
                cont = getResponse(getAction(displayActions(actions)), actions, responses, next);
            }
            
            sceneOne:
            displaySetting("\\YBart \\DYappears in front of you. What do you do?");
            Console.WriteLine(getAction(displayActions(new string[] {"ATTACK", "FLIRT"})));
        }
    }
}
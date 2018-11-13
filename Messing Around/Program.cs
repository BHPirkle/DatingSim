using System;
using System.Net;
using System.Net.Mime;
using System.Threading;

namespace MessingAroundMobile
{
    internal class Program
    {
        static void setColor(string color)
        {
            switch (color.ToLower())
            {
                case "w":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "g":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "y":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "dy":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "b":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "db":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "r":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "dr":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        static void display(string input) // Making rich text module
        {
            char[] text = input.ToCharArray();
            
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '\\':
                        // Yellow
                        if (text[i + 1] == 'Y')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            setColor("y");
                        }
                        
                        // Blue
                        if (text[i + 1] == 'B')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            setColor("b");
                        }
                        
                        // Red
                        if (text[i + 1] == 'R')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            setColor("r");
                        }
                        
                        // Green
                        if (text[i + 1] == 'G')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            setColor("g");
                        }

                        // Dark Yellow
                        if (text[i + 1] == 'D' && text[i + 2] == 'Y')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            text[i + 2] = '\0';
                            setColor("dy");
                        }
                        
                        // Dark Red
                        if (text[i + 1] == 'D' && text[i + 2] == 'R')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            text[i + 2] = '\0';
                            setColor("dr");
                        }
                        
                        // White
                        if (text[i + 1] == 'W')
                        {
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            setColor("w");
                        }
                        
                        // Longer pause, seconds based on number after \L (example: \L1 will extend the wait by 1 second)
                        if (text[i + 1] == 'L' && int.Parse(text[i + 2].ToString()) > 0)
                        {
                            int customSleep = int.Parse(text[i + 2].ToString());
                            text[i] = '\0';
                            text[i + 1] = '\0';
                            text[i + 2] = '\0';
                            Thread.Sleep(customSleep * 100);
                        }

                        break;
                    case '.':
                        Console.Write(text[i]);
                        Thread.Sleep(150);

                        break;
                    case ',':
                        Console.Write(text[i]);
                        Thread.Sleep(150);

                        break;
                    default:
                        if (text[i] != '\0')
                        {
                            Console.Write(text[i]);
                            Thread.Sleep(35);
                        }
                        
                        break;
                }
            }
        }
        
        static void initializeScene(string settingText, string[] actions, string[] responses, bool[] next)
        {
            bool cont = false;
            
            setColor("dy");

            display(settingText);

            cont = canContinue(actions, responses, next);
            
            while (!cont)
            {
                cont = canContinue(actions, responses, next);
            }
        }

        static string[] displayActions(string[] actions) // Work on this later
        {
            // Creating actions method, should allow for more... user-friendly input?
            Console.WriteLine();
            
            setColor("y");
            
            for (int i = 0; i < actions.Length; i++)
            {
                Console.Write("[{0}]     ", actions[i].ToUpper());
            }
            
            Console.Write("[QUIT]");

            setColor("w");

            return actions;
        }

        static string getAction(string[] actions) // Focus on after text manipulation
        {
            setColor("y");
            Console.WriteLine();
            string action = Console.ReadLine();
            setColor("w");

            while (action != "")
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    if (action.ToLower() == "quit")
                    {
                        Environment.Exit(1);
                    }
                    
                    if (action.ToLower() == actions[i])
                    {
                        return actions[i];
                    }
                }

                setColor("r");
                Console.WriteLine("That is not a valid action.");

                setColor("y");
                action = Console.ReadLine();
                setColor("w");
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
                        setColor("b");
                        Console.WriteLine("[{0}]", actions[i].ToUpper());
                        setColor("w");
                        Console.WriteLine();
                        display(responses[i]);
                        Console.WriteLine();
                        return true;
                    }
                    else
                    {
                        setColor("db");
                        Console.WriteLine("[{0}]", actions[i].ToUpper());
                        setColor("w");
                        Console.WriteLine();
                        display(responses[i]);
                        return false;
                    }
                }
            }

            return false;
        }

        static bool canContinue(string[] actions, string[] responses, bool[] next)
        {
            bool cont = false;
            
            while (cont == false)
            {
                cont = getResponse(getAction(displayActions(actions)), actions, responses, next);
            }
            return cont;
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
            string settingText = "Empty";
            string[] actions = { };
            string[] responses = { };
            bool cont = false;
            bool[] next = { };

            // Ask for name, use system name as default.
            Console.WriteLine("Is your name {0}?", Environment.UserName);
            Console.WriteLine("[ENTER \"Y\" OR \"N\" TO ANSWER]");
            setColor("y");
            string answer = Console.ReadLine();
            setColor("w");

            // If the system name is not preferred, then get the real name.
            if (answer.ToLower() == "n")
            {
                Console.WriteLine("Please enter your preferred name:");
                setColor("y");
                name = Console.ReadLine();
                setColor("w");
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
            // Pretty self-explanatory.
            settingText =
                "\\DYWelcome to Seth's Dating Sim, where you can live out your desires of dating.\\L1.\\L1.\\L2 \\YBartosz Burda\\DY!!!!!!!!!!!!";
            actions = new string[] {"play"};
            next = new bool[] {true};
            responses = new string[]
                {"\\DYIn this game, you will be presented with a \\Yscenario \\DYwith its own \\Yactions\\DY."};
            initializeScene(settingText, actions, responses, next);

            settingText =
                "\\DYTo choose an \\Yaction\\DY, simply type exactly what's in the brackets. (ex. \\Y[CONTINUE] \\DY would be entered as \"\\Ycontinue\\DY\")";
            actions = new string[] {"continue"};
            responses = new string[] {"\\DYGood! Some \\Yactions \\DYwill not lead to a new \\Yscenario\\DY."};
            initializeScene(settingText, actions, responses, next);

            settingText = "\\DYChoose \\Y[USELESS] \\DYto see an example of a limited action.";
            actions = new string[] {"useless", "continue"};
            responses = new string[]
                {"\\DYGreat! Now you can choose \\Y[CONTINUE] \\DYto move on to the real game.\nHave fun!", ""};
            next = new bool[] {false, true};
            initializeScene(settingText, actions, responses, next);

            sceneOne:
            // Initial meet.
            settingText = "\\DYA wild \\YBartosz Burda \\DYappears in front of you!\nWhat should you do?";
            actions = new string[] {"attack", "flirt", "run"};
            next = new bool[] {false, true, false};
            responses = new string[]
            {
                "\\DYYou throw a punch, but he catches it right before it hits him.\n\\YBart: \\W'Sup, you got a snap?\n\\DYYou mumble your Snapchat username, but he doesn't understand.",
                "\\DYYou wink at him awkwardly, your mouth twitching as you try to hold it.", "\\DYYou can't run from fate."
            };
            initializeScene(settingText, actions, responses, next);

            settingText =
                "\\DYBart pulls a \\Ytoothpick \\DYout of a pocket on his jacket sleeve, sliding it between his teeth.\n\\YBart: \\WWeirdo. Hold on, gotta get this egg out of my teeth.";
            actions = new string[] {"flirt", "toothpick?"};
            next = new bool[] {false, true};
            responses = new string[]
            {
                "\\DYYou try winking again, but he continues picking his teeth unimpressed",
                "\\DYYou comment on his \\Ytoothpick\\DY, noting that he looks 10% cooler with it in."
            };
            initializeScene(settingText, actions, responses, next);

            settingText =
                "\\DYHe takes a step forward, putting his hands in his jacket pockets. He moves the \\Ytoothpick \\DYaround in his mouth.";
            actions = new string[] {"flirt", "stupid", "snap"};
            next = new bool[] {false, true, false};
            responses = new string[]
                {"\\DYYou fiddle with your hair, unsure of what other moves you could make besides winking.", "\\DYYou tell him the \\Ytoothpick \\DYmakes him look silly. He bites down and breaks it, the \\Ytoothpick \\DYfalling to the floor.", "\\DYYou open Snapchat on your phone, offering your \\YQR code\\DY. He doesn't move."};
            initializeScene(settingText, actions, responses, next);

            sceneTwo:
            // Fight scene.
            settingText = "\\R\\L1H\\L1e\\L1'\\L1s\\L1 \\L1m\\L1a\\L1d\\L1.";
            actions = new string[] {"attack", "flirt", "run"};
            next = new bool[] {true, false, false};
            responses = new string[] {"\\DRYou throw a punch and hit \\RBart \\DRdirectly in the chest.", "\\DYYou try \\Ydouble-winking \\DYto impress him, but he's teeming with anger.", "\\DYYou think about running, but can't since there's no controls for that."};
            initializeScene(settingText, actions, responses, next);

            settingText = "\\DR-5 HP\n\\RBart: \\WYou're gonna regret that! I'm a freaking \\Rpilot\\W!";
            actions = new string[] {"attack", "apologize", "flirt"};
            next = new bool[] {true, false, false};
            responses = new string[] {""};
        }
    }
}
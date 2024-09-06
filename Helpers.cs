namespace CodeJam4
{
    internal class Helpers
    {
        // Method to prompt the user for input
        public static string? GetUserInput(string prompt)
        {
            Console.Write(prompt + " ");
            Helpers.ResetConsoleColor();
            string? inputLine = Console.ReadLine();

            return string.IsNullOrEmpty(inputLine) ? null : inputLine;
        }

        // Method to remove quotation marks from a file path string
        public static string? RemoveQuotations(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            else
                return path.Trim(new char[] { '"' });
        }

        // Method to change the console text color
        public static void SetConsoleColor(string color)
        {
            switch (color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    break;
            }
        }

        // Method to reset the console text color to white
        public static void ResetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Method to print the app title and start menu
        public static void PrintStartMenu()
        {
            Helpers.ResetConsoleColor();
            Helpers.SetConsoleColor("magenta");
            Console.WriteLine(@"
.-------.  ______      ________         .---.  .---.     .-''-.    .---.     .-------.     .-''-.  .-------.     
\  _(`)_ \|    _ `''. |        |        |   |  |_ _|   .'_ _   \   | ,_|     \  _(`)_ \  .'_ _   \ |  _ _   \    
| (_ o._)|| _ | ) _  \|   .----'        |   |  ( ' )  / ( ` )   ',-./  )     | (_ o._)| / ( ` )   '| ( ' )  |    
|  (_,_) /|( ''_'  ) ||  _|____         |   '-(_{;}_). (_ o _)  |\  '_ '`)   |  (_,_) /. (_ o _)  ||(_ o _) /    
|   '-.-' | . (_) `. ||_( )_   |        |      (_,_) |  (_,_)___| > (_)  )   |   '-.-' |  (_,_)___|| (_,_).' __  
|   |     |(_    ._) '(_ o._)__|        | _ _--.   | '  \   .---.(  .  .-'   |   |     '  \   .---.|  |\ \  |  | 
|   |     |  (_.\.' / |(_,_)            |( ' ) |   |  \  `-'    / `-'`-'|___ |   |      \  `-'    /|  | \ `'   / 
/   )     |       .'  |   |             (_{;}_)|   |   \       /   |        \/   )       \       / |  |  \    /  
`---'     '-----'`    '---'             '(_,_) '---'    `'-..-'    `--------``---'        `'-..-'  ''-'   `'-'                                                                                                      
");
            Helpers.ResetConsoleColor();

            Console.WriteLine("Choose...\n[1] 📄 → 🔤  PDF to Text\n[2] 🔤 → 📄  Text to PDF\n[Q] Quit");
        }

        // Method to prompt user to quit or restart the app
        public static void EndOfProgram()
        {
            string? choice = Helpers.GetUserInput("\n➡️ Press 'Q' to quit, Press 'R' to restart:");

            while (true)
            {
                if (string.IsNullOrEmpty(choice) || (choice.ToLower() != "q" && choice.ToLower() != "r"))
                {
                   Helpers.SetConsoleColor("red");
                   choice = Helpers.GetUserInput("❌ Please make a selection [Q or R]:");
                }
                else break;
            }
            
            if (choice.ToLower().Equals("q"))
            {
                // move this to another function so it can be used at beginning of program as well
                QuitProgram();
                

            }
            else if (choice.ToLower().Equals("r"))
            {
                Console.Clear();
                Program.Main();
            }
        }

        public static void QuitProgram()
        {
            SetConsoleColor("magenta");
            Console.WriteLine("\nThanks for using PDF HELPER!");
            Console.WriteLine(@"
 _______      ____     __   .-''-.  .---.  .---.  
\  ____  \    \   \   /  /.'_ _   \ \   /  \   /  
| |    \ |     \  _. /  '/ ( ` )   '|   |  |   |  
| |____/ /      _( )_ .'. (_ o _)  | \ /    \ /   
|   _ _ '.  ___(_ o _)' |  (_,_)___|  v      v    
|  ( ' )  \|   |(_,_)'  '  \   .---. _ _    _ _   
| (_{;}_) ||   `-'  /    \  `-'    /(_I_)  (_I_)  
|  (_,_)  / \      /      \       /(_(=)_)(_(=)_) 
/_______.'   `-..-'        `'-..-'  (_I_)  (_I_)  
                                                  
");
            ResetConsoleColor();
            Environment.Exit(0);
        }
    }
}
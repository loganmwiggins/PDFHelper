namespace CodeJam4
{
    internal class Program
    {
        public static void Main()
        {
            // Print app title and start menu
            Helpers.PrintStartMenu();

            // Prompt user to choose from menu
            string? choice = Helpers.GetUserInput("\n➡️ Select:");

            // User choice input validation
            while (true)
            {
                // add quit option to beginning of program
                if (string.IsNullOrEmpty(choice) || (choice != "1" && choice != "2" && choice.ToLower() != "q"))
                {
                    Helpers.SetConsoleColor("red");
                    Console.WriteLine("❌ Please enter 1 or 2 to use the helper, or enter Q to quit\n");
                    Helpers.ResetConsoleColor();
                    choice = Helpers.GetUserInput("➡️ Select:");
                }
                else break;
            }
            Console.WriteLine();

            // Handle user choice
            switch (choice.ToLower())
            {
                case "1":
                    Console.Clear();
                    AppLogic.OptPdfToText();
                    break;
                case "2":
                    Console.Clear();
                    AppLogic.OptTextToPdf();
                    break;
                case "q":
                    Helpers.QuitProgram();
                    break;
            }

            // Prompt user to quit or restart
            Helpers.EndOfProgram();
        }
    }
}
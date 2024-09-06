using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;
using System.Reflection;
using System;
using System.IO;
using System.Diagnostics;

namespace CodeJam4
{
    internal class AppLogic
    {
        // USER CHOOSES "[1] PDF TO TEXT"
        public static void OptPdfToText()
        {
            Helpers.SetConsoleColor("yellow");
            Console.WriteLine("Convert Your PDF to Text...\n");
            Helpers.ResetConsoleColor();

            // Prompt for PDF file path
            string? pdfPath = Helpers.GetUserInput("➡️ Enter the path to your PDF file:");
            pdfPath = Helpers.RemoveQuotations(pdfPath);

            // Input validation
            while (true)
            {
                if (!File.Exists(pdfPath) || Path.GetExtension(pdfPath).ToLower() != ".pdf")
                {
                    Helpers.SetConsoleColor("red");
                    pdfPath = Helpers.GetUserInput("❌ Enter a valid path to a PDF file:");
                }
                else break;
            }

            // Call conversion method
            ConvertPdfToText(pdfPath);
        }

        // "PDF TO TEXT" CONVERSION METHOD
        public static void ConvertPdfToText(string path)
        {
            using (var reader = new PdfReader(path))
            {
                var pdf = new PdfDocument(reader);
                var output = new StringBuilder();

                for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                {
                    var pageText = PdfTextExtractor.GetTextFromPage(pdf.GetPage(i));
                    output.Append(pageText);
                }
                pdf.Close();

                var result = output.ToString();

                // Print contents
                Helpers.SetConsoleColor("blue");
                Console.WriteLine($"\n📄 {Path.GetFileName(path)}:");
                Helpers.ResetConsoleColor();
                Console.WriteLine("----------");
                Console.WriteLine(result);
                Console.WriteLine("----------");
            }
        }

        // USER CHOOSES "[2] TEXT TO PDF"
        public static void OptTextToPdf()
        {
            Helpers.SetConsoleColor("yellow");
            Console.WriteLine("Create Your PDF...\n");
            Helpers.ResetConsoleColor();

            // Prompt user for PDF file name
            string? name = Helpers.GetUserInput("➡️ Enter a file name for your new PDF:");

            // Input validation
            while (true)
            {
                if (string.IsNullOrEmpty(name) 
                    
                ) {
                    Helpers.SetConsoleColor("red");
                    Console.WriteLine("❌ File name cannot be empty\n");
                    Helpers.ResetConsoleColor();
                    name = Helpers.GetUserInput("➡️ Enter a file name for your new PDF:");
                }
                else if (name.Contains("?")
                    || name.Contains("\\")
                    || name.Contains("/")
                    || name.Contains(":")
                    || name.Contains("*")
                    || name.Contains("?")
                    || name.Contains("\"")
                    || name.Contains("<")
                    || name.Contains(">")
                    || name.Contains("|"))
                {
                    Helpers.SetConsoleColor("red");
                    Console.WriteLine("❌ File name cannot contain: \\ / : * ? \" < > |\n");
                    Helpers.ResetConsoleColor();
                    name = Helpers.GetUserInput("➡️ Enter a name for your new PDF file:");
                }
                else if (name.Length > 150)
                {
                    Helpers.SetConsoleColor("red");
                    Console.WriteLine("❌ File name cannot exceed 150 characters\n");
                    Helpers.ResetConsoleColor();
                    name = Helpers.GetUserInput("➡️ Enter a name for your new PDF file:");
                }
                else break;
            }

            string? header = Helpers.GetUserInput("➡️ Enter a header for your new PDF file (press Enter to skip):");

            Console.WriteLine("➡️ Enter PDF content (press Ctrl+Z to end):");
            Console.WriteLine("----------");
            string txt = GetMultiLineTxt(name);

            if (string.IsNullOrEmpty(header))
            {
                ConvertTextToPdf(txt, name);
            }
            else PdfHeader.ConvertTextToPdfWithHeader(txt, name, header);
        }

        // "TEXT TO PDF" CONVERSION METHOD
        public static void ConvertTextToPdf(string content, string name)
        {
            using (var document = new Document(new PdfDocument(new PdfWriter(name + ".pdf"))))
            {
                document.Add(new Paragraph(content));
            }
        }

        public static string GetMultiLineTxt(string name)
        {
            StringBuilder inputText = new StringBuilder();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(intercept: true); // Intercept so the key isn't displayed

                    // CTRL+Z is detected when both Control key and 'Z' key are pressed.
                    if (keyInfo.Key == ConsoleKey.Z && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Console.WriteLine("\n----------");
                        Helpers.SetConsoleColor("green");
                        Console.WriteLine($"\n✅ You successfully created a PDF called {name}.pdf!");
                        string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        string parentDirectory = Directory.GetParent(currentPath).Parent.Parent.FullName; //getting the parent directory of the project (should be portable across machines)
                        parentDirectory = (parentDirectory + "\\" + name + ".pdf"); //appending the pdf file to the path
                        Console.WriteLine("Copy this path into your file explorer to view your brand new PDF! : " + parentDirectory);
                        Helpers.ResetConsoleColor();
                        break;
                    }

                    // Check for Enter key to add a new line in the input
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        inputText.Append(Environment.NewLine);
                        Console.WriteLine();
                    }
                    else
                    {
                        // Append the current key to the input
                        inputText.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar); // Display the key in the console
                    }
                }
            }
            return inputText.ToString();
        }
    }
}

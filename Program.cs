
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StarCitizenTranslator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the path to the input file (EN):");
            string inputFilePath = Console.ReadLine();
            inputFilePath = inputFilePath.Replace("\"", "");
            Console.WriteLine("Enter the path to the output file (de, es, ...):");
            string outputFilePath = Console.ReadLine();
            outputFilePath = outputFilePath.Replace("\"", "");

            int startLine = 0;
            if(File.Exists(outputFilePath))
                startLine = File.ReadLines(outputFilePath).Count();
            using StreamReader reader = new StreamReader(inputFilePath);
            using StreamWriter writer = new StreamWriter(outputFilePath, true, new UTF8Encoding(true));
            int currentLine = 0;
            string line;
            while((line = await reader.ReadLineAsync()) != null)
            {
                currentLine++;
                if(currentLine <= startLine) // Skip already translated lines
                    continue;
                string[] parts = line.Split('=');
                if(parts.Length == 2)
                {
                    string leftPart = parts[0].Trim();
                    string rightPart = parts[1].Trim();

                    try
                    {
                        // if left part starts with Human, vehicle_Name or item_Name, copy the line from the input file
                        if(leftPart.StartsWith("Human"))
                        {
                            Console.WriteLine($"Copying line {currentLine}: {rightPart}");
                            await writer.WriteLineAsync(line);
                            continue;
                        }
                        if(leftPart.StartsWith("vehicle_Name"))
                        {
                            Console.WriteLine($"Copying line {currentLine}: {rightPart}");
                            await writer.WriteLineAsync(line);
                            continue;
                        }
                        if(leftPart.StartsWith("item_Name"))
                        {
                            Console.WriteLine($"Copying line {currentLine}: {rightPart}");
                            await writer.WriteLineAsync(line);
                            continue;
                        }
                        Console.Write($"Translating line {currentLine}: {rightPart}");
                        string translatedText = await TranslationHelper.TranslateText(rightPart);
                        Console.WriteLine($" -> {translatedText}");
                        await writer.WriteLineAsync($"{leftPart}={translatedText}");
                    } catch(Exception ex)
                    {
                        Console.WriteLine($"Error translating line {currentLine}: {ex.Message}");
                        break;
                    }
                }
            }
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}

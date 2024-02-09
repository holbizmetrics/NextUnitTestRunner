using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NextUnit.TestRunner.Extensions
{
    public static class ConsoleExtensions
    {
        public static void WriteColoredLine(this string message)
        {
            var colorRegex = new Regex("<(.*?)>(.*?)</\\1>", RegexOptions.Singleline);
            int lastPosition = 0;

            foreach (Match match in colorRegex.Matches(message))
            {
                Console.Write(message.Substring(lastPosition, match.Index - lastPosition));

                if (Enum.TryParse(match.Groups[1].Value, true, out ConsoleColor color))
                {
                    Console.ForegroundColor = color;
                }
                else
                {
                    // If we can't parse the color, write the text without coloring.
                    Console.Write(match.Groups[0].Value);
                    lastPosition = match.Index + match.Length;
                    continue;
                }

                Console.Write(match.Groups[2].Value);
                Console.ResetColor();
                lastPosition = match.Index + match.Length;
            }

            // Print the remainder of the message
            if (lastPosition < message.Length)
            {
                Console.Write(message.Substring(lastPosition));
            }
            Console.WriteLine();
        }
    }
}
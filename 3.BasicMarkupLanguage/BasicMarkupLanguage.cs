namespace _3.BasicMarkupLanguage
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class BasicMarkupLanguage
    {
        // WTF? :D
        private const string Pattern =
            @"\s*<\s*(?<command>inverse|reverse|repeat|stop)\s*(?:value\s*=\s*\""\s*(?<repeats>[0-9]|10)\s*\""\s*)?(?:content\s*=\s*\""(?<content>[^\""]{0,50})\s*\""\s*)?\/\s*>";

        public static void Main()
        {
            var input = Console.ReadLine();
            var lineNumber = 1;
            while (true)
            {
                var regex = new Regex(Pattern);
                var decodedInput = regex.Match(input);

                var commandName = decodedInput.Groups["command"].Value;

                if (commandName.Equals("stop"))
                {
                    break;
                }

                if (!string.IsNullOrEmpty(decodedInput.Groups["content"].Value))
                {
                    switch (commandName.ToLower())
                    {
                        case "inverse":
                            Inverse(decodedInput.Groups["content"].Value, lineNumber);
                            lineNumber++;
                            break;
                        case "reverse":
                            Reverse(decodedInput.Groups["content"].Value, lineNumber);
                            lineNumber++;
                            break;
                        case "repeat":
                            Reapeat(decodedInput.Groups["content"].Value, int.Parse(decodedInput.Groups["repeats"].Value), lineNumber);
                            lineNumber += int.Parse(decodedInput.Groups["repeats"].Value);
                            break;
                        default: throw new NotImplementedException();
                    }
                }

                input = Console.ReadLine();
            }
        }

        private static void Reapeat(string text, int repetitions, int lineNumber)
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"{lineNumber + i}. {text}");
            }
        }

        private static void Reverse(string text, int lineNumber)
        {
            Console.WriteLine($"{lineNumber}. {new string(text.ToCharArray().Reverse().ToArray())}");
        }

        private static void Inverse(string text, int lineNumber)
        {
            Console.WriteLine($"{lineNumber}. {new string(text.Select(ch => char.IsLower(ch) ? char.ToUpper(ch) : char.IsUpper(ch) ? char.ToLower(ch) : ch).ToArray())}");
        }
    }
}
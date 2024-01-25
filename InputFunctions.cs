using System.Text.RegularExpressions;

namespace SVGStyleChanger
{
    public class InputFunctions
    {
        public static bool GetAddIfNotExist()
        {
            Console.Clear();
            Console.WriteLine("Would you like to add the attribute/value you have chosen when a path does not have the attribute? (y/n)");
            ConsoleKey key;
            while (true)
            {
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (key == ConsoleKey.N)
                {
                    return false;
                }
            }

        }

        public static string[] GetAttributeValues(string[] attributeNames)
        {
            string[] attributeValues = new string[attributeNames.Length];
            for (int i = 0; i < attributeNames.Length; i++)
            {
                Console.Clear();
                string attributeName = attributeNames[i];
                // Find current attribute by name
                SvgAttribute currentAttribute = null;
                foreach (SvgAttribute svgattribute in SvgAttributes.GetAttributes())
                {
                    if (svgattribute.Name == attributeName) { currentAttribute = svgattribute; }
                }
                bool valid = false;
                while (!valid)
                {
                    Console.WriteLine("Enter a value for " + attributeName);
                    string value = Console.ReadLine().Trim();
                    switch (currentAttribute.Type)
                    {
                        case AttributeType.Color:
                            //Check if it's a valid color
                            Regex hexColorRegex = new Regex("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
                            if (hexColorRegex.IsMatch(value))
                            {
                                valid = true;
                                attributeValues[i] = value;
                            }
                            else
                            {
                                Console.WriteLine($"Invalid color value: {value}");
                                Console.WriteLine("The color should be in the format #RRGGBB (hexadecimal).");
                            }
                            break;
                        case AttributeType.Numeric:
                            // Check if it's a valid numeric value
                            if (double.TryParse(value, out double numericValue))
                            {
                                valid = true;
                                attributeValues[i] = numericValue.ToString();
                            }
                            else
                            {
                                Console.WriteLine($"Invalid numeric value: {value}");
                                Console.WriteLine("The value should be a valid numeric format.");
                            }
                            break;
                        case AttributeType.Enumerated:
                            // Check if it's one of the acceptable enumerated values
                            List<string> acceptableValues = currentAttribute.AcceptableValues;
                            if (acceptableValues.Contains(value))
                            {
                                valid = true;
                                attributeValues[i] = value;
                            }
                            else
                            {
                                Console.WriteLine($"Invalid value: {value}");
                                Console.WriteLine("The value should be one of the following:");
                                foreach (string acceptableValue in acceptableValues)
                                {
                                    Console.WriteLine("\t" + acceptableValue);
                                }
                            }
                            break;
                    }
                }
            }
            return attributeValues;
        }

        public static string[] GetAttributeNames()
        {
            List<SvgAttribute> attributes = SvgAttributes.GetAttributes();
            List<string> attributeNames = new List<string>();
            foreach (SvgAttribute attribute in attributes)
            {
                attributeNames.Add(attribute.Name);
            }
            return DisplayOptionsAndGetSelection(attributeNames).ToArray();
        }

        public static List<string> DisplayOptionsAndGetSelection(List<string> options)
        {
            options.Add("Confirm Selection");
            int currentIndex = 0;
            List<int> selectedIndices = new List<int>();
            ConsoleKey key;

            Console.CursorVisible = false;
            bool confirmed = false;
            while (!confirmed)
            {
                Console.Clear();
                for (int i = 0; i < options.Count; i++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;

                    if (selectedIndices.Contains(i))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (i == currentIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    if (options[i] == "Confirm Selection")
                    {
                        Console.WriteLine();
                    }

                    Console.WriteLine($"{options[i]}");
                }

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow && currentIndex > 0)
                {
                    currentIndex--;
                }
                else if (key == ConsoleKey.DownArrow && currentIndex < options.Count - 1)
                {
                    currentIndex++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (options[currentIndex] == "Confirm Selection")
                    {
                        confirmed = true;
                    }
                    else
                    {
                        if (selectedIndices.Contains(currentIndex))
                        {
                            selectedIndices.Remove(currentIndex);
                        }
                        else
                        {
                            selectedIndices.Add(currentIndex);
                        }
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.CursorVisible = true;
            return options.Where((option, index) => selectedIndices.Contains(index)).ToList();
        }

        public static string GetPath(string prompt, bool mustExist)
        {
            Console.Clear();
            string path = "";
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine(prompt);
                path = Console.ReadLine().Trim();
                if (Directory.Exists(path) || mustExist == false)
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine($"Path not found: {path}");
                }
            }
            return path;
        }
    }
}

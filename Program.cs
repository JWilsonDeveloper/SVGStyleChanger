using System.Text.RegularExpressions;

namespace SVGStyleChanger
{
    public class Program
    {
        static void Main()
        {
            Parameters parameters = GetParameters();
            
            string sourceFolderPath = parameters.SourceFolderPath; // Set source folder holding svg files
            string destinationFolderPath = parameters.DestinationFolderPath; // Set destination folder path (it will be created if it doesn't exist)
            string[] attributeNames = parameters.AttributeNames; // Set the attributes to change (e.g. { "stroke", "stroke-width", "fill" })
            string[] attributeValues = parameters.AttributeValues; // Set the attributes' values 
            bool addIfNotExists = parameters.AddIfNotExist; // True if attributes should be added on paths where they currently don't exist

            // Create the destination folder if it doesn't exist
            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }

            // Get all SVG files in the source folder
            string[] svgFiles = Directory.GetFiles(sourceFolderPath, "*.svg");

            foreach (var svgFilePath in svgFiles)
            {
                // Read SVG content from the source file
                string svgContent = File.ReadAllText(svgFilePath);

                Console.WriteLine($"Original SVG Content of {Path.GetFileName(svgFilePath)}:");
                Console.WriteLine(svgContent);

                // Modify path
                string modifiedSvgContent = ChangePathAttributes(svgContent, attributeNames, attributeValues, addIfNotExists);

                Console.WriteLine($"Modified SVG Content of {Path.GetFileName(svgFilePath)}:");
                Console.WriteLine(modifiedSvgContent);

                // Create the destination file path
                string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(svgFilePath));

                // Write the modified content to the destination file
                File.WriteAllText(destinationFilePath, modifiedSvgContent);

                Console.WriteLine($"Path changed successfully for {Path.GetFileName(svgFilePath)}!");
                Console.WriteLine("------------------------------");
            }

            Console.ReadLine();
        }

        static Parameters GetParameters()
        {
            Parameters parameters = new Parameters();
            parameters.SourceFolderPath = InputFunctions.GetPath("Type in the path for the folder that contains your existing .svg files", true);
            
            parameters.DestinationFolderPath = InputFunctions.GetPath("Type in the path for the folder where you would like to add your new .svg files", false);
            parameters.AttributeNames = InputFunctions.GetAttributeNames();
            parameters.AttributeValues = InputFunctions.GetAttributeValues(parameters.AttributeNames);
            parameters.AddIfNotExist = InputFunctions.GetAddIfNotExist();
            Console.Clear();
            parameters.PrintParameters();
            Console.ReadLine() ;

            return parameters;
        }

        

        static string ChangePathAttributes(string svgContent, string[] attributeNames, string[] attributeValues, bool addIfNotExists)
        {
            if (attributeNames.Length != attributeValues.Length)
            {
                throw new ArgumentException("Number of attribute names must match the number of attribute values.");
            }

            // Construct the pattern dynamically based on the provided attribute names
            string pattern = @"<path([^>]*)style=""([^""]*)""([^>]*)>";

            // Replace attribute values in SVG content for <path> elements
            string modifiedSvg = Regex.Replace(svgContent, pattern, match =>
            {
                // Extract existing values
                string pathAttributes = match.Groups[1].Value;
                string styleAttributes = match.Groups[2].Value;
                string remainingAttributes = match.Groups[3].Value;

                // Construct the new attributes with extracted variables
                string newStyleAttributes = GetNewStyleAttributes(styleAttributes, attributeNames, attributeValues, addIfNotExists);

                // Construct the new path tag with the new attributes
                string newPathTag = $"<path{pathAttributes}style=\"{newStyleAttributes}\"{remainingAttributes}>";

                return newPathTag;
            });

            return modifiedSvg;
        }

        static string GetNewStyleAttributes(string existingStyle, string[] attributeNames, string[] attributeValues, bool addIfNotExists)
        {
            List<string> styleAttributes = existingStyle.Split(';').Select(attr => attr.Trim()).ToList();

            for (int i = 0; i < attributeNames.Length; i++)
            {
                int index = styleAttributes.FindIndex(attr => attr.StartsWith($"{attributeNames[i]}:"));

                if (index != -1)
                {
                    styleAttributes[index] = $"{attributeNames[i]}:{attributeValues[i]}";
                }
                else if (addIfNotExists)
                {
                    styleAttributes.Add($"{attributeNames[i]}:{attributeValues[i]}");
                }
            }

            return string.Join(";", styleAttributes);
        }

    }
}
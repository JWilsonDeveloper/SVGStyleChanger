namespace SVGStyleChanger
{
    public class Parameters
    {
        private string? _sourceFolderPath;
        private string? _destinationFolderPath;
        private string[]? _attributeNames;
        private string[]? _attributeValues;
        private bool _addIfNotExist;

        public string? SourceFolderPath { get => _sourceFolderPath; set => _sourceFolderPath = value; }
        public string? DestinationFolderPath { get => _destinationFolderPath; set => _destinationFolderPath = value; }
        public string[]? AttributeNames { get => _attributeNames; set => _attributeNames = value; }
        public string[]? AttributeValues { get => _attributeValues; set => _attributeValues = value; }
        public bool AddIfNotExist { get => _addIfNotExist; set => _addIfNotExist = value; }

        public void PrintParameters()
        {
            if(SourceFolderPath!= null)
            {
                Console.WriteLine("SourceFolderPath: " + SourceFolderPath);
            }
            if (DestinationFolderPath != null)
            {
                Console.WriteLine("DestinationFolderPath: " + DestinationFolderPath);
            }
            if (AttributeNames != null)
            {
                Console.WriteLine("AttributeNames: ");
                foreach(string attributeName in AttributeNames)
                {
                    Console.WriteLine("\t" + attributeName);
                }
            }
            if (AttributeValues != null)
            {
                Console.WriteLine("AttributeValues: ");
                foreach (string attributeValue in AttributeValues)
                {
                    Console.WriteLine("\t" + attributeValue);
                }
            }
            Console.WriteLine("AddIfNotExist: " + AddIfNotExist);
        }
    }
}

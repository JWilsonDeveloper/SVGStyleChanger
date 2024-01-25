namespace SVGStyleChanger
{
    public class SvgAttribute
    {
        public string Name { get; set; }
        public AttributeType Type { get; set; }
        public List<string> AcceptableValues { get; set; }

        public SvgAttribute(string name, AttributeType type, List<string> acceptableValues = null)
        {
            Name = name;
            Type = type;
            AcceptableValues = acceptableValues ?? new List<string>();
        }
    }
}

namespace SVGStyleChanger
{
    public static class SvgAttributes
    {
        public static List<SvgAttribute> GetAttributes()
        {
            return new List<SvgAttribute>
            {
                new SvgAttribute("stroke", AttributeType.Color),
                new SvgAttribute("stroke-width", AttributeType.Numeric),
                new SvgAttribute("stroke-linecap", AttributeType.Enumerated, new List<string> { "butt", "round", "square" }),
                new SvgAttribute("stroke-linejoin", AttributeType.Enumerated, new List<string> { "miter", "round", "bevel" }),
                new SvgAttribute("stroke-opacity", AttributeType.Numeric),
                new SvgAttribute("fill", AttributeType.Color),
                new SvgAttribute("fill-opacity", AttributeType.Numeric),
                new SvgAttribute("fill-rule", AttributeType.Enumerated, new List<string> { "nonzero", "evenodd" }),
                new SvgAttribute("opacity", AttributeType.Numeric),
                new SvgAttribute("visibility", AttributeType.Enumerated, new List<string> { "visible", "hidden", "collapse" })
            };
        }
    }
}

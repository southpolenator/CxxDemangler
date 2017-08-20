namespace CxxDemangler.Parsers
{
    internal class DestructorName : IParsingResult
    {
        public DestructorName(IParsingResult name)
        {
            Name = name;
        }

        public IParsingResult Name { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = UnresolvedType.Parse(context) ?? SimpleId.Parse(context);

            if (name != null)
            {
                return new DestructorName(name);
            }
            return null;
        }
    }
}

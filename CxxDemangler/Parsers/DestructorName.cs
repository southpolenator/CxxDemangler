namespace CxxDemangler.Parsers
{
    internal class DestructorName : IParsingResult
    {
        public IParsingResult Name { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = UnresolvedType.Parse(context) ?? SimpleId.Parse(context);

            if (name != null)
            {
                return new DestructorName()
                {
                    Name = name,
                };
            }
            return null;
        }
    }
}

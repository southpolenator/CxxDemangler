using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CxxDemangler.Parsers
{
    internal class UnscopedTemplateName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = UnscopedName.Parse(context);

            if (name != null)
            {
                // TODO: context.SubstitutionTable.Add(name);
                return name;
            }

            return Substitution.Parse(context);
        }
    }
}

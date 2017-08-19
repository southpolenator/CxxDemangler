using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CxxDemangler
{
    internal struct RewindState
    {
        public int Position { get; set; }
    }

    internal struct ParsingContext
    {
        public SimpleStringParser Parser { get; set; }

        public SubstitutionTable SubstitutionTable { get; set; }

        public RewindState RewindState
        {
            get
            {
                return new RewindState()
                {
                    Position = Parser.Position,
                };
            }
        }

        public void Rewind(RewindState rewind)
        {
            Parser.Position = rewind.Position;
        }
    }
}

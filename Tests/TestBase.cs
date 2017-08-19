using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CxxDemangler.Tests
{
    public abstract class TestBase
    {
        internal abstract IParsingResult Parse(ParsingContext context);

        internal IParsingResult Parse(string input)
        {
            ParsingContext context = CxxDemangler.CreateContext(input);

            return Parse(context);
        }

        internal void Verify(string input, IParsingResult expected)
        {
            ParsingContext context = CxxDemangler.CreateContext(input);
            IParsingResult actual = Parse(context);

            CompareParsingResult(expected, actual);
            if (input.EndsWith("..."))
            {
                Assert.AreEqual(input.Length - 3, context.Parser.Position, "Not everything was parsed");
            }
        }

        protected static void CompareParsingResult(object expected, object actual, string path = "")
        {
            Assert.AreEqual(expected == null, actual == null, $"Different null tests at:\n{path}");
            if (expected == null && actual == null)
            {
                return;
            }

            Type type = expected.GetType();

            if (typeof(IParsingResult).IsAssignableFrom(type))
            {
                Assert.AreEqual(expected.GetType(), actual.GetType(), $"Different types at:\n{path}");
                foreach (PropertyInfo property in type.GetProperties())
                {
                    CompareParsingResult(property.GetValue(expected), property.GetValue(actual), $"{path}\n{type.FullName}.{property.Name}");
                }
            }
            else if (typeof(IEnumerable<IParsingResult>).IsAssignableFrom(type))
            {
                IParsingResult[] expectedArray = ((IEnumerable<IParsingResult>)expected).ToArray();
                IParsingResult[] actualArray = ((IEnumerable<IParsingResult>)actual).ToArray();

                Assert.AreEqual(expectedArray.Length, actualArray.Length, $"Different lengths at:\n{path}");
                for (int i = 0; i < expectedArray.Length; i++)
                {
                    CompareParsingResult(expectedArray[i], actualArray[i], $"{path}[{i}]");
                }
            }
            else
            {
                Assert.AreEqual(expected, actual, $"Different values at:\n{path}");
            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CxxDemangler.Tests
{
    public abstract class TestBase
    {
        internal abstract IParsingResult Parse(ParsingContext context);

        internal virtual IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield break;
        }

        internal ParsingContext CreateContext(string input)
        {
            ParsingContext context = CxxDemangler.CreateContext(input);

            foreach (IParsingResult substitution in SubstitutionTableList())
            {
                context.SubstitutionTable.Add(substitution);
            }
            return context;
        }

        internal IParsingResult Parse(string input)
        {
            ParsingContext context = CreateContext(input);

            return Parse(context);
        }

        internal void Verify(string input, IParsingResult expected, string endsWith = "...")
        {
            ParsingContext parsingContext = CreateContext(input);
            IParsingResult result = Parse(parsingContext);

            CompareParsingResult(expected, result);
            Assert.IsTrue(string.IsNullOrEmpty(endsWith) || input.EndsWith(endsWith));
            if (!string.IsNullOrEmpty(endsWith))
            {
                Assert.AreEqual(input.Length - endsWith.Length, parsingContext.Parser.Position, "Not everything was parsed");
            }

            // Demangle to verify that there are no exceptions
            DemanglingContext demanglingContext = DemanglingContext.Create(parsingContext, true);

            result?.Demangle(demanglingContext);
            System.Console.WriteLine(demanglingContext.Writer.Text);
        }

        protected static void CompareParsingResult(object expected, object actual, string path = "")
        {
            Assert.AreEqual(expected == null, actual == null, $"Different null tests at: {path}");
            if (expected == null && actual == null)
            {
                return;
            }

            System.Type type = expected.GetType();

            if (typeof(IParsingResult).IsAssignableFrom(type))
            {
                Assert.AreEqual(expected.GetType(), actual.GetType(), $"Different types at: {path}");
                foreach (PropertyInfo property in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                {
                    CompareParsingResult(property.GetValue(expected), property.GetValue(actual), $"{path}\n{type.FullName}.{property.Name}");
                }
            }
            else if (typeof(IEnumerable<IParsingResult>).IsAssignableFrom(type))
            {
                IParsingResult[] expectedArray = ((IEnumerable<IParsingResult>)expected).ToArray();
                IParsingResult[] actualArray = ((IEnumerable<IParsingResult>)actual).ToArray();

                Assert.AreEqual(expectedArray.Length, actualArray.Length, $"Different lengths at: {path}");
                for (int i = 0; i < expectedArray.Length; i++)
                {
                    CompareParsingResult(expectedArray[i], actualArray[i], $"{path}[{i}]");
                }
            }
            else
            {
                Assert.AreEqual(expected, actual, $"Different values at: {path}");
            }
        }
    }
}

using System.Text;

namespace Kamishibai.CodeAnalysis.Generator
{
    public abstract class TemplateBase
    {
        protected StringBuilder GenerationEnvironment { get; } = new();

        public void Write(string text)
        {
            GenerationEnvironment.Append(text);
        }

        public class ToStringInstanceHelper
        {
            public string ToStringWithCulture(object objectToConvert)
            {
                return (string)objectToConvert;
            }
        }

        public ToStringInstanceHelper ToStringHelper { get; } = new();
    }
}
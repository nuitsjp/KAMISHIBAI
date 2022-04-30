using Codeer.TestAssistant.GeneratorToolKit;

namespace Driver.InTarget
{
    // ReSharper disable once UnusedMember.Global
    public class NamingRule : IDriverElementNameGenerator
    {
        public int Priority => 1;

        public string GenerateName(object target)
        {
            return string.Empty;
        }
    }
}

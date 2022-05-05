using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;

namespace Driver.InTarget
{
    // ReSharper disable once UnusedMember.Global
    public class WindowAnalysisMenuAction : IWindowAnalysisMenuAction
    {
        public Dictionary<string, MenuAction> GetAction(object target, WindowAnalysisTreeInfo info)
        {
            var dic = new Dictionary<string, MenuAction>();
            return dic;
        }
    }
}

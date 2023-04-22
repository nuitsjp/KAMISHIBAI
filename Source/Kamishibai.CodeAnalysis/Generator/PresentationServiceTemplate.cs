﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 17.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Kamishibai.CodeAnalysis.Generator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class PresentationServiceTemplate : TemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("#pragma warning disable CS1591 \r\n// <auto-generated />\r\n#nullable enable\r\nusing S" +
                    "ystem;\r\nusing System.Threading.Tasks;\r\nusing Kamishibai;\r\n\r\nnamespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            this.Write("\r\n{\r\n    public partial interface IPresentationService : IPresentationServiceBase" +
                    "\r\n    {\r\n");

foreach(var navigationInfo in NavigationInfos)
{

            this.Write("        Task<bool> NavigateTo");
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationInfo.NavigationName));
            this.Write("Async(");
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationInfo.NavigationParameters));
            this.Write(");\r\n");
  
}

foreach(var openWindowInfo in OpenWindowInfos)
{

            this.Write("        Task Open");
            this.Write(this.ToStringHelper.ToStringWithCulture(openWindowInfo.NavigationName));
            this.Write("WindowAsync(");
            this.Write(this.ToStringHelper.ToStringWithCulture(openWindowInfo.NavigationParameters));
            this.Write(");\r\n");
  
}

foreach(var openDialogInfo in OpenDialogInfos)
{

            this.Write("        Task Open");
            this.Write(this.ToStringHelper.ToStringWithCulture(openDialogInfo.NavigationName));
            this.Write("DialogAsync(");
            this.Write(this.ToStringHelper.ToStringWithCulture(openDialogInfo.NavigationParameters));
            this.Write(");\r\n");
  
}

            this.Write(@"    }

    public class PresentationService : PresentationServiceBase, IPresentationService
    {
        private readonly IServiceProvider _serviceProvider;

        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base (serviceProvider, navigationFrameProvider, windowService)
        {
            _serviceProvider = serviceProvider;
        }

");

foreach(var navigationInfo in NavigationInfos)
{

            this.Write("        public Task<bool> NavigateTo");
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationInfo.NavigationName));
            this.Write("Async(");
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationInfo.NavigationParameters));
            this.Write(")\r\n        {\r\n            return NavigateAsync(\r\n                new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationInfo.ViewModelName));
            this.Write("(\r\n                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationInfo.ConstructorParameters));
            this.Write("\r\n                ), \r\n                frameName);\r\n        }\r\n");
  
}

foreach(var openWindowInfo in OpenWindowInfos)
{

            this.Write("        public Task Open");
            this.Write(this.ToStringHelper.ToStringWithCulture(openWindowInfo.NavigationName));
            this.Write("WindowAsync(");
            this.Write(this.ToStringHelper.ToStringWithCulture(openWindowInfo.NavigationParameters));
            this.Write(")\r\n        {\r\n            return OpenWindowAsync(\r\n                new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(openWindowInfo.ViewModelName));
            this.Write("(\r\n                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(openWindowInfo.ConstructorParameters));
            this.Write("\r\n                ), \r\n                owner,\r\n                options);\r\n       " +
                    " }\r\n");
  
}

foreach(var openDialogInfo in OpenDialogInfos)
{

            this.Write("        public Task Open");
            this.Write(this.ToStringHelper.ToStringWithCulture(openDialogInfo.NavigationName));
            this.Write("DialogAsync(");
            this.Write(this.ToStringHelper.ToStringWithCulture(openDialogInfo.NavigationParameters));
            this.Write(")\r\n        {\r\n            return OpenDialogAsync(\r\n                new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(openDialogInfo.ViewModelName));
            this.Write("(\r\n                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(openDialogInfo.ConstructorParameters));
            this.Write("\r\n                ), \r\n                owner,\r\n                options);\r\n       " +
                    " }\r\n");
  
}

            this.Write("    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
}

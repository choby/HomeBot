#pragma checksum "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3d58297b90abd146ffc29c930d488cd498dbd886"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.razor-page", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/_ViewImports.cshtml"
using HomeBot;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/_ViewImports.cshtml"
using HomeBot.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
using HomeBot.Infrastructure.Db.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d58297b90abd146ffc29c930d488cd498dbd886", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c934d46b871f421a3e5b104047beb737082fb45e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
  
  
    var logs = (IEnumerable<Log>)ViewBag.Logs;

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div id=\"myCarousel\" class=\"carousel slide\" data-ride=\"carousel\" data-interval=\"6000\">\n  <table style=\"width:100%\">\n      <tbody>\n          \n");
#nullable restore
#line 12 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
                
                  foreach (var log in logs)
                  {

#line default
#line hidden
#nullable disable
            WriteLiteral("                      <tr>\n                          <td style=\"width:70%\">");
#nullable restore
#line 16 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
                                            Write(log.Info);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                          <td style=\"width:10%\">");
#nullable restore
#line 17 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
                                            Write(log.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                           <td style=\"width:20%\">");
#nullable restore
#line 18 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
                                             Write(log.DateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                         \n                      </tr>\n");
#nullable restore
#line 21 "/Users/choby/Projects/HomeBot/HomeBot/HomeBot.App/Views/Home/Index.cshtml"
                  }
              

#line default
#line hidden
#nullable disable
            WriteLiteral("             \n        \n      </tbody>\n  </table>\n</div>\n\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Views_Home_Index> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_Index> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_Index>)PageContext?.ViewData;
        public Views_Home_Index Model => ViewData.Model;
    }
}
#pragma warning restore 1591

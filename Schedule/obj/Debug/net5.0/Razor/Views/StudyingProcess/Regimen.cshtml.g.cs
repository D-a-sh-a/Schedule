#pragma checksum "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "fe0c1815298850e20d7c4b631c1d9a1ab16f43d0833f7280fd7168e8acc31338"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_StudyingProcess_Regimen), @"mvc.1.0.view", @"/Views/StudyingProcess/Regimen.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\_ViewImports.cshtml"
using Schedule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\_ViewImports.cshtml"
using Schedule.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
using Schedule.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
using Schedule.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"fe0c1815298850e20d7c4b631c1d9a1ab16f43d0833f7280fd7168e8acc31338", @"/Views/StudyingProcess/Regimen.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"33177c27ab27bfeb11175e8c572c05fe350a55d243517e73244cb447af0310c0", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_StudyingProcess_Regimen : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RegimenViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t<div class=\"row row-cols-1 row-cols-md-2 g-5\">\r\n");
#nullable restore
#line 7 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
   foreach (var day in Model.days)
		{
			var counter = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t<div class=\"col\">\r\n\t\t\t\t<table class=\"table\">\r\n\t\t\t\t\t<caption><h5>");
#nullable restore
#line 12 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
             Write(DaysEnum.GetDayName(day.day));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5></caption>\r\n");
#nullable restore
#line 13 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
      foreach (var row in Model.timetable)
					{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t<td>");
#nullable restore
#line 16 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
      Write(row);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t<td>\r\n\t\t\t\t\t\t\t\t");
#nullable restore
#line 18 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
   Write(day.subjectNames.FirstOrDefault(p => p.id == counter)?.value);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 19 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
         if (ViewBag.role == "Student")
								{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t\t\t\t<br /> ");
#nullable restore
#line 21 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
           Write(day.subjectLecturerNames.FirstOrDefault(p => p.id == counter)?.value);

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
                                                                                     

								}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t</tr>\r\n");
#nullable restore
#line 26 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
						counter += 1;
					}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t</table>\r\n\t\t\t</div>\r\n");
#nullable restore
#line 30 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"
		}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t</div>\r\n");
#nullable restore
#line 32 "C:\Users\dariy\Documents\GitHub\Schedule\Schedule\Views\StudyingProcess\Regimen.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script>
	document.addEventListener(""DOMContentLoaded"", function () {
		var homeElement = document.getElementById(""nav-link-regimen"");
		if (homeElement)
			homeElement.classList.add(""active"");

	});
</script>
<link href=""/css/RegimenStyle.css"" rel=""stylesheet"" />");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RegimenViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

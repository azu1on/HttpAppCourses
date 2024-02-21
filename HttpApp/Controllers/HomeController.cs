using HttpApp.Attributes;
using HttpApp.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task HomePageAsync(HttpListenerContext context)
        {
            using var writer = new StreamWriter(context.Response.OutputStream);

            var pageHtml = await File.ReadAllTextAsync("Views/Home.html");
            await writer.WriteLineAsync(pageHtml);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/html";

        }
    }
}

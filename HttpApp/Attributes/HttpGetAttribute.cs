using HttpApp.Attributes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Attributes
{
    public class HttpGetAttribute : HttpAttribute
    {
        public HttpGetAttribute(string routing) : base(HttpMethod.Get, routing) { }

        public HttpGetAttribute() : base(HttpMethod.Get, null) { }
    }
}
